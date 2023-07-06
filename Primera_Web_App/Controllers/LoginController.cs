using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Primera_Web_App.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

namespace Primera_Web_App.Controllers
{

    public class LoginController : Controller
    {

        // Acción para mostrar la vista de inicio de sesión
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsuarioCLS logUser)
        {
            if(ModelState.IsValid)
              {
                using (var db = new P_W_A_Entities())
                {
                    var usu = db.Usuarios.Where(us => us.USUARIO.Equals(logUser.USUARIO) && us.PASSWORD.Equals(logUser.PASSWORD)).FirstOrDefault();
                    if(usu != null)
                    {
                        Session["UserId"] = usu.ID_USUARIO.ToString();
                        Session["Username"] = usu.USUARIO.ToString();
                        Session["Saludo"] = usu.NOMUSUARIO.ToString();
                        return RedirectToAction("Index", "Empleado");                           
                    }
                }
            }
            return View(logUser);            
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}