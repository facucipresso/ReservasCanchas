using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservasCanchas.BusinessLogic;
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

        private readonly AccountBusinessLogic _accountBusinessLogic;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService, AccountBusinessLogic accountBusinessLogic)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _accountBusinessLogic = accountBusinessLogic;
        }

        [HttpPost("register")]
        public async Task<ActionResult<NewUserDto>> Register([FromBody] RegisterDto registerDto)
        {// PASO 8 USO DE JWT hago todas las validaciones (PASO 9 USO DE JWT, antes de probar el endpoint tengo que hacer la migracion a la bbdd y el update de la misma)
            //(paso 10 en ITokenService)

            var userRegistered = await _accountBusinessLogic.RegisterAsync(registerDto);
            return Ok(userRegistered);

        }

        //PASO 15 ESO DE JWT, hacer el login (paso 16 en el program.cs)
        [HttpPost("login")]
        public async Task<ActionResult<NewUserDto>> Login([FromBody] LoginDto loginDto)
        {
            var userLoged = await _accountBusinessLogic.LoginAsync(loginDto);
            return Ok(userLoged);
        }

    }
}
