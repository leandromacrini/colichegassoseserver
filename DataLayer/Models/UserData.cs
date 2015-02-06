namespace ColicheGassose
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserData
    {
        public UserData(UserDataTransport transport)
        {
            this.App_Id = transport.App_Id;
            this.Name = transport.Name;
            this.PatientPID = transport.PatientPID;
            this.DeviceToken = transport.DeviceToken;
            this.OS = transport.OS;
        }
    }
}
