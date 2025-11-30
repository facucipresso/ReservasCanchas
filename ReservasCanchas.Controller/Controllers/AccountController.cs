using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservasCanchas.BusinessLogic.Dtos.Account;
using ReservasCanchas.BusinessLogic.Exceptions;
using ReservasCanchas.BusinessLogic.JWTService;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;

namespace ReservasCanchas.Controller.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // PASO 5 USO DE JWT HACER EL CONTROLADOR Y EN PRIMER ENDPOINT ES EL DE REGISTRO pero todavia no hago el metodo (paso 6 en RegisterDto)
        //busco el usuario, me permite hacer operaciones con los usuarios
        private readonly UserManager<User> _userManager;

        //PASO 14 USO DE JWT, agrego itokenservice para poder crear token y mandarlo en la respuesta (paso 15 aca hacer el login)
        private readonly TokenService _tokenService;

        //checkeo contraseña
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {// PASO 8 USO DE JWT hago todas las validaciones (PASO 9 USO DE JWT, antes de probar el endpoint tengo que hacer la migracion a la bbdd y el update de la misma)
            //(paso 10 en ITokenService)
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                //si esta bien ingresado el dto, creo un nuevo usuario
                var appUser = new User
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    Name = registerDto.Name,
                    LastName = registerDto.LastName,
                    PhoneNumber = registerDto.PhoneNumber
                };
                //aca lo sigo creando, le agrego la contraseña que ingreso pero hasheada
                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (createdUser.Succeeded)
                {
                    //si la contraseña fue bien, le asigno un rol
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "SuperAdmin"); // le cambio momentaneamente a que lo cargue con rol SuperAdmin para poder trabajar, sino aca va Usuario 
                    if (roleResult.Succeeded)
                    {
                        //esto es con NewUserDto creado y teniendo TokenService
                        return Ok(new NewUserDto
                        {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Token = await _tokenService.CreateToken(appUser)
                        });
                    }
                    else
                    {
                        throw new BadRequestException($"Error en el registro del usuario {appUser.UserName}, fallo en la asignacion del rol: {roleResult.Errors}");
                        //return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    throw new BadRequestException($"Error en el registro del usuario {appUser.UserName}: {createdUser.Errors}");
                    //return StatusCode(500, createdUser.Errors);
                }

            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Ocurrio un error en el proceso de registro del usuario: {ex}");
                //return StatusCode(500, ex);
            }

        }

        //PASO 15 ESO DE JWT, hacer el login (paso 16 en el program.cs)
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //busco el usuario en la base de datos con user manager que es el que me facilita el acceso y manejo de usuarios
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("Usuario Invalido");

            //checkea que la contraseña sea la correcta
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false); // ver bien el parametro 'false' de esta funcion
            if (!result.Succeeded) return Unauthorized("Username no encontrado o contaseña incorrecta");

            return Ok(new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user)
            });
        }

    }
}
