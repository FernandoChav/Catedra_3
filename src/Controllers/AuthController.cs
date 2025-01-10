using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catedra3.src.DTOs;
using catedra3.src.Interfaces;
using catedra3.src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace catedra3.src.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;  // Inyecta UserManager
        private readonly SignInManager<User> _signInManager;  // Inyecta SignInManager

        private readonly IUserRepository _userRepository;

        public AuthController(IJwtService jwtService, UserManager<User> userManager, SignInManager<User> signInManager, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login loginRequest)
        {
            // Buscar el usuario por correo electrónico
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);

            if (user == null)
            {
                Console.WriteLine($"Usuario no encontrado: {loginRequest.Email}"); // Log para verificar que el usuario no fue encontrado
                return Unauthorized("Credenciales inválidas: El usuario no existe.");
            }

            // Verificar la contraseña
            var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, false, false);

            if (!result.Succeeded)

            {
                Console.WriteLine($"Contraseña incorrecta: {loginRequest.Email}"); // Log para verificar que la contraseña es incorrecta
                return Unauthorized("Credenciales inválidas: Contraseña incorrecta.");
            }


            // Verificar la contraseña
            

            // Generar el JWT
            var token = _jwtService.GenerateJwtToken(loginRequest.Email);

            // Retornar el token generado
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register registerRequest)
        {
            // Verificar si ya existe un usuario con el mismo correo
            var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email);
            if (existingUser != null)
            {
                return Conflict("El correo electrónico ya está en uso.");
            }

            // Crear el nuevo usuario
            var newUser = new User
            {
                UserName = registerRequest.Email,  // Puede ser necesario asignar un nombre de usuario
                Email = registerRequest.Email
            };

            // Crear el usuario con la contraseña, el cual es automáticamente encriptado
            var createResult = await _userManager.CreateAsync(newUser, registerRequest.Password);

            if (!createResult.Succeeded)
            {
                return BadRequest("Error al registrar el usuario.");
            }
            else
            {
                Console.WriteLine($"Usuario creado: {newUser.Email}"); // Log para verificar que el usuario fue creado correctamente
            }

            return Ok(new { message = "Usuario registrado exitosamente." });
        }
    }
}
