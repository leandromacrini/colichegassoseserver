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

    public partial class AppointmentTransport
    {
        public int ID { get; set; }
        public System.DateTime When { get; set; }
        public string Info { get; set; }
        public int UserDataID { get; set; }

        public AppointmentTransport() { }

        public AppointmentTransport(Appointment appointment)
        {
            this.ID = appointment.App_Id;
            this.When = appointment.When;
            this.Info = appointment.Info;
            this.UserDataID = appointment.UserDataID;
        }
    }
}
