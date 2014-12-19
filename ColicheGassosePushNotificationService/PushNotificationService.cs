using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ColicheGassosePushNotificationService
{
    public partial class PushNotificationService : ServiceBase
    {
        public PushNotificationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine(String.Format("Coliche Gassose Push Notification Service started at: {0}", DateTime.Now));
        }

        protected override void OnStop()
        {
            Console.WriteLine(String.Format("Coliche Gassose Push Notification Service stoppped at: {0}", DateTime.Now));
        }
    }
}
