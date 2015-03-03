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
        public ActionResult GetRimediStatistics()
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