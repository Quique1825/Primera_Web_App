using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Primera_Web_App.Models
{
    public class RolCLS
    {
        public int IDROL { get; set;}
        [Display(Name = "Nombre del Rol")]
        [Required]
        [StringLength(50, ErrorMessage ="El largo maximo es de 50 Caracteres")]

        public string NOMBRE { get; set;}
        [Display(Name = "Descripcion del Rol")]
        [Required]
        [StringLength(50, ErrorMessage = "El largo maximo es de 50 Caracteres")]

        public string DESCRIPCION { get; set; }
        public int HABILITADO { get; set; }
        
        public string nombreFiltro { get; set; }
    }
}