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
    
    public partial class Appointment
    {
        public int ID { get; set; }
        public int App_Id { get; set; }
        public System.DateTime When { get; set; }
        public string Info { get; set; }
        public int UserDataID { get; set; }
    
        public virtual Notification Notification { get; set; }
        public virtual UserData UserData { get; set; }
    }
}
