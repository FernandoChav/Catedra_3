using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catedra3.src.Models;
using Microsoft.EntityFrameworkCore;

namespace catedra3.src.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración explícita de la relación
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User) // Un Post tiene un User
            .WithMany(u => u.Posts) // Un User tiene muchos Posts
            .HasForeignKey(p => p.UserId) // FK explícita
            .OnDelete(DeleteBehavior.Cascade); // Configuración de eliminación en cascada opcional
    }
    }

    
}