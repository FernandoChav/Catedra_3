using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catedra3.src.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}