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
                        }
                        else
                        {
                            throw new InvalidOperationException(String.Format("User with ID {0} was not found", data.UserData.PatientId));
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
                            var notification = Notification.Generate(appointment.When, user.DeviceToken, String.Format("Appuntamento alle ore {0}", appointment.When.ToString("HH:mm")));

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
                            var notification = Notification.Generate(dbPillAlert.When, user.DeviceToken, String.Format("Reminder alle ore {0}", dbPillAlert.When.ToString("HH:mm")));

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
                    if(context.ChangeTracker.HasChanges()) context.SaveChanges();
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
            catch(Exception ex)
            {
                error = ex.Message;
            }

            return Json(
                new {
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