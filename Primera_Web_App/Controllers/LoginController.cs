using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Primera_Web_App.Models;

namespace Primera_Web_App.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        //GET: Registrar
        public ActionResult Registrar()
        {
            return View();
        }
    }
}