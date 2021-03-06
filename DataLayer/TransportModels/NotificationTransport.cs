//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ColicheGassose
{
    using System;
    using System.Collections.Generic;

    public partial class NotificationTransport
    {
        public int ID { get; set; }
        public NotificationStatusEnum Status { get; set; }
        public System.DateTime When { get; set; }
        public string Message { get; set; }
        public string DeviceToken { get; set; }

        public NotificationTransport() { }

        public NotificationTransport(Notification notification)
        {
            this.ID = notification.ID;
            this.Status = notification.Status;
            this.When = notification.When;
            this.Message = notification.Message;
            this.DeviceToken = notification.DeviceToken;
        }
    }
}
