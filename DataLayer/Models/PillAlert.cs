namespace ColicheGassose
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public enum  PillAlertRemedies
    {
        TerapiaPosizionale = 0,
	    Massaggio,
	    MusicaDolce,
	    Movimento,
	    Probiotici,
	    AltroRimedio
    }

    public partial class PillAlert
    {
        public PillAlert() { }

        public int WhenWeek
        {
            get
            {
                return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(this.When, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            }
        }

        public PillAlert(PillAlertTransport transport)
        {
            this.App_Id = transport.ID;
            this.Asked = transport.Asked.HasValue? transport.Asked.Value == 1 : false;
            this.ParentId = transport.ParentId;
            this.PillId = transport.PillId;
            this.Taken = transport.Taken.HasValue ? transport.Taken.Value == 1 : false;
            this.When = transport.When;
            this.Info = transport.Info;
        }

        public void update(PillAlertTransport pillAlert)
        {
            this.App_Id = pillAlert.ID;
            this.Asked = pillAlert.Asked.HasValue ? pillAlert.Asked.Value == 1 : false;
            this.ParentId = pillAlert.ParentId;
            this.PillId = pillAlert.PillId;
            this.Taken = pillAlert.Asked.HasValue ? pillAlert.Asked.Value == 1 : false;
            this.When = pillAlert.When;
            this.Info = pillAlert.Info;
        }
    }
}
