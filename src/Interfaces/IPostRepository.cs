using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catedra3.src.Models;

namespace catedra3.src.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> CreatePostAsync(Post post); // Método para crear un post

        Task<IEnumerable<Post>> GetAllPostsAsync(); // Obtener todos los posts

        Task<Post?> GetPostByIdAsync(int postId); // Obtener un post por ID

        Task<bool> IsPostOwnedByUserAsync(int postId, string userId); // Verificar si el post es propiedad del usuario

        Task DeletePostAsync(Post post); // Eliminar un post
    }
}