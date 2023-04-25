using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Primera_Web_App.Models;

namespace Primera_Web_App.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index()
        {
           List<EmpleadoCLS> lst = null;
            using (P_W_A_Entities db = new P_W_A_Entities())
                {
                    lst = (from empleado in db.Empleado
                           select new EmpleadoCLS
                           {
                               IDEMPLEADO = empleado.IDEMPLEADO,
                               NOMBRE = empleado.NOMBRE,
                               APELLIDO = empleado.APELLIDO,
                               FECHACONTRATO = (DateTime)empleado.FECHACONTRATO,
                               SUELDO = (decimal)empleado.SUELDO,
                               IDTIPOUSUARIO = (int)empleado.IDTIPOUSUARIO,
                               IDTIPOCONTRATO = (int)empleado.IDTIPOCONTRATO,
                               IDSEXO = (int)empleado.IDSEXO,
                               HABILITADO = (int)empleado.HABILITADO,
                               TIENEUSUARIO = (int)empleado.TIENEUSUARIO,
                               TIPOUSUARIO = empleado.TIPOUSUARIO
                           }).ToList();
                }
                return View(lst);
            }
            
        // GET: Empleado/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Empleado/Create
        public ActionResult Agregar()
        {
            llenarSexo();
            ViewBag.lista = listaSexo;
            return View();
        }

        // POST: Empleado/Create
        [HttpPost]
        public ActionResult Agregar(EmpleadoCLS oEmpleadoCLS)
        {
            if (!ModelState.IsValid)
            {
                return View("oEmpleadoCLS");
            }
            else
            {
                using(var db = new P_W_A_Entities())
                {
                    Empleado oEmpleado = new Empleado();
                    oEmpleado.NOMBRE = oEmpleadoCLS.NOMBRE;
                    oEmpleado.APELLIDO = oEmpleadoCLS.APELLIDO;
                    oEmpleado.FECHACONTRATO = oEmpleadoCLS.FECHACONTRATO;
                    oEmpleado.SUELDO = oEmpleadoCLS.SUELDO;
                    oEmpleado.IDSEXO = oEmpleadoCLS.IDSEXO;
                    oEmpleado.HABILITADO = 1;
                    db.Empleado.Add(oEmpleado);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Empleado/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Empleado/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Empleado/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
