using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace catedra3.src.DTOs
{
    public class Register
    {
        [Required(ErrorMessage = "El campo Email es requerido")]
        [EmailAddress(ErrorMessage = "El campo Email debe ser una dirección de correo electrónico válida")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo Password es requerido")]
        [MinLength(6, ErrorMessage = "El campo Password debe tener al menos 6 caracteres")]
        [RegularExpression(@"^(?=.*\d).+$", ErrorMessage = "La contraseña debe contener al menos un número.")]
        public string Password { get; set; } = string.Empty;
    }
}