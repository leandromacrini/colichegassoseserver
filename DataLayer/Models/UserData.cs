namespace ColicheGassose
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserData
    {
        public UserData(UserDataTransport transport)
        {
            this.ID = transport.PatientId ?? 0;
            this.App_Id = transport.ID;
            this.Name = transport.Name;
            this.PatientPID = transport.PatientPID;
            this.DeviceToken = transport.DeviceToken;
            this.DeviceOSVersion = transport.DeviceOSVersion;
            this.DeviceOS = transport.DeviceOS;
        }
    }
}
