using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace catedra3.src.DTOs
{
    public class CreatePostDto
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        [MinLength(5, ErrorMessage = "El título debe tener al menos 5 caracteres.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "La imagen es obligatoria.")]
        public IFormFile Image { get; set; } = null!;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no es válido.")]
        public string Email { get; set; } = string.Empty;
    }
}