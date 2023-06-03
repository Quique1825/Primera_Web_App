using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Primera_Web_App.Permisos;

namespace Primera_Web_App.Controllers
{
    public class HomeController : Controller
    {
        [ValidarSesion]

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Empleado()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Login");

        }

    
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Index();
        }
    }
}