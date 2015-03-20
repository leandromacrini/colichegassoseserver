using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ColicheGassose.Controllers
{
    public class StatisticsController : Controller
    {
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetSintomiStatistics()
        {
            var symptomsThisWeek = new List<object>();
            var symptomsThisWeekVLS = new List<object>();

            string error = "None";

            int weeklyPianti = 0;
            int weeklyPiantiIntensitaMedia = 0;
            int weeklyPiantiDurataMedia = 0;

            int weeklyRigurgiti = 0;
            int weeklyRigurgitiIntensitaMedia = 0;
            int weeklyRigurgitiDurataMedia = 0;

            int weeklyAgitazioni = 0;
            int weeklyAgitazioniIntensitaMedia = 0;
            int weeklyAgitazioniDurataMedia = 0;

            int weeklyPiantiVLS = 0;
            int weeklyPiantiIntensitaMediaVLS = 0;
            int weeklyPiantiDurataMediaVLS = 0;

            int weeklyRigurgitiVLS = 0;
            int weeklyRigurgitiIntensitaMediaVLS = 0;
            int weeklyRigurgitiDurataMediaVLS = 0;

            int weeklyAgitazioniVLS = 0;
            int weeklyAgitazioniIntensitaMediaVLS = 0;
            int weeklyAgitazioniDurataMediaVLS = 0;

            try
            {
                using (var context = new DataModelContainer())
                {
                    var allSymptoms = context.SymptomSet.AsEnumerable();

                    weeklyPianti = allSymptoms.Where(s => s.Pianto && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Count();
                    weeklyPiantiIntensitaMedia = (int) allSymptoms.Where(s => s.Pianto && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Select(s => s.Intensity).Average();
                    weeklyPiantiDurataMedia = (int) allSymptoms.Where(s => s.Pianto && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Select(s => s.Duration).Average();
                    
                    weeklyRigurgiti = allSymptoms.Where(s => s.Rigurgito && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Count();
                    weeklyRigurgitiIntensitaMedia = (int) allSymptoms.Where(s => s.Rigurgito && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Select(s => s.Intensity).Average();
                    weeklyRigurgitiDurataMedia = (int) allSymptoms.Where(s => s.Rigurgito && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Select(s => s.Duration).Average();

                    weeklyAgitazioni = allSymptoms.Where(s => s.Agitazione && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Count();
                    weeklyAgitazioniIntensitaMedia = (int) allSymptoms.Where(s => s.Agitazione && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Select(s => s.Intensity).Average();
                    weeklyAgitazioniDurataMedia = (int) allSymptoms.Where(s => s.Agitazione && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Select(s => s.Duration).Average();

                    var vlsUsers =
                        (from pillAlert in context.PillAlertSet
                         where !String.IsNullOrEmpty(pillAlert.Info) && pillAlert.Info.ToLower().Contains("vls")
                         group pillAlert by pillAlert.UserDataID into g
                         select new { UserDataID = g.Key, FirstVLSUse = g.Min(pa => pa.When) }).ToList();

                    weeklyPiantiVLS = allSymptoms.Where(
                            s => s.Pianto &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vlsUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVLSUse)).Count();
                    weeklyPiantiIntensitaMediaVLS = (int)allSymptoms.Where(
                            s => s.Pianto &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vlsUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVLSUse))
                            .Select(s => s.Intensity)
                            .Average();
                    weeklyPiantiDurataMediaVLS = (int)allSymptoms.Where(
                            s => s.Pianto &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vlsUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVLSUse))
                            .Select(s => s.Duration)
                            .Average();

                    weeklyRigurgitiVLS = allSymptoms.Where(
                            s => s.Rigurgito &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vlsUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVLSUse)).Count();
                    weeklyRigurgitiIntensitaMediaVLS = (int)allSymptoms.Where(
                            s => s.Rigurgito &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vlsUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVLSUse))
                            .Select(s => s.Intensity)
                            .Average();
                    weeklyRigurgitiDurataMediaVLS = (int)allSymptoms.Where(
                            s => s.Rigurgito &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vlsUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVLSUse))
                            .Select(s => s.Duration)
                            .Average();

                    weeklyAgitazioniVLS = allSymptoms.Where(
                            s => s.Agitazione &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vlsUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVLSUse)).Count();
                    weeklyAgitazioniIntensitaMediaVLS = (int)allSymptoms.Where(
                            s => s.Agitazione &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vlsUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVLSUse))
                            .Select(s => s.Intensity)
                            .Average();
                    weeklyAgitazioniDurataMediaVLS = (int)allSymptoms.Where(
                            s => s.Agitazione &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vlsUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVLSUse))
                            .Select(s => s.Duration)
                            .Average();

                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new
            {
                error = error,
                weeklyPianti = weeklyPianti,
                weeklyPiantiIntensitaMedia = weeklyPiantiIntensitaMedia,
                weeklyPiantiDurataMedia = weeklyPiantiDurataMedia,

                weeklyRigurgiti = weeklyRigurgiti,
                weeklyRigurgitiIntensitaMedia = weeklyRigurgitiIntensitaMedia,
                weeklyRigurgitiDurataMedia = weeklyRigurgitiDurataMedia,

                weeklyAgitazioni = weeklyAgitazioni,
                weeklyAgitazioniIntensitaMedia = weeklyAgitazioniIntensitaMedia,
                weeklyAgitazioniDurataMedia = weeklyAgitazioniDurataMedia,

                weeklyPiantiVLS = weeklyPiantiVLS,
                weeklyPiantiIntensitaMediaVLS = weeklyPiantiIntensitaMediaVLS,
                weeklyPiantiDurataMediaVLS = weeklyPiantiDurataMediaVLS,

                weeklyRigurgitiVLS = weeklyRigurgitiVLS,
                weeklyRigurgitiIntensitaMediaVLS = weeklyRigurgitiIntensitaMediaVLS,
                weeklyRigurgitiDurataMediaVLS = weeklyRigurgitiDurataMediaVLS,

                weeklyAgitazioniVLS = weeklyAgitazioniVLS,
                weeklyAgitazioniIntensitaMediaVLS = weeklyAgitazioniIntensitaMediaVLS,
                weeklyAgitazioniDurataMediaVLS = weeklyAgitazioniDurataMediaVLS,
                
                symptomsCounts = new List<object>()
                {
                    new
                        {
                            label = "Pianti",
                            value = weeklyPianti,
                            color = "#AA0000",
                            highlight = "#FF0000"
                        },
                    new
                    {
                        label = "Rigurgiti",
                        value = weeklyRigurgiti,
                        color = "#00AA00",
                        highlight = "#00FF00"
                    },
                    new
                    {
                        label = "Agitazioni",
                        value = weeklyAgitazioni,
                        color = "#0000AA",
                        highlight = "#0000FF"
                    }
                },

                symptomsCountsVLS = new List<object>()
                {
                    new
                        {
                            label = "Pianti",
                            value = weeklyPiantiVLS,
                            color = "#AA0000",
                            highlight = "#FF0000"
                        },
                    new
                    {
                        label = "Rigurgiti",
                        value = weeklyRigurgitiVLS,
                        color = "#00AA00",
                        highlight = "#00FF00"
                    },
                    new
                    {
                        label = "Agitazioni",
                        value = weeklyAgitazioniVLS,
                        color = "#0000AA",
                        highlight = "#0000FF"
                    }
                }
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetRimediStatistics()
        {
            var months = new string[] { "Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno", "Luglio", "Agosto", "Settembre", "Ottobre", "Novembre", "Dicembre" };

            var labelsRimedi = new List<string>();
            var valuesRimedi = new List<int>();

            var labelsRimediVLS = new List<string>();
            var valuesRimediVLS = new List<int>();

            int totalRimedi = 0;
            int totalRimediVLS = 0;

            string error = "None";

            try
            {
                using (var context = new DataModelContainer())
                {
                    totalRimedi = context.PillAlertSet.Count();
                    totalRimediVLS = context.PillAlertSet.Where(p => p.Info.ToLower().Contains("vls")).Count();

                    //Rimedi for months in last 12 months
                    var monthsCount = 12;
                    var year = DateTime.Now.Year;
                    var month = DateTime.Now.Month;
                    for (int i = 0; i < monthsCount; i++)
                    {
                        var dataRimedi = context.PillAlertSet
                            .Where(p => p.When.Year == year && p.When.Month == month)
                            .AsEnumerable();

                        labelsRimedi.Add(months[month - 1] + " " + year);
                        valuesRimedi.Add(dataRimedi.Count());

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
                    labelsRimedi.Reverse();
                    valuesRimedi.Reverse();

                    //VLS Rimedi for months in last 12 months
                    year = DateTime.Now.Year;
                    month = DateTime.Now.Month;
                    for (int i = 0; i < monthsCount; i++)
                    {
                        var dataRimediVLS = context.PillAlertSet
                            .Where(p => p.When.Year == year && p.When.Month == month && p.Info.ToLower().Contains("vls"))
                            .AsEnumerable();

                        labelsRimediVLS.Add(months[month - 1] + " " + year);
                        valuesRimediVLS.Add(dataRimediVLS.Count());

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
                    labelsRimediVLS.Reverse();
                    valuesRimediVLS.Reverse();
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new
            {
                totalRimedi = totalRimedi,
                totalRimediVLS = totalRimediVLS,
                rimedi =
                    new
                    {
                        labels = labelsRimedi,
                        data = valuesRimedi
                    },
                rimediVLS =
                    new
                    {
                        labels = labelsRimediVLS,
                        data = valuesRimediVLS
                    }
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetAppuntamentiStatistics()
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

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
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
                    monthAccesses = allUsers.Where(u => u.LastAccess.HasValue && u.LastAccess.Value.Month == DateTime.Now.Month && u.LastAccess.Value.Year == DateTime.Now.Year).Count();
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

                        labelsAccesses.Add(months[month - 1] + " " + year);
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

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
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

        [Authorize]
        [HttpGet]
        public ActionResult ShowStatistics()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Home");
        }
    }
}