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
    
    public partial class Notification
    {
        public int ID { get; set; }
        public NotificationStatusEnum Status { get; set; }
        public System.DateTime When { get; set; }
        public string Message { get; set; }
        public string DeviceToken { get; set; }
        public string DestinationOS { get; set; }
    
        public virtual Appointment Appointment { get; set; }
        public virtual PillAlert PillAlert { get; set; }
    }
}
