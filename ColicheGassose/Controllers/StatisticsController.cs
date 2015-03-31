using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ColicheGassose.Controllers
{
    public class StatisticsController : Controller
    {
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetMedieStatistics()
        {
            var symptomsThisWeek = new List<object>();
            var symptomsThisWeekVSL = new List<object>();

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

            int weeklyPiantiVSL = 0;
            int weeklyPiantiIntensitaMediaVSL = 0;
            int weeklyPiantiDurataMediaVSL = 0;

            int weeklyRigurgitiVSL = 0;
            int weeklyRigurgitiIntensitaMediaVSL = 0;
            int weeklyRigurgitiDurataMediaVSL = 0;

            int weeklyAgitazioniVSL = 0;
            int weeklyAgitazioniIntensitaMediaVSL = 0;
            int weeklyAgitazioniDurataMediaVSL = 0;

            try
            {
                using (var context = new DataModelContainer())
                {
                    var allSymptoms = context.SymptomSet.AsEnumerable();

                    weeklyPianti = allSymptoms.Where(s => s.Pianto && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Count();
                    weeklyPiantiIntensitaMedia = (int)allSymptoms.Where(s => s.Pianto && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Select(s => s.Intensity).Average();
                    weeklyPiantiDurataMedia = (int)allSymptoms.Where(s => s.Pianto && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Select(s => s.Duration).Average();

                    weeklyRigurgiti = allSymptoms.Where(s => s.Rigurgito && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Count();
                    weeklyRigurgitiIntensitaMedia = (int)allSymptoms.Where(s => s.Rigurgito && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Select(s => s.Intensity).Average();
                    weeklyRigurgitiDurataMedia = (int)allSymptoms.Where(s => s.Rigurgito && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Select(s => s.Duration).Average();

                    weeklyAgitazioni = allSymptoms.Where(s => s.Agitazione && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Count();
                    weeklyAgitazioniIntensitaMedia = (int)allSymptoms.Where(s => s.Agitazione && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Select(s => s.Intensity).Average();
                    weeklyAgitazioniDurataMedia = (int)allSymptoms.Where(s => s.Agitazione && s.When.Year == DateTime.Now.Year && s.When.DayOfYear > DateTime.Now.DayOfYear - 7 && s.When.DayOfYear <= DateTime.Now.DayOfYear).Select(s => s.Duration).Average();

                    var vslUsers =
                        (from pillAlert in context.PillAlertSet
                         where !String.IsNullOrEmpty(pillAlert.Info) && pillAlert.Info.ToLower().Contains("vsl")
                         group pillAlert by pillAlert.UserDataID into g
                         select new { UserDataID = g.Key, FirstVSLUse = g.Min(pa => pa.When) }).ToList();

                    weeklyPiantiVSL = allSymptoms.Where(
                            s => s.Pianto &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse)).Count();
                    weeklyPiantiIntensitaMediaVSL = (int)allSymptoms.Where(
                            s => s.Pianto &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse))
                            .Select(s => s.Intensity)
                            .Average();
                    weeklyPiantiDurataMediaVSL = (int)allSymptoms.Where(
                            s => s.Pianto &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse))
                            .Select(s => s.Duration)
                            .Average();

                    weeklyRigurgitiVSL = allSymptoms.Where(
                            s => s.Rigurgito &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse)).Count();
                    weeklyRigurgitiIntensitaMediaVSL = (int)allSymptoms.Where(
                            s => s.Rigurgito &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse))
                            .Select(s => s.Intensity)
                            .Average();
                    weeklyRigurgitiDurataMediaVSL = (int)allSymptoms.Where(
                            s => s.Rigurgito &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse))
                            .Select(s => s.Duration)
                            .Average();

                    weeklyAgitazioniVSL = allSymptoms.Where(
                            s => s.Agitazione &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse)).Count();
                    weeklyAgitazioniIntensitaMediaVSL = (int)allSymptoms.Where(
                            s => s.Agitazione &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse))
                            .Select(s => s.Intensity)
                            .Average();
                    weeklyAgitazioniDurataMediaVSL = (int)allSymptoms.Where(
                            s => s.Agitazione &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse))
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

                weeklyPiantiVSL = weeklyPiantiVSL,
                weeklyPiantiIntensitaMediaVSL = weeklyPiantiIntensitaMediaVSL,
                weeklyPiantiDurataMediaVSL = weeklyPiantiDurataMediaVSL,

                weeklyRigurgitiVSL = weeklyRigurgitiVSL,
                weeklyRigurgitiIntensitaMediaVSL = weeklyRigurgitiIntensitaMediaVSL,
                weeklyRigurgitiDurataMediaVSL = weeklyRigurgitiDurataMediaVSL,

                weeklyAgitazioniVSL = weeklyAgitazioniVSL,
                weeklyAgitazioniIntensitaMediaVSL = weeklyAgitazioniIntensitaMediaVSL,
                weeklyAgitazioniDurataMediaVSL = weeklyAgitazioniDurataMediaVSL,

                symptomsCounts = new List<object>()
                {
                    new
                        {
                            label = "Terapia posizionale",
                            color = "rgba(132, 245, 108, 1)",
                            value = 15,
                            highlight = "#EEE"
                        },
                    new
                    {
                        label = "Massaggio",
                        value = 5,
                        color = "rgba(108, 134, 245, 1)",
                        highlight = "#EEE"
                    },
                    new
                    {
                        label = "Musica dolce",
                        value = 6,
                        color = "rgba(245, 108, 217, 1)",
                        highlight = "#EEE"
                    },
                    new
                    {
                        label = "Movimento",
                        value = 7,
                        color = "rgba(108, 245, 245, 1)",
                        highlight = "#EEE"
                    },
                    new
                    {
                        label = "Probiotici",
                        value = 7,
                        color = "rgba(255, 0, 0, 1)",
                        highlight = "#EEE"
                    },
                    new
                    {
                        label = "Rimedio personale",
                        value = 5,
                        color = "rgba(50, 134, 108, 1)",
                        highlight = "#EEE"
                    }
                },

                symptomsCountsVSL = new List<object>()
                {
                    new
                        {
                            label = "Terapia posizionale",
                            color = "rgba(132, 245, 108, 1)",
                            value = 15,
                            highlight = "#EEE"
                        },
                    new
                    {
                        label = "Massaggio",
                        value = 5,
                        color = "rgba(108, 134, 245, 1)",
                        highlight = "#EEE"
                    },
                    new
                    {
                        label = "Musica dolce",
                        value = 6,
                        color = "rgba(245, 108, 217, 1)",
                        highlight = "#EEE"
                    },
                    new
                    {
                        label = "Movimento",
                        value = 7,
                        color = "rgba(108, 245, 245, 1)",
                        highlight = "#EEE"
                    },
                    new
                    {
                        label = "Probiotici",
                        value = 7,
                        color = "rgba(255, 0, 0, 1)",
                        highlight = "#EEE"
                    },
                    new
                    {
                        label = "VSL#3",
                        value = 10,
                        color = "#f5866c",
                        highlight = "#EEE"
                    },
                    new
                    {
                        label = "Rimedio personale",
                        value = 5,
                        color = "rgba(50, 134, 108, 1)",
                        highlight = "#EEE"
                    }
                }
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetSintomiStatistics()
        {
            var symptomsThisWeek = new List<object>();
            var symptomsThisWeekVSL = new List<object>();

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

            int weeklyPiantiVSL = 0;
            int weeklyPiantiIntensitaMediaVSL = 0;
            int weeklyPiantiDurataMediaVSL = 0;

            int weeklyRigurgitiVSL = 0;
            int weeklyRigurgitiIntensitaMediaVSL = 0;
            int weeklyRigurgitiDurataMediaVSL = 0;

            int weeklyAgitazioniVSL = 0;
            int weeklyAgitazioniIntensitaMediaVSL = 0;
            int weeklyAgitazioniDurataMediaVSL = 0;

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

                    var vslUsers =
                        (from pillAlert in context.PillAlertSet
                         where !String.IsNullOrEmpty(pillAlert.Info) && pillAlert.Info.ToLower().Contains("vsl")
                         group pillAlert by pillAlert.UserDataID into g
                         select new { UserDataID = g.Key, FirstVSLUse = g.Min(pa => pa.When) }).ToList();

                    weeklyPiantiVSL = allSymptoms.Where(
                            s => s.Pianto &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse)).Count();
                    weeklyPiantiIntensitaMediaVSL = (int)allSymptoms.Where(
                            s => s.Pianto &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse))
                            .Select(s => s.Intensity)
                            .Average();
                    weeklyPiantiDurataMediaVSL = (int)allSymptoms.Where(
                            s => s.Pianto &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse))
                            .Select(s => s.Duration)
                            .Average();

                    weeklyRigurgitiVSL = allSymptoms.Where(
                            s => s.Rigurgito &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse)).Count();
                    weeklyRigurgitiIntensitaMediaVSL = (int)allSymptoms.Where(
                            s => s.Rigurgito &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse))
                            .Select(s => s.Intensity)
                            .Average();
                    weeklyRigurgitiDurataMediaVSL = (int)allSymptoms.Where(
                            s => s.Rigurgito &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse))
                            .Select(s => s.Duration)
                            .Average();

                    weeklyAgitazioniVSL = allSymptoms.Where(
                            s => s.Agitazione &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse)).Count();
                    weeklyAgitazioniIntensitaMediaVSL = (int)allSymptoms.Where(
                            s => s.Agitazione &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse))
                            .Select(s => s.Intensity)
                            .Average();
                    weeklyAgitazioniDurataMediaVSL = (int)allSymptoms.Where(
                            s => s.Agitazione &&
                            s.When.Year == DateTime.Now.Year &&
                            s.When.DayOfYear > DateTime.Now.DayOfYear - 7 &&
                            s.When.DayOfYear <= DateTime.Now.DayOfYear &&
                            vslUsers.Any(u => u.UserDataID == s.UserDataID && s.When >= u.FirstVSLUse))
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

                weeklyPiantiVSL = weeklyPiantiVSL,
                weeklyPiantiIntensitaMediaVSL = weeklyPiantiIntensitaMediaVSL,
                weeklyPiantiDurataMediaVSL = weeklyPiantiDurataMediaVSL,

                weeklyRigurgitiVSL = weeklyRigurgitiVSL,
                weeklyRigurgitiIntensitaMediaVSL = weeklyRigurgitiIntensitaMediaVSL,
                weeklyRigurgitiDurataMediaVSL = weeklyRigurgitiDurataMediaVSL,

                weeklyAgitazioniVSL = weeklyAgitazioniVSL,
                weeklyAgitazioniIntensitaMediaVSL = weeklyAgitazioniIntensitaMediaVSL,
                weeklyAgitazioniDurataMediaVSL = weeklyAgitazioniDurataMediaVSL,
                
                symptomsCounts = new List<object>()
                {
                    new
                        {
                            label = "Pianti",
                            value = weeklyPianti,
                            color = "#F56C6C",
                            highlight = "#FF0000"
                        },
                    new
                    {
                        label = "Rigurgiti",
                        value = weeklyRigurgiti,
                        color = "#F29696",
                        highlight = "#FF0000"
                    },
                    new
                    {
                        label = "Agitazioni",
                        value = weeklyAgitazioni,
                        color = "#F0BABA",
                        highlight = "#FF0000"
                    }
                },

                symptomsCountsVSL = new List<object>()
                {
                    new
                        {
                            label = "Pianti",
                            value = weeklyPiantiVSL,
                            color = "#f5866c",
                            highlight = "#FF0000"
                        },
                    new
                    {
                        label = "Rigurgiti",
                        value = weeklyRigurgitiVSL,
                        color = "#F5AB9A",
                        highlight = "#FF0000"
                    },
                    new
                    {
                        label = "Agitazioni",
                        value = weeklyAgitazioniVSL,
                        color = "#F4CFC7",
                        highlight = "#FF0000"
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
            var valuesRimedi = new List<int>[] { new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>() };

            var labelsRimediVSL = new List<string>();
            var valuesRimediVSL = new List<int>();

            int totalRimedi = 0;
            int totalRimediVSL = 0;

            string error = "None";

            try
            {
                using (var context = new DataModelContainer())
                {
                    totalRimedi = context.PillAlertSet.Count();
                    totalRimediVSL = context.PillAlertSet.Where(p => p.Info.ToLower().Contains("vsl")).Count();

                    var year = DateTime.Now.Year;
                    var month = DateTime.Now.Month;

                    //Rimedi for months in last 12 months
                    var monthsCount = 12;
                    for (int pillID = 0; pillID < 6; pillID++)
                    {
                        year = DateTime.Now.Year;
                        month = DateTime.Now.Month;
                        for (int i = 0; i < monthsCount; i++)
                        {
                            var dataRimedi = context.PillAlertSet
                                .Where(p => p.When.Year == year && p.When.Month == month && p.PillId == pillID)
                                .AsEnumerable();

                            if (pillID == 0)
                            {
                                labelsRimedi.Add(months[month - 1] + " " + year);
                            }
                            valuesRimedi[pillID].Add(dataRimedi.Count());

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
                        valuesRimedi[pillID].Reverse();
                    }
                   
                    labelsRimedi.Reverse();

                    //VSL Rimedi for months in last 12 months
                    year = DateTime.Now.Year;
                    month = DateTime.Now.Month;
                    for (int i = 0; i < monthsCount; i++)
                    {
                        var dataRimediVSL = context.PillAlertSet
                            .Where(p => p.When.Year == year && p.When.Month == month && p.Info.ToLower().Contains("vsl"))
                            .AsEnumerable();

                        labelsRimediVSL.Add(months[month - 1] + " " + year);
                        valuesRimediVSL.Add(dataRimediVSL.Count());

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
                    labelsRimediVSL.Reverse();
                    valuesRimediVSL.Reverse();
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new
            {
                totalRimedi = totalRimedi,
                totalRimediVSL = totalRimediVSL,
                rimedi =
                    new
                    {
                        labels = labelsRimedi,
                        data = valuesRimedi
                    },
                rimediVSL =
                    new
                    {
                        labels = labelsRimediVSL,
                        data = valuesRimediVSL
                    }
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetAppuntamentiStatistics()
        {
            var months = new string[] { "Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno", "Luglio", "Agosto", "Settembre", "Ottobre", "Novembre", "Dicembre" };

            var labelsAppointments = new List<string>();
            var valuesAppointments = new List<int>();

            var totalAppointments = 0;

            string error = "None";

            try
            {
                using (var context = new DataModelContainer())
                {
                    totalAppointments = context.AppointmentSet.Count();

                    //Appointments for months in last 12 months
                    var monthsCount = 12;
                    var year = DateTime.Now.Year;
                    var month = DateTime.Now.Month;
                    for (int i = 0; i < monthsCount; i++)
                    {
                        var dataAppointments = context.AppointmentSet
                            .Where(apt => apt.When.Year == year && apt.When.Month == month)
                            .AsEnumerable();

                        labelsAppointments.Add(months[month - 1] + " " + year);
                        valuesAppointments.Add(dataAppointments.Count());

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
                    labelsAppointments.Reverse();
                    valuesAppointments.Reverse();
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new
            {
                totalAppointments = totalAppointments,
                appointments =
                    new
                    {
                        labels = labelsAppointments,
                        data = valuesAppointments
                    }
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
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ExportStatistics()
        {
            var sw = new StringWriter();

            string users = String.Empty;
            string symptoms = String.Empty;
            string remedies = String.Empty;
            string appointments = String.Empty;

            try
            {
                using (var context = new DataModelContainer())
                {
                    #region Users Export

                    //header
                    sw.WriteLine(String.Format("{0};{1};{2};{3};{4};{5}", "User ID", "Registration Date", "Last Access Date", "OS", "OS Version", "Device Token"));

                    //data
                    foreach (var user in context.UserDataSet.AsEnumerable())
                    {
                        sw.WriteLine(String.Format("{0};{1};{2};{3};{4};{5}", user.ID, user.RegistrationDate, user.LastAccess, user.DeviceOS, user.DeviceOSVersion, user.DeviceToken));
                    }
                    //save
                    users = sw.ToString();
                    //clear
                    sw.GetStringBuilder().Clear();

                    #endregion

                    #region Symptoms Export

                    //header
                    sw.WriteLine(String.Format("{0};{1};{2};{3};{4};{5};{6};{7}", "Symptom ID", "User ID", "Symptom Date", "Regurgitation", "Crying", "Agitation", "Duration", "Intensity"));

                    //data
                    foreach (var symptom in context.SymptomSet.AsEnumerable())
                    {
                        sw.WriteLine(String.Format("{0};{1};{2};{3};{4};{5};{6};{7}", symptom.ID, symptom.UserDataID, symptom.When, symptom.Rigurgito, symptom.Pianto, symptom.Agitazione, symptom.Duration, symptom.Intensity));
                    }
                    //save
                    symptoms = sw.ToString();
                    //clear
                    sw.GetStringBuilder().Clear();

                    #endregion

                    #region Remedies Export

                    //header
                    sw.WriteLine(String.Format("{0};{1};{2};{3};{4};{5};{6}", "Remedy ID", "User ID", "Remedy Date", "Remedy Type", "Info", "Taken", "Asked"));

                    //data
                    foreach (var remedy in context.PillAlertSet.AsEnumerable())
                    {
                        sw.WriteLine(String.Format("{0};{1};{2};{3};{4};{5};{6}", remedy.ID, remedy.UserDataID, remedy.When, remedy.PillId, remedy.Info, remedy.Taken, remedy.Asked));
                    }
                    //save
                    remedies = sw.ToString();
                    //clear
                    sw.GetStringBuilder().Clear();

                    #endregion

                    #region Appointments Export

                    //header
                    sw.WriteLine(String.Format("{0};{1};{2};{3}", "Appointment ID", "User ID", "Appointment Date", "Info"));

                    //data
                    foreach (var appointment in context.AppointmentSet.AsEnumerable())
                    {
                        sw.WriteLine(String.Format("{0};{1};{2};{3}", appointment.ID, appointment.UserDataID, appointment.When, appointment.Info));
                    }
                    //save
                    appointments = sw.ToString();
                    //clear
                    sw.GetStringBuilder().Clear();

                    #endregion

                }

            }
            catch (Exception ex)
            {
                //TODO
            }

            ZipFile result = new ZipFile();

            result.AddEntry("Users.csv", users);
            result.AddEntry("Symptoms.csv", symptoms);
            result.AddEntry("Remedies.csv", remedies);
            result.AddEntry("Appointments.csv", appointments);
            
            MemoryStream workStream = new MemoryStream();
            result.Save(workStream);
            workStream.Seek(0, SeekOrigin.Begin);

            return File(workStream, System.Net.Mime.MediaTypeNames.Application.Zip, String.Format("Export {0}.zip", DateTime.Now));
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

        [HttpGet]
        public ActionResult Login()
        {
            return RedirectToAction("Login", "Home");
        }
    }
}