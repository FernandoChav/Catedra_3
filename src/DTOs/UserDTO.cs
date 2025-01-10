using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using catedra3.src.Models;

namespace catedra3.src.DTOs
{
    public class UserDTO
    {

        [Required(ErrorMessage = "El campo Email es requerido")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo Password es requerido")]
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}