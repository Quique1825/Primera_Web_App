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
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public FileResult generarPDF()
        {
            Document doc = new Document();
            byte[] buffer;
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                Paragraph titulo = new Paragraph("Listado Empleados");
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                Paragraph ESPACIO = new Paragraph(" ");
                doc.Add(ESPACIO);
                PdfPTable tabla = new PdfPTable(4);
                float[] values = new float[4] { 30, 40, 40, 40 };
                tabla.SetWidths(values);

                PdfPCell celda1 = new PdfPCell(new Phrase("ID EMPLEADO"));
                celda1.BackgroundColor = new BaseColor(130, 130, 130);
                celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda1);

                PdfPCell celda2 = new PdfPCell(new Phrase("NOMBRE"));
                celda2.BackgroundColor = new BaseColor(130, 130, 130);
                celda2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda2);

                PdfPCell celda3 = new PdfPCell(new Phrase("APELLIDO"));
                celda3.BackgroundColor = new BaseColor(130, 130, 130);
                celda3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda3);

                PdfPCell celda4 = new PdfPCell(new Phrase("SUELDO"));
                celda4.BackgroundColor = new BaseColor(130, 130, 130);
                celda4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda4);

                List<EmpleadoCLS> lista = (List<EmpleadoCLS>)Session["lista"];
                int num_reg = lista.Count();
                for (int i = 0; i < num_reg; i++)
                {
                    tabla.AddCell(lista[i].IDEMPLEADO.ToString());
                    tabla.AddCell(lista[i].NOMBRE);
                    tabla.AddCell(lista[i].APELLIDO);
                    tabla.AddCell(lista[i].SUELDO.ToString());
                }
                doc.Add(tabla);
                doc.Close();
                buffer = ms.ToArray();
                return File(buffer, "application/pdf");
            }
        }

        public FileResult generarExel()
        {
            byte[] buffer;
            using (MemoryStream ms = new MemoryStream())
            {
                ExcelPackage ep = new ExcelPackage();
                ep.Workbook.Worksheets.Add("Reporte Empleado");
                ExcelWorksheet ew = ep.Workbook.Worksheets[1];
                ew.Cells[1, 1].Value = "ID Empleado";
                ew.Cells[1, 2].Value = "Nombre";
                ew.Cells[1, 3].Value = "Apellido";
                ew.Cells[1, 4].Value = "Fecha Contrato";
                ew.Cells[1, 5].Value = "Sueldo";


                ew.Column(1).Width = 20;
                ew.Column(2).Width = 60;
                ew.Column(3).Width = 60;
                ew.Column(4).Width = 60;
                ew.Column(5).Width = 60;


                using (var range = ew.Cells[1, 1, 1, 6])
                {
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkKhaki);
                }
                List<EmpleadoCLS> lista = (List<EmpleadoCLS>)Session["lista"];
                int nregistros = lista.Count;
                for (int i = 0; i < nregistros; i++)
                {
                    ew.Cells[i + 2, 1].Value = lista[i].IDEMPLEADO;
                    ew.Cells[i + 2, 2].Value = lista[i].NOMBRE;
                    ew.Cells[i + 2, 3].Value = lista[i].APELLIDO;
                    ew.Cells[i + 2, 4].Value = lista[i].FECHACONTRATO;
                    ew.Cells[i + 2, 5].Value = lista[i].SUELDO;
                }
                ep.SaveAs(ms);
                buffer = ms.ToArray();
            }
            return File(buffer, "application/vdn.ms-exel");
        }

        public ActionResult Index(EmpleadoCLS oEmpleadoCLS)
        {
            string apeEmp = oEmpleadoCLS.APELLIDO;
            List<EmpleadoCLS> lst = null;
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                using (var db = new P_W_A_Entities())
                {
                    if (oEmpleadoCLS.APELLIDO == null)
                    {
                        lst = (from empleado in db.Empleado
                               where empleado.HABILITADO == 1
                               select new EmpleadoCLS
                               {
                                   IDEMPLEADO = empleado.IDEMPLEADO,
                                   NOMBRE = empleado.NOMBRE,
                                   APELLIDO = empleado.APELLIDO,
                                   FECHACONTRATO = (DateTime)empleado.FECHACONTRATO,
                                   SUELDO = (decimal)empleado.SUELDO,
                                   IDSEXO = (int)empleado.IDSEXO,
                               }).ToList();
                        Session["lista"] = lst;
                    }
                    else
                    {
                        lst = (from empleado in db.Empleado
                               where empleado.HABILITADO == 1 && empleado.APELLIDO.Contains(apeEmp)
                               select new EmpleadoCLS
                               {
                                   IDEMPLEADO = empleado.IDEMPLEADO,
                                   NOMBRE = empleado.NOMBRE,
                                   APELLIDO = empleado.APELLIDO,
                                   FECHACONTRATO = (DateTime)empleado.FECHACONTRATO,
                                   SUELDO = (decimal)empleado.SUELDO,
                                   IDSEXO = (int)empleado.IDSEXO,
                               }).ToList();
                        Session["lista"] = lst;
                    }

                    return View(lst);
                }
            }
        }

        public ActionResult EmpEliminado(EmpleadoCLS oEmpleadoCLS)
        {
            string apeEmp = oEmpleadoCLS.APELLIDO;
            List<EmpleadoCLS> lst = null;
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                using (var db = new P_W_A_Entities())
                {
                    if (oEmpleadoCLS.APELLIDO == null)
                    {
                        lst = (from empleado in db.Empleado
                               where empleado.HABILITADO == 0
                               select new EmpleadoCLS
                               {
                                   IDEMPLEADO = empleado.IDEMPLEADO,
                                   NOMBRE = empleado.NOMBRE,
                                   APELLIDO = empleado.APELLIDO,
                                   FECHACONTRATO = (DateTime)empleado.FECHACONTRATO,
                                   SUELDO = (decimal)empleado.SUELDO,
                                   IDSEXO = (int)empleado.IDSEXO,
                               }).ToList();
                        Session["lista"] = lst;
                    }
                    else
                    {
                        lst = (from empleado in db.Empleado
                               where empleado.HABILITADO == 0 && empleado.APELLIDO.Contains(apeEmp)
                               select new EmpleadoCLS
                               {
                                   IDEMPLEADO = empleado.IDEMPLEADO,
                                   NOMBRE = empleado.NOMBRE,
                                   APELLIDO = empleado.APELLIDO,
                                   FECHACONTRATO = (DateTime)empleado.FECHACONTRATO,
                                   SUELDO = (decimal)empleado.SUELDO,
                                   IDSEXO = (int)empleado.IDSEXO,
                               }).ToList();
                        Session["lista"] = lst;
                    }

                    return View(lst);
                }
            }
        }

        List<SelectListItem> listaSexo;
        private void LlenarSexo()
        {
            using (var db = new P_W_A_Entities())
            {
                listaSexo = (from d in db.SEXO
                             select new SelectListItem
                             {
                                 Text = d.DESCRIPCION,
                                 Value = d.IDSEXO.ToString(),
                             }).ToList();
                listaSexo.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
            }

        }

        // GET: Empleado/Create
        public ActionResult Agregar()
        {
            LlenarSexo();
            ViewBag.lista = listaSexo;
            return View();
        }


        // POST: Empleado/Agregar
        [HttpPost]
        public ActionResult Agregar(EmpleadoCLS oEmpleadoCLS)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int cantReg = 0;
                string numdoc = oEmpleadoCLS.NUMDOCUMENTO;
                using (var db = new P_W_A_Entities())
                {
                    cantReg = db.Empleado.Where(e => e.NUMDOCUMENTO.Equals(numdoc)).Count();
                }
                if (cantReg >= 1)
                {
                    ModelState.AddModelError("DNI", "DNI YA EXISTENTE");
                   // oEmpleadoCLS.mensajeError = "¡¡ Ya Exite un Empleado con ese documento";
                    LlenarSexo();
                    ViewBag.lista = listaSexo;
                    return View(oEmpleadoCLS);
                }
                else
                {
                    using (var db = new P_W_A_Entities())
                    {
                        Empleado oEmpleado = new Empleado();
                        oEmpleado.NOMBRE = oEmpleadoCLS.NOMBRE;
                        oEmpleado.APELLIDO = oEmpleadoCLS.APELLIDO;
                        oEmpleado.FECHACONTRATO = oEmpleadoCLS.FECHACONTRATO;
                        oEmpleado.SUELDO = oEmpleadoCLS.SUELDO;
                        oEmpleado.IDSEXO = oEmpleadoCLS.IDSEXO;
                        oEmpleado.NUMDOCUMENTO = oEmpleadoCLS.NUMDOCUMENTO;
                        oEmpleado.HABILITADO = 1;
                        oEmpleado.IDTIPOUSUARIO = 1;
                        oEmpleado.IDTIPOCONTRATO = 1;
                        oEmpleado.TIPOUSUARIO = "A";
                        oEmpleado.TIENEUSUARIO = 1;
                        db.Empleado.Add(oEmpleado);
                        db.SaveChanges();

                    }
                    return RedirectToAction("Index");
                }
            }
        }

        // GET: Empleado/Edit
        [HttpGet]
        public ActionResult Editar(int id)
        {
            LlenarSexo();
            ViewBag.lista = listaSexo;
            EmpleadoCLS oEmpleadoCLS = new EmpleadoCLS();

            using (var db = new P_W_A_Entities())
            {
                Empleado oEmpleado = db.Empleado.Where(p => p.IDEMPLEADO.Equals(id)).First();
                oEmpleadoCLS.IDEMPLEADO = oEmpleado.IDEMPLEADO;
                oEmpleadoCLS.NOMBRE = oEmpleado.NOMBRE;
                oEmpleadoCLS.APELLIDO = oEmpleado.APELLIDO;
                oEmpleadoCLS.FECHACONTRATO = (DateTime)oEmpleado.FECHACONTRATO;
                oEmpleadoCLS.SUELDO = (decimal)oEmpleado.SUELDO;
                oEmpleadoCLS.IDSEXO = (int)oEmpleado.IDSEXO;
            }
            return View(oEmpleadoCLS);
        }

        // POST: Empleado/Edit
        [HttpPost]
        public ActionResult Editar(EmpleadoCLS oEmpleadoCLS)
        {
            int id = oEmpleadoCLS.IDEMPLEADO;
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    LlenarSexo();
                    ViewBag.lista = listaSexo;
                 //   return View(oEmpleadoCLS);
                }
            }
                using (var db = new P_W_A_Entities())
                {
                    Empleado oEmpleado = db.Empleado.Where(p => p.IDEMPLEADO.Equals(id)).First();
                    oEmpleado.NOMBRE = oEmpleadoCLS.NOMBRE;
                    oEmpleado.APELLIDO = oEmpleadoCLS.APELLIDO;
                    oEmpleado.FECHACONTRATO = oEmpleadoCLS.FECHACONTRATO;
                    oEmpleado.SUELDO = oEmpleadoCLS.SUELDO;
                    oEmpleado.IDSEXO = oEmpleadoCLS.IDSEXO;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
         }
    
        // GET: Empleado/Delete/5
        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                using (var db = new P_W_A_Entities())
                {
                    Empleado oEmpleado = db.Empleado.Where(p => p.IDEMPLEADO.Equals(id)).First();
                    oEmpleado.HABILITADO = 0;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}
