using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace catedra3.src.Models
{
    public class User : IdentityUser
    {
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}