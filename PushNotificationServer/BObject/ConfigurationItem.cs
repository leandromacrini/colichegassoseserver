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

    public class ConfigurationItem
    {
        // FROM DB
        public int Id { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public bool Autostart { get; set; }
        public string AppleCertificate { get; set; }
        public string ApplePassword { get; set; }
        public string GoogleKey { get; set; }
        public string GoogleSenderID { get; set; }
        public string GoogleAppName { get; set; }
        public bool Production { get; set; }

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

        public ConfigurationItem(SqlDataReader oReader) : this()
        {
            this.Id = (int)oReader["Id"];
            this.Name = oReader["Name"].ToString();
            this.ConnectionString = oReader["ConnectionString"].ToString();
            this.Autostart = (bool)oReader["Autostart"];
            this.AppleCertificate = oReader["AppleCertificate"].ToString();
            this.ApplePassword = oReader["ApplePassword"].ToString();
            this.GoogleKey = oReader["GoogleKey"].ToString();
            this.GoogleSenderID = oReader["GoogleSenderID"].ToString();
            this.GoogleAppName = oReader["GoogleAppName"].ToString();
            this.Production = (bool)oReader["Production"];
        }

        public string getUniqueName()
        {
            return this.Id + "." + this.Name;
        }

        public List<Notification> getCurrentNotifications()
        {
            var result = new List<Notification>();
            using (SqlConnection myConnection = new SqlConnection(this.ConnectionString))
            {
                string oString = "Select * from NotificationSet"; //TODO only for correct datetime
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        result.Add(new Notification(oReader, this));
                    }

                    myConnection.Close();
                }
            }
            return result;
        }
    }
}
