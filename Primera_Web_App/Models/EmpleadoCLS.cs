using System;
using System.ComponentModel.DataAnnotations;

namespace Primera_Web_App.Models
{
    public class EmpleadoCLS
    {
            public int IDEMPLEADO { get; set; }
           
            [Display(Name = "Nombre")]
            [Required]
            [StringLength(100, ErrorMessage = " El nombre no debe contener mas de 100 caracteres")]
            public string NOMBRE { get; set; }
           
            [Display(Name = "Apellido")]
            [Required]
            public string APELLIDO { get; set; }
            
            [Required]
            [Display(Name = "Fecha de Contrato")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> FECHACONTRATO { get; set; }
            
            [Required]
            public Nullable<decimal> SUELDO { get; set; }

            [Display(Name = "Tipo de Usuario")]
            public Nullable<int> IDTIPOUSUARIO { get; set; }

            [Display(Name = "Tipo de Contrato")]
            public Nullable<int> IDTIPOCONTRATO { get; set; }

            [Display(Name = "Sexo")]
            public Nullable<int> IDSEXO { get; set; }

            public Nullable<int> HABILITADO { get; set; }

            public Nullable<int> TIENEUSUARIO { get; set; }

            [Display(Name = "Usuario Tipo")]
            public string TIPOUSUARIO { get; set; }

          

            public virtual SEXO Sexo { get; set; }

    }
}