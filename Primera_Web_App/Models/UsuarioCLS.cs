using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Primera_Web_App.Models
{
    public class UsuarioCLS
    {
            public int ID_USUARIO { get; set; }

            [Required(ErrorMessage = "El campo Nombre de usuario es requerido.")]
            public string USUARIO { get; set; }

            [Required(ErrorMessage = "El campo Contraseña es requerido.")]
            [DataType(DataType.Password)]
            public string PASSWORD { get; set; }

            public int HABILITADO { get; set; }
            public string TIPOUSUARIO { get; set; }

            public string NOMUSUARIO { get; set; }

            public string ConfirmarClave { get; set; }

            public String nombreUsuario { get; set; }
            public String contraseña { get; set; }


    }
}