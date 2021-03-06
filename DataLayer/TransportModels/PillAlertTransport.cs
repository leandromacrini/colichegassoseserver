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

    public partial class PillAlertTransport
    {
        public int ID { get; set; }
        public int PillId { get; set; }
        public System.DateTime When { get; set; }
        public Nullable<int> Taken { get; set; }
        public Nullable<int> Asked { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int UserDataID { get; set; }
        public string Info { get; set; }

        public PillAlertTransport() { }

        public PillAlertTransport(PillAlert pillAlert)
        {
            this.ID = pillAlert.App_Id;
            this.When = pillAlert.When;
            this.PillId = pillAlert.PillId;
            this.Asked = pillAlert.Asked.HasValue && pillAlert.Asked.Value ? 1 : 0;
            this.Taken = pillAlert.Taken.HasValue && pillAlert.Taken.Value ? 1 : 0;
            this.ParentId = pillAlert.ParentId;
            this.UserDataID = pillAlert.UserDataID;
            this.Info = pillAlert.Info;
        }
    }
}
