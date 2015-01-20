using ColicheGassose;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushNotificationServer
{
    public enum ConfigurationStatus{
        Started,
        Stopped,
        None
    }

    class ConfigurationItem
    {
        // FROM DB
        public int Id { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public bool Autostart { get; set; }

        //FROM APPLICATION
        public ConfigurationStatus Status { get; set; }
        public int Pushes { get; set; }
        public int Errors { get; set; }

        public ConfigurationItem()
        {
            Pushes = 0;
            Errors = 0;
            Status = ConfigurationStatus.None;
        }

        public void checkNotifications()
        {
            using (SqlConnection myConnection = new SqlConnection(this.ConnectionString))
            {
                string oString = "Select * from NotificationSet";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        Notification notification = new Notification();

                        notification.Id = (int)oReader["Id"];
                        notification.Status = (int)oReader["Status"];
                        notification.When = DateTime.Parse(oReader["When"].ToString());
                        notification.Message = oReader["Message"].ToString();
                        notification.DeviceToken = oReader["DeviceToken"].ToString();
                    }

                    myConnection.Close();
                }
            }
        }
    }
}
