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
        public ActionResult Login(UsuarioCLS logUser)
        {
            Boolean iniciar = false;
            try
            {
                using (var db = new P_W_A_Entities())
                {
                    Usuarios usuarios = (from us in db.Usuarios
                                         where us.USUARIO.ToUpper() == logUser.nombreUsuario.ToUpper()
                                         where us.PASSWORD.ToUpper() == logUser.contraseña.ToUpper()
                                         select new Usuarios
                                         {
                                             USUARIO = us.USUARIO
                                         }
                ).FirstOrDefault();
                    if(usuarios != null)
                    {
                        ViewBag.nombreUsuario = usuarios.USUARIO;
                        iniciar = true;
                    }
                }
            }
            catch
            {

            }

            if(!iniciar)
            {
                ViewBag.Error = "Usuario/Contraseña Incorrectos";
                return View(logUser);
            }
            else
            {
                return RedirectToAction("Listado", "Empleado");
            }
        }

    }
}