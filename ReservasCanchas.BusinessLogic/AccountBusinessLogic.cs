using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservasCanchas.BusinessLogic.Dtos.Account;
using ReservasCanchas.BusinessLogic.Exceptions;
using ReservasCanchas.BusinessLogic.JWTService;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic
{
    public class AccountBusinessLogic
    {

        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserRepository _userRepository;

        public AccountBusinessLogic(UserManager<User> userManager, TokenService tokenService, SignInManager<User> signInManager, UserRepository userRepository)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userRepository = userRepository;
        }

        public async Task<NewUserDto> RegisterAsync(RegisterDto registerDto)
        {
            var normalizedEmail = _userManager.NormalizeEmail(registerDto.Email);
            var existingEmail = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);
            if (await _userManager.FindByNameAsync(registerDto.Username) != null)
                throw new BadRequestException("El nombre de usuario ya está en uso.");
            if (existingEmail != null)
                throw new BadRequestException("El email ya está registrado.");
            if (await _userRepository.ExistByPhoneAsync(registerDto.PhoneNumber))
                throw new BadRequestException("El numero de telefono ya está registrado.");
            
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
                var roleResult = await _userManager.AddToRoleAsync(appUser, "Usuario");
                if (roleResult.Succeeded)
                {
                    //esto es con NewUserDto creado y teniendo TokenService
                    return (new NewUserDto
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = await _tokenService.CreateToken(appUser)
                    });
                }
                else
                {
                    throw new BadRequestException($"Error en el registro del usuario {appUser.UserName}, fallo en la asignacion del rol: {roleResult.Errors}");
                }
            }
            else
            {
                var identityErrors = string.Join(" | ", createdUser.Errors.Select(e => $"{e.Code}: {e.Description}"));
                Console.WriteLine(identityErrors);
                throw new BadRequestException($"Error en el registro del usuario {appUser.UserName}: {identityErrors}");
            }
        }

        public async Task<NewUserDto> LoginAsync(LoginDto loginDto)
        {
            //busco el usuario en la base de datos con user manager que es el que me facilita el acceso y manejo de usuarios
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) throw new BadRequestException($"Usuario no encontrado");

            //checkea que la contraseña sea la correcta
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false); // ver bien el parametro 'false' de esta funcion
            if (!result.Succeeded) throw new BadRequestException($"Contraseña inconrrecta");

            return (new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user)
            });
        }
    }


}
