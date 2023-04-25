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
        [BindProperty]
        public InputModel Input {get; set;}

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            public int ID_USUARIO { get; set; }
            [Required]
            public string USER { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string PASSWORD { get; set; }

            public int HABILITADO { get; set; }
            public string TIPOUSUARIO { get; set; }

            public string ConfirmarClave { get; set; }
        }
    }
}