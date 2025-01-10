using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catedra3.src.Data;
using catedra3.src.Interfaces;
using catedra3.src.Models;
using Microsoft.EntityFrameworkCore;

namespace catedra3.src.Repositories
{
    public class PostRepository: IPostRepository
    {
       private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<Post> CreatePostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts
                .Include(p => p.User) // Incluye información del usuario relacionado
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Post?> GetPostByIdAsync(int postId)
        {
            return await _context.Posts
                .Include(p => p.User) // Incluye información del usuario relacionado
                .FirstOrDefaultAsync(p => p.Id == postId);
        }

        /// <inheritdoc />
        public async Task<bool> IsPostOwnedByUserAsync(int postId, string userId)
        {
            return await _context.Posts.AnyAsync(p => p.Id == postId && p.UserId == userId);
        }

        /// <inheritdoc />
        public async Task DeletePostAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        } 
    }
}
