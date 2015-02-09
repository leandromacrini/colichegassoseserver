using Newtonsoft.Json;
using PushNotificationServer.Properties;
using PushSharp;
using PushSharp.Android;
using PushSharp.Apple;
using PushSharp.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PushNotificationServer
{
    public partial class MainForm : Form, INotifyPropertyChanged
    {
        List<ConfigurationItem> configurations = new List<ConfigurationItem>();
        BindingSource source = new BindingSource();

        BackgroundWorker worker = new BackgroundWorker();

        int pushInterval = Properties.Settings.Default.PushInterval;

        public void Log(string message)
        {
            if (!Directory.Exists("logs")) Directory.CreateDirectory("logs");

            //log to Today file
            File.AppendAllText(String.Format("logs/{0:yyyyMMdd}.log", DateTime.Now), message + Environment.NewLine, Encoding.ASCII);

            //log to textbox
            txbLog.Text += message + Environment.NewLine;
        }

        public void Reset()
        {
            //reset log
            txbLog.Text = "";
        }

        private bool _isServerEnabled = false;

        [Bindable(true)]
        public bool IsServerEnabled
        {
            get
            {
                return _isServerEnabled;
            }
            set
            {
                _isServerEnabled = value;
                RaisePropertyChanged("IsServerEnabled");
                RaisePropertyChanged("IsServerStartable");
                RaisePropertyChanged("IsServerStoppable");
            }
        }

        public bool IsServerStartable
        {
            get { return !_isServerEnabled; }
        }

        public bool IsServerStoppable
        {
            get { return _isServerEnabled; }
        }

        private void worker_DoWork(object s, DoWorkEventArgs e)
        {
            BackgroundWorker worker = s as BackgroundWorker;
            
            //Create our push services broker
            var push = new PushBroker();

            //Wire up the events for all the services that the broker registers
            push.OnNotificationSent += new NotificationSentDelegate((sender, notification) =>
            {
                //update notication item
                worker.ReportProgress(0, "Sent: " + sender + " -> " + notification);
            });

            push.OnChannelException += new ChannelExceptionDelegate((sender, channel, exception) =>
            {
                worker.ReportProgress(0, "Channel Exception: " + sender + " -> " + exception);
            });

            push.OnServiceException += new ServiceExceptionDelegate((sender, exception) =>
            {
                worker.ReportProgress(0, "Service Exception: " + sender + " -> " + exception);
            });

            push.OnNotificationFailed += new NotificationFailedDelegate((sender, notification, notificationFailureException) =>
            {
                worker.ReportProgress(0, "Failure: " + sender + " -> " + notificationFailureException.Message + " -> " + notification);
            });

            push.OnDeviceSubscriptionExpired += new DeviceSubscriptionExpiredDelegate((sender, expiredDeviceSubscriptionId, timestamp, notification) =>
            {
                worker.ReportProgress(0, "Device Subscription Expired: " + sender + " -> " + expiredDeviceSubscriptionId);
            });

            push.OnDeviceSubscriptionChanged += new DeviceSubscriptionChangedDelegate((sender, oldSubscriptionId, newSubscriptionId, notification) =>
            {
                worker.ReportProgress(0, "Device Registration Changed:  Old-> " + oldSubscriptionId + "  New-> " + newSubscriptionId + " -> " + notification);
            });

            push.OnChannelCreated += new ChannelCreatedDelegate((sender, pushChannel) =>
            {
                worker.ReportProgress(0, "Channel Created for: " + sender);
            });

            push.OnChannelDestroyed += new ChannelDestroyedDelegate((sender) =>
            {
                worker.ReportProgress(0, "Channel Destroyed for: " + sender);
            });


            //Init
            try
            {

                worker.ReportProgress(0, String.Format("-- SERVER START AT {0:d/M/yy HH:mm} --", DateTime.Now));

                string connection = Properties.Settings.Default.ConnectionString;
                using (SqlConnection myConnection = new SqlConnection(connection))
                {
                    worker.ReportProgress(0, "DB connection Ok");

                    //reset configurations
                    configurations.Clear();

                    string oString = "Select * from ConfigurationSet";
                    SqlCommand oCmd = new SqlCommand(oString, myConnection);
                    myConnection.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            //read configuration

                            ConfigurationItem item = new ConfigurationItem(oReader);

                            if (item.Autostart)
                            {
                                item.Status = ConfigurationStatus.Started;
                            }

                            //create push config Apple
                            if( !String.IsNullOrEmpty(item.AppleCertificate) )
                            {
                                var appleCert = File.ReadAllBytes(Path.Combine(new[] { Directory.GetCurrentDirectory(), "Certificates", item.AppleCertificate }));
                                push.RegisterAppleService(new ApplePushChannelSettings(item.Production, appleCert, item.ApplePassword), item.getUniqueName() + ".APNS");
                            }

                            //create push config Android
                            if (!String.IsNullOrEmpty(item.GoogleKey))
                            {
                                push.RegisterGcmService(new GcmPushChannelSettings(item.GoogleKey), item.getUniqueName() + ".GCM");
                            }

                            configurations.Add(item);
                        }

                        myConnection.Close();

                        //autostart configuration
                        worker.ReportProgress(0, String.Format("Found {0} configurations", configurations.Count()));
                    }

                    //update datasource
                    this.gridConfigurations.Invoke((MethodInvoker)delegate { source.ResetBindings(false); });
                }
            }
            catch (Exception ex)
            {
                worker.ReportProgress(0, String.Format("-- SERVER ERROR AT {0:d/M/yy HH:mm} --", DateTime.Now));
                worker.ReportProgress(0, ex.Message + Environment.NewLine);
                return; //abort
            }

            //Never Ending Sto... ehm, loop
            while(true)
            {
                if ((worker.CancellationPending == true))
                {
                    push.StopAllServices();
                    worker.ReportProgress(0, String.Format("-- SERVER STOP AT {0:d/M/yy HH:mm} --" + Environment.NewLine, DateTime.Now));
                    e.Cancel = true;
                    break;
                }
                else
                {
                    foreach (ConfigurationItem config in configurations)
                    {
                        if (config.Status == ConfigurationStatus.Started)
                        {
                            //send notification
                            foreach (var notification in config.getCurrentNotifications())
                            {
                                if (notification.isForAndroid())
                                {
                                    //ANDROID
                                    push.QueueNotification(
                                        new GcmNotification().ForDeviceRegistrationId(notification.DeviceToken)
                                            .WithJson(JsonConvert.SerializeObject(new { alert = notification.Message, badge = 1 })),
                                        config.getUniqueName() + ".GCM"
                                    );
                                }
                                else
                                {
                                    //APPLE
                                    push.QueueNotification(
                                        new AppleNotification()
                                            .ForDeviceToken(notification.DeviceToken)
                                            .WithAlert(notification.Message)
                                            .WithSound("default")
                                            .WithBadge(1),
                                        config.getUniqueName() + ".APNS"
                                        );
                                }
                            }
                        }
                    }

                    //update datasource
                    this.gridConfigurations.Invoke((MethodInvoker)delegate { source.ResetBindings(false); });

                    // Perform a time consuming operation
                    System.Threading.Thread.Sleep(pushInterval * 1000);
                }

                
            }
        }

        public MainForm()
        {
            InitializeComponent();

            //worker
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsServerEnabled = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //grid binding
            source.DataSource = configurations;
            gridConfigurations.DataSource = source;

            btnStart.DataBindings.Add("Enabled", this, "IsServerStartable");
            btnStop.DataBindings.Add("Enabled", this, "IsServerStoppable");
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Log(e.UserState.ToString());
        }

        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                this.Show();
                this.Activate();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!realExit)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void openServerConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();
        }


        private bool realExit = false;
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            realExit = true;
            Application.Exit();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!worker.IsBusy)
            {
                IsServerEnabled = true;
                worker.RunWorkerAsync();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy) worker.CancelAsync();

            while (worker.IsBusy)
            {
                Application.DoEvents();
            }

            IsServerEnabled = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
