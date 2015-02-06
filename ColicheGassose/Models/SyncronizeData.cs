using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColicheGassose.Models
{
    public class SyncronizeData
    {
        public UserDataTransport UserData;

        public List<AppointmentTransport> AppointmentsEdit;
        public List<int> AppointmentsDelete;

        public List<PillAlertTransport> PillAlertsEdit;
        public List<int> PillAlertsDelete;

        public List<SymptomTransport> SymptomsEdit;
        public List<int> SymptomsDelete;
    }
}