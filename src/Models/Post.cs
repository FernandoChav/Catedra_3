using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catedra3.src.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime PublishedAt { get; set; }

        public string url { get; set; } = string.Empty;

        public string UserId { get; set; }

        public User User { get; set; } = null!;
    }
}