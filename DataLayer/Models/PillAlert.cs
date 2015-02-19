namespace ColicheGassose
{
    using System;
    using System.Collections.Generic;

    public partial class PillAlert
    {
        public PillAlert() { }

        public PillAlert(PillAlertTransport transport)
        {
            this.App_Id = transport.App_Id;
            this.Asked = transport.Asked;
            this.ParentId = transport.ParentId;
            this.PillId = transport.PillId;
            this.Taken = transport.Taken;
            this.When = transport.When;
            this.Info = transport.Info;
        }

        public void update(PillAlertTransport pillAlert)
        {
            this.App_Id = pillAlert.App_Id;
            this.Asked = pillAlert.Asked;
            this.ParentId = pillAlert.ParentId;
            this.PillId = pillAlert.PillId;
            this.Taken = pillAlert.Taken;
            this.When = pillAlert.When;
            this.Info = pillAlert.Info;
        }
    }
}
