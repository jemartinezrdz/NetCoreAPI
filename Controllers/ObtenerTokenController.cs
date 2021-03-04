using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ApiExamen.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace ApiExamen.Controllers
{

    public class ObtenerTokenController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public ObtenerTokenController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Autenticacion con jwt para el login del front end y el funcionamiento de los metodos del api
        /// </summary>
        /// <param name="Recibo_DTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(200, Type = typeof(Usuario))]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Usuario accesos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (accesos.usuario == configuration["credenciales:user"] && accesos.contrasena == configuration["credenciales:pass"])
            {
                var _userInfo = AutenticarUsuario(accesos.usuario, accesos.contrasena);
                if (_userInfo != null)
                {
                    return Ok(new { token = GenerarTokenJWT(_userInfo) });
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Unauthorized();
            }

           
        }

        private TokenInfo AutenticarUsuario(string usuario, string password)
        {
            
            return new TokenInfo()
            {

                Nombre = "Eduardo",
                Apellidos = "Martinez",
                Email = "eddy.jisus@gmail.com",
                Telefono = "81 8692 2224",
                Rol = "Programador Analista"

            };

            
        }

        private string GenerarTokenJWT(TokenInfo tokenInfo)
        {
            
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["TokenJWT:ClaveSecreta"])
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

           
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("nombre", tokenInfo.Nombre),
                new Claim("apellidos", tokenInfo.Apellidos),
                new Claim("email", tokenInfo.Email),
                new Claim("telefono", tokenInfo.Telefono),
                new Claim("rol", tokenInfo.Rol)
            };

            
            var _Payload = new JwtPayload(
                    issuer: configuration["TokenJWT:Issuer"],
                    audience: configuration["TokenJWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    // Exipra a la 24 horas.
                    expires: DateTime.UtcNow.AddHours(1)
                );

            
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
    }
}