using ColicheGassose;
using ColicheGassose.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColicheGassose.Controllers
{
    public class ApiController : Controller
    {

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetUtentiStatistics()
        {
            var months = new string[] { "Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno", "Luglio", "Agosto", "Settembre", "Ottobre", "Novembre", "Dicembre" };

            var labelsAccesses = new List<string>();
            var valuesAccesses = new List<int>();

            var labelsNewUsers = new List<string>();
            var valuesNewUsers = new List<int>();

            var labelsTotals = new List<string>();
            var valuesTotals = new List<int>();

            var todayAccesses = 0;
            var monthAccesses = 0;
            var totalUsers = 0;

            string error = "None";

            try
            {
                using (var context = new DataModelContainer())
                {
                    var allUsers = context.UserDataSet.AsEnumerable();

                    //Accesses of today
                    todayAccesses = allUsers.Where(u => u.LastAccess.HasValue && u.LastAccess.Value.Date == DateTime.Now.Date).Count();
                    monthAccesses = allUsers.Where(u => u.LastAccess.HasValue && u.LastAccess.Value.Month == DateTime.Now.Month).Count();
                    totalUsers = allUsers.Count();

                    //Accesses of this month

                    //Total register users

                    //Access for months in last 12 months
                    var monthsCount = 12;
                    var year = DateTime.Now.Year;
                    var month = DateTime.Now.Month;
                    for (int i = 0; i < monthsCount; i++)
                    {
                        var dataAccesses = allUsers
                            .Where(p => p.LastAccess.HasValue && p.LastAccess.Value.Year == year && p.LastAccess.Value.Month == month)
                            .AsEnumerable();
                        
                        labelsAccesses.Add(months[month-1] + " " + year);
                        valuesAccesses.Add(dataAccesses.Count());

                        if (month > 1)
                        {
                            month--;
                        }
                        else
                        {
                            month = 12;
                            year--;
                        }
                    }
                    labelsAccesses.Reverse();
                    valuesAccesses.Reverse();

                    //New Users for months in last 12 months
                    year = DateTime.Now.Year;
                    month = DateTime.Now.Month;
                    for (int i = 0; i < monthsCount; i++)
                    {
                        var dataNewUsers = allUsers
                            .Where(p => p.RegistrationDate.HasValue && p.RegistrationDate.Value.Year == year && p.RegistrationDate.Value.Month == month)
                            .AsEnumerable();

                        labelsNewUsers.Add(months[month - 1] + " " + year);
                        valuesNewUsers.Add(dataNewUsers.Count());

                        if (month > 1)
                        {
                            month--;
                        }
                        else
                        {
                            month = 12;
                            year--;
                        }
                    }
                    labelsNewUsers.Reverse();
                    valuesNewUsers.Reverse();

                    //Total Users for months in last 12 months
                    year = DateTime.Now.Year;
                    month = DateTime.Now.Month;
                    for (int i = 0; i < monthsCount; i++)
                    {
                        var dataTotalUsers = allUsers
                            .Where(
                                p => p.RegistrationDate.HasValue &&
                                    ((p.RegistrationDate.Value.Year == year && p.RegistrationDate.Value.Month <= month) ||
                                    (p.RegistrationDate.Value.Year < year)))
                            .AsEnumerable();

                        labelsTotals.Add(months[month - 1] + " " + year);
                        valuesTotals.Add(dataTotalUsers.Count());

                        if (month > 1)
                        {
                            month--;
                        }
                        else
                        {
                            month = 12;
                            year--;
                        }
                    }
                    labelsTotals.Reverse();
                    valuesTotals.Reverse();
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new
                {
                    todayAccesses = todayAccesses,
                    monthAccesses = monthAccesses,
                    totalUsers = totalUsers,
                    accesses =
                        new
                        {
                            labels = labelsAccesses,
                            data = valuesAccesses
                        },
                    newusers =
                        new
                        {
                            labels = labelsNewUsers,
                            data = valuesNewUsers
                        },
                    totals =
                        new
                        {
                            labels = labelsTotals,
                            data = valuesTotals
                        }
                }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetAppStatistics()
        {
            var deviceTypes = new List<object>();

            string error = "None";

            try
            {
                using (var context = new DataModelContainer())
                {
                    var allUsers = context.UserDataSet.AsEnumerable();

                    var data = allUsers.GroupBy(u => u.DeviceOS).Select(g => g).AsEnumerable();
                    var i = 1;
                    foreach (var group in data)
                    {
                        deviceTypes.Add(new
                        { 
                            label = group.Key, 
                            value = group.Count(),
                            color = "rgb(" + ((int)200 * i / data.Count()) + "," + ((int)200 * i / data.Count()) + "," + ((int)200 * i / data.Count()) + ")",
                            highlight = "rgba(245, 134, 108, 1)",
                        });
                        i++;
                    }
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new
            {
                error = error,
                deviceTypes = deviceTypes
            }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RandomizeDB()
        {
            dynamic result = new System.Dynamic.ExpandoObject();


            using (var context = new DataModelContainer())
            {
                try
                {
                    Random generator = new Random();
                    DateTime eventsStartDate = new DateTime(DateTime.Now.Year - 1, 1, 1, 0, 0, 0);
                    DateTime eventsEndDate = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59);
                    TimeSpan eventsSpan = eventsEndDate - eventsStartDate;

                    //creating some fake user
                    result.FakeUsersCount = generator.Next(50, 101);
                    result.FakeAppointmentsCount = 0;
                    result.FakeSymptomsCount = 0;
                    result.FakeRemindersCount = 0;

                    for (int u = 1; u <= result.FakeUsersCount; u++)
                    {
                        //create user
                        UserData user = context.UserDataSet.Add(new UserData()
                        {
                            App_Id = 1,
                            DeviceOS = u % 2 == 0 ? "android" : "iphone",
                            DeviceOSVersion = "8." + u,
                            Name = "Fake User " + u,
                            RegistrationDate = eventsStartDate + new TimeSpan(0, generator.Next(0, (int)eventsSpan.TotalMinutes), 0),
                            LastAccess = eventsStartDate + new TimeSpan(0, generator.Next(0, (int)eventsSpan.TotalMinutes), 0)
                        });

                        context.SaveChanges(); //becouse we need the user ID for new users

                        //create some appointments
                        int fakeAppointmentsCount = generator.Next(1, 11);
                        result.FakeAppointmentsCount += fakeAppointmentsCount;

                        for (int a = 1; a <= fakeAppointmentsCount; a++)
                        {
                            context.AppointmentSet.Add(new Appointment()
                            {
                                App_Id = a,
                                Info = "Fake Appointment " + a,
                                UserDataID = user.ID,
                                When = eventsStartDate + new TimeSpan(0, generator.Next(0, (int)eventsSpan.TotalMinutes), 0)
                            });
                        }

                        //create some symptoms
                        int fakeSymptomsCount = generator.Next(50, 101);
                        result.FakeSymptomsCount += fakeSymptomsCount;

                        for (int s = 1; s <= fakeSymptomsCount; s++)
                        {
                            context.SymptomSet.Add(new Symptom()
                            {
                                App_Id = s,
                                UserDataID = user.ID,
                                Agitazione = generator.Next(0, 2) == 1,
                                Pianto = generator.Next(0, 2) == 1,
                                Rigurgito = generator.Next(0, 2) == 1,
                                Duration = generator.Next(0, 6),
                                Intensity = generator.Next(0, 6),
                                When = eventsStartDate + new TimeSpan(0, generator.Next(0, (int)eventsSpan.TotalMinutes + 1), 0)
                            });
                        }

                        //create some reminder
                        int fakeRemindersCount = generator.Next(50, 101);
                        result.FakeRemindersCount += fakeRemindersCount;

                        for (int r = 1; r <= fakeRemindersCount; r++)
                        {
                            context.PillAlertSet.Add(new PillAlert()
                            {
                                App_Id = r,
                                UserDataID = user.ID,
                                PillId = generator.Next(0, 6),
                                Taken = true,
                                Asked = true,
                                Info = "Fake Reminder " + r,
                                When = eventsStartDate + new TimeSpan(0, generator.Next(0, (int)eventsSpan.TotalMinutes + 1), 0)
                            });
                        }

                    }
                    context.SaveChanges();
                    result.Message = "Operation complete";
                }
                catch (Exception ex)
                {
                    result.Error = true;
                    result.Message = ex.Message;
                    result.Data = ex.StackTrace;
                }
            }
            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult syncronize(SyncronizeData data)
        {
            SyncronizeResponse result = new SyncronizeResponse();
            try
            {
                if (data == null) throw new InvalidOperationException("Request data is not valid");

                result.UserData = data.UserData;

                using (var context = new DataModelContainer())
                {

                    #region UserData

                    UserData user = null;
                    //new user?
                    if (data.UserData.PatientId == null)
                    {
                        //create
                        user = context.UserDataSet.Add(new UserData(data.UserData));
                        user.RegistrationDate = DateTime.Now;
                    }
                    else
                    {
                        user = context.UserDataSet.Find(data.UserData.PatientId);
                        if (user != null)
                        {
                            //update values
                            user.App_Id = data.UserData.ID;
                            user.Name = data.UserData.Name;
                            user.PatientPID = data.UserData.PatientPID;
                            user.DeviceToken = data.UserData.DeviceToken;
                            user.DeviceOS = data.UserData.DeviceOS;
                            user.DeviceOSVersion = data.UserData.DeviceOSVersion;
                            user.LastAccess = DateTime.Now;
                        }
                        else
                        {
                            //throw new InvalidOperationException(String.Format("User with ID {0} was not found", data.UserData.PatientId));

                            //recreate user
                            user = context.UserDataSet.Add(new UserData(data.UserData));
                            user.RegistrationDate = DateTime.Now;
                        }
                    }

                    if (context.ChangeTracker.HasChanges()) context.SaveChanges(); //becouse we need the user ID for new users

                    //update response server id
                    result.UserData.PatientId = user.ID;

                    #endregion

                    #region Appointments

                    //Appointments Edit/Create
                    if (data.AppointmentsEdit != null)
                    {
                        foreach (var appointment in data.AppointmentsEdit)
                        {
                            //search Appointment
                            Appointment dbAppointment = context.AppointmentSet.Where(s => s.App_Id == appointment.ID && s.UserDataID == user.ID).FirstOrDefault();
                            if (dbAppointment == null)
                            {
                                dbAppointment = new Appointment(appointment);
                                dbAppointment.UserData = user;
                                context.AppointmentSet.Add(dbAppointment);
                            }
                            else
                            {
                                dbAppointment.update(appointment);
                                //remove Notification
                                if (dbAppointment.Notification != null)
                                {
                                    context.NotificationSet.Remove(dbAppointment.Notification);
                                    dbAppointment.Notification = null;
                                }
                            }

                            //generate new Notification
                            var notification = Notification.Generate(appointment.When, user.DeviceToken, user.DeviceOS, String.Format("Appuntamento alle ore {0}", appointment.When.ToString("HH:mm")));

                            if (notification != null)
                            {
                                notification.Appointment = dbAppointment;
                                context.NotificationSet.Add(notification);
                            }
                        }
                    }
                    //Appointments Delete
                    if (data.AppointmentsDelete != null)
                    {
                        foreach (var appointmentApp_ID in data.AppointmentsDelete)
                        {
                            Appointment dbAppointment = context.AppointmentSet.Where(s => s.App_Id == appointmentApp_ID && s.UserDataID == user.ID).FirstOrDefault();
                            if (dbAppointment != null)
                            {
                                if (dbAppointment.Notification != null)
                                {
                                    context.NotificationSet.Remove(dbAppointment.Notification);
                                    dbAppointment.Notification = null;
                                }
                                context.AppointmentSet.Remove(dbAppointment);
                            }
                        }
                    }

                    #endregion

                    #region PillAlerts

                    //PillAlerts Edit/Create
                    if (data.PillAlertsEdit != null)
                    {
                        foreach (var pillAlert in data.PillAlertsEdit)
                        {
                            //search Appointment
                            PillAlert dbPillAlert = context.PillAlertSet.Where(s => s.App_Id == pillAlert.ID && s.UserDataID == user.ID).FirstOrDefault();
                            if (dbPillAlert == null)
                            {
                                dbPillAlert = new PillAlert(pillAlert);
                                dbPillAlert.UserData = user;
                                context.PillAlertSet.Add(dbPillAlert);
                            }
                            else
                            {
                                dbPillAlert.update(pillAlert);
                                //remove Notification
                                if (dbPillAlert.Notification != null)
                                {
                                    context.NotificationSet.Remove(dbPillAlert.Notification);
                                    dbPillAlert.Notification = null;
                                }
                            }

                            //generate new Notification
                            var notification = Notification.Generate(dbPillAlert.When, user.DeviceToken, user.DeviceOS, String.Format("Reminder alle ore {0}", dbPillAlert.When.ToString("HH:mm")));

                            if (notification != null)
                            {
                                notification.PillAlert = dbPillAlert;
                                context.NotificationSet.Add(notification);
                            }
                        }
                    }

                    //PillAlerts Delete
                    if (data.PillAlertsDelete != null)
                    {
                        foreach (var pillAlertApp_ID in data.PillAlertsDelete)
                        {
                            PillAlert dbPillAlert = context.PillAlertSet.Where(s => s.App_Id == pillAlertApp_ID && s.UserDataID == user.ID).FirstOrDefault();
                            if (dbPillAlert != null)
                            {
                                if (dbPillAlert.Notification != null)
                                {
                                    context.NotificationSet.Remove(dbPillAlert.Notification);
                                    dbPillAlert.Notification = null;
                                }
                                context.PillAlertSet.Remove(dbPillAlert);
                            }
                        }
                    }

                    #endregion

                    #region Symptoms

                    //Symptoms Edit/Create
                    if (data.SymptomsEdit != null)
                    {
                        foreach (var symptom in data.SymptomsEdit)
                        {
                            Symptom dbSymptom = context.SymptomSet.Where(s => s.App_Id == symptom.ID && s.UserDataID == user.ID).FirstOrDefault();
                            if (dbSymptom == null)
                            {
                                dbSymptom = new Symptom(symptom);
                                dbSymptom.UserData = user;
                                context.SymptomSet.Add(dbSymptom);
                            }
                            else
                            {
                                dbSymptom.update(symptom);
                            }
                        }
                    }
                    //Symptoms Delete
                    if (data.SymptomsDelete != null && data.SymptomsDelete.Count > 0)
                    {
                        context.SymptomSet.RemoveRange(
                            context.SymptomSet.Where(
                                a => data.SymptomsDelete.Contains(a.App_Id) && a.UserDataID == user.ID));
                    }

                    #endregion

                    //Save changes to DB
                    if (context.ChangeTracker.HasChanges()) context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ex.Message;
                result.Data = ex.StackTrace;
            }

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult echo(object data)
        {
            int userDatas = 0;
            int symptoms = 0;
            int pillAlerts = 0;
            int appointments = 0;
            string error = "None";

            try
            {
                using (var context = new DataModelContainer())
                {
                    userDatas = context.UserDataSet.Count();
                    symptoms = context.SymptomSet.Count();
                    pillAlerts = context.PillAlertSet.Count();
                    appointments = context.AppointmentSet.Count();
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(
                new
                {
                    Error = error,
                    RequestData = data,
                    UserDatas = userDatas,
                    Symptoms = symptoms,
                    PillAlerts = pillAlerts,
                    Appointments = appointments
                });
        }
    }
}