using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using catedra3.src.DTOs;
using catedra3.src.Interfaces;
using catedra3.src.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace catedra3.src.Controllers
{
    [ApiController]
    [Route("api/posts")]
    [Authorize] // Asegura que solo usuarios autenticados puedan acceder
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly UserManager<User> _userManager;

        public PostController(
            IPostRepository postRepository,
            ICloudinaryService cloudinaryService,
            UserManager<User> userManager)
        {
            _postRepository = postRepository;
            _cloudinaryService = cloudinaryService;
            _userManager = userManager;
        }

        // POST: api/Posts
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDto postDto)
        {
            // Validar que el correo no sea nulo o vacío
            if (string.IsNullOrEmpty(postDto.Email))
            {
                return BadRequest("El correo es obligatorio.");
            }

            // Buscar el usuario por email usando UserManager
            var user = await _userManager.FindByEmailAsync(postDto.Email);
            if (user == null)
            {
                return Unauthorized("Usuario no encontrado.");
            }

            // Subir la imagen utilizando CloudinaryService
            string imageUrl;
            try
            {
                imageUrl = await _cloudinaryService.UploadImageAsync(postDto.Image);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al subir la imagen: {ex.Message}");
            }

            // Crear el nuevo post
            var newPost = new Post
            {
                Title = postDto.Title,
                PublishedAt = DateTime.UtcNow,
                url = imageUrl, // URL de la imagen subida a Cloudinary
                UserId = user.Id,
                User = user
            };


            // Guardar el post en la base de datos usando el repository
            await _postRepository.CreatePostAsync(newPost);
            var postDtoResponse = new PostDTO
            {
                Title = newPost.Title,
                ImageUrl = newPost.url,  // Asignar el URL de la imagen
                PublishedAt = newPost.PublishedAt,
                UserEmail = user.Email  // Solo devolver el correo del usuario (no toda la entidad)
            };

            // Retornar el post creado
            return CreatedAtAction(nameof(GetPostById), new { id = newPost.Id }, postDtoResponse);
        }


        


        // GET: api/Posts
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            var postDtos = posts.Select(post => new PostDTO
            {
                Title = post.Title,
                ImageUrl = post.url,
                PublishedAt = post.PublishedAt,
                UserEmail = post.User.Email
            });

            return Ok(postDtos);
        }

        // GET: api/Posts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound(new { message = "Post no encontrado." });
            }

            var postDto = new PostDTO
            {
                Title = post.Title,
                ImageUrl = post.url,
                PublishedAt = post.PublishedAt,
                UserEmail = post.User.Email
            };

            return Ok(postDto);
        }

        // DELETE: api/Posts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var user = await GetAuthenticatedUserAsync();
            if (user == null)
            {
                return Unauthorized(new { message = "Usuario no autenticado o no encontrado." });
            }

            var isOwnedByUser = await _postRepository.IsPostOwnedByUserAsync(id, user.Id);

            if (!isOwnedByUser)
            {
                return Forbid("No tienes permiso para eliminar este post.");
            }

            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound(new { message = "Post no encontrado." });
            }

            await _postRepository.DeletePostAsync(post);

            return Ok(new { message = "Post eliminado exitosamente." });
        }

        // Método auxiliar para obtener el usuario autenticado usando el email del token
        private async Task<User> GetAuthenticatedUserAsync()
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value; // Extraer el email del token
            if (string.IsNullOrEmpty(email))
            {
                return null; // No hay email en el token
            }

            return await _userManager.FindByEmailAsync(email); // Buscar usuario por email
        }
    }
}
