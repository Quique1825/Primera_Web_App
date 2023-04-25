using System;
using System.ComponentModel.DataAnnotations;

namespace Primera_Web_App.Models
{
    public class EmpleadoCLS
    {
            public int IDEMPLEADO { get; set; }
            public string NOMBRE { get; set; }
            public string APELLIDO { get; set; }
            [Display(Name = "Fecha Contrato")]
            public DateTime FECHACONTRATO { get; set; }
            public decimal SUELDO { get; set; }
            public int IDTIPOUSUARIO { get; set; }
            public int IDTIPOCONTRATO { get; set; }
            public int IDSEXO { get; set; }
            public int HABILITADO { get; set; }
            public int TIENEUSUARIO { get; set; }
            public string TIPOUSUARIO { get; set; }
        
    }
}