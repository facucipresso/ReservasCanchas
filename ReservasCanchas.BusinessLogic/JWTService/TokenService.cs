using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.JWTService
{
    public class TokenService
    {
        //me permite acceder al appSettings.json
        private readonly IConfiguration _config;


        private readonly SymmetricSecurityKey _key;

        private readonly UserManager<User> _userManager;

        public TokenService(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SignInKey"]));
            _userManager = userManager;
        }
        public async Task<string> CreateToken(User user)
        {
            /*
            //pares clave/valor que quiero yo que vayan en el token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // El rol principal
            }
            */

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            //que tipo de encriptado quiero
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //creo el token, creo el objeto de representacion del token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7), //ver que onda con esto DateTime.UtcNow por la base de datos
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            //hago el token handler, creo que seria nuestro creador de tokens
            var tokenHandler = new JwtSecurityTokenHandler();

            //aca me lo crea como objeto, yo lo necesito en string
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //aca lo devuelvo en formato string
            return tokenHandler.WriteToken(token);
        }
    }
}
