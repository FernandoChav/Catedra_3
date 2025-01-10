using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catedra3.src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace catedra3.src.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            // Configurar Identity para tener claves primarias explícitas
            builder.Entity<IdentityUserLogin<string>>().HasKey(login => login.UserId);
            builder.Entity<IdentityUserRole<string>>().HasKey(userRole => new { userRole.UserId, userRole.RoleId });
            builder.Entity<IdentityUserClaim<string>>().HasKey(userClaim => userClaim.Id);
            builder.Entity<IdentityRoleClaim<string>>().HasKey(roleClaim => roleClaim.Id);
            builder.Entity<IdentityUserToken<string>>().HasKey(userToken => new { userToken.UserId, userToken.LoginProvider, userToken.Name });
            // Configuración explícita de la relación
        // Configuración de eliminación en cascada opcional
        }
    }

    
}