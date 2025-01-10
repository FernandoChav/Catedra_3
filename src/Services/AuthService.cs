using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using catedra3.src.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace catedra3.src.Services
{
    public class AuthService : IJwtService
    {
        private readonly string _secretKey;

        public AuthService(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:SecretKey"];
        }
        public string GenerateJwtToken(string email)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name, email)
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:5001",  // Valor de 'Issuer' configurado
                audience: "http://localhost:5164",  // Valor de 'Audience' configurado
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}