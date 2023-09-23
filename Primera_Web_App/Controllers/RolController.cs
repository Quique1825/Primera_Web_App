using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using Primera_Web_App.Models;


namespace Primera_Web_App.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Index()
        {
            List<RolCLS> listaRol = new List<RolCLS>();
            using (var db = new P_W_A_Entities())
            {
                listaRol = (from rol in db.Rol
                            select new RolCLS
                            {
                                IDROL = rol.IDROL,
                                NOMBRE = rol.NOMBRE,
                                DESCRIPCION = rol.DESCRIPCION,
                                HABILITADO = (int)rol.HABILITADO
                            }).ToList();
            }
            return View(listaRol);
        }

        // GET: Rol/Details/5
        public ActionResult Filtrar(RolCLS oRolCLS)
        {
            string nombreRol = oRolCLS.nombreFiltro;
            List<RolCLS> listaRol = new List<RolCLS>();
            using (var db = new P_W_A_Entities())
            {
                if (nombreRol == null)
                {
                    listaRol = (from rol in db.Rol
                                select new RolCLS
                                {
                                    IDROL = rol.IDROL,
                                    NOMBRE = rol.NOMBRE,
                                    DESCRIPCION = rol.DESCRIPCION,
                                    HABILITADO = (int)rol.HABILITADO
                                }).ToList();
                }
                else
                {
                    listaRol = (from rol in db.Rol
                                where rol.NOMBRE.Contains(nombreRol)
                                select new RolCLS
                                {
                                    IDROL = rol.IDROL,
                                    NOMBRE = rol.NOMBRE,
                                    DESCRIPCION = rol.DESCRIPCION
                                }).ToList();
                }
                return PartialView("_TablaRol", listaRol);
            }

        }


        public string Guardar2(RolCLS oRolCLS, int titulo)
        {
            string rpta = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();
                    rpta += "<ul class = 'list-group'";
                    foreach (var item in query)
                    {
                        rpta += "<li class='list-group-item'>" + item + "</li";
                    }
                    rpta += "'</ul'";
                }
                else
                {
                    using (var db = new P_W_A_Entities())
                    {
                       if (titulo == -1)
                       {
                            Rol oRol = new Rol();
                            oRol.IDROL = oRolCLS.IDROL;
                            oRol.NOMBRE = oRolCLS.NOMBRE;
                            oRol.DESCRIPCION = oRolCLS.DESCRIPCION;
                            oRol.HABILITADO = 1;
                            db.Rol.Add(oRol);
                            rpta = db.SaveChanges().ToString();
                            if (rpta == "0") rpta = "";
                       }else {
                            Rol oRol = db.Rol.Where(p => p.IDROL == titulo).First();
                            oRol.NOMBRE = oRolCLS.NOMBRE;
                            oRol.DESCRIPCION = oRolCLS.DESCRIPCION;
                            rpta = db.SaveChanges().ToString();
                       }
                    }
                }
            }
             catch (Exception ex)
            {
              rpta = "";
            }
            return rpta;
        }

        public string Eliminar(RolCLS oRolCLS)
        {
            string rpta = "";
            try
            {
                int id = oRolCLS.IDROL;
                using (var db = new P_W_A_Entities())
                {
                    Rol oRol = db.Rol.Where(p => p.IDROL == id).First();
                    db.Rol.Remove(oRol);
                    rpta = db.SaveChanges().ToString();                     
                }
            }
            catch (Exception)
            {
                rpta = "";
            }
            return rpta;
        }

        public JsonResult rellenarCampos(int titulo)
        {
            RolCLS oRolCLS = new RolCLS();
            using (var db = new P_W_A_Entities())
            {
                Rol oRol = db.Rol.Where(p => p.IDROL == titulo).First();
                oRolCLS.NOMBRE = oRol.NOMBRE;
                oRolCLS.DESCRIPCION = oRol.DESCRIPCION;
                oRolCLS.HABILITADO = 1;
            }
            return Json(oRolCLS, JsonRequestBehavior.AllowGet);
        }

    }
}
