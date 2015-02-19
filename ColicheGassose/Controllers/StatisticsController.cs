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
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Statistics");
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("ShowStatistics", "Statistics");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(WebUser webuser)
        {
            if (ModelState.IsValid)
            {
                if (Validate(webuser))
                {
                    FormsAuthentication.SetAuthCookie(webuser.User, false);
                    return RedirectToAction("ShowStatistics", "Statistics");
                }
                else
                {
                    ModelState.AddModelError("Errors", "Dati di accesso errati!");
                }
            }
            return View(webuser);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult ShowStatistics()
        {
            return View();
        }

        private bool Validate(WebUser user)
        {
            return true;
        }
    }
}