using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Helpers;
    /// <summary>
    /// Helper to manage working with jwt tokens
    /// </summary>
    public class JwtHelper
    {
        private readonly IConfiguration _config;

        /// <summary>
        /// Get configuration to get access to static files and generate jwt.
        /// </summary>
        /// <param name="configuration">Configuration of current app</param>
        public JwtHelper(IConfiguration configuration)
        {
            _config = configuration;
        }

        /// <summary>
        /// Generates new jwt token for specified user
        /// </summary>
        /// <param name="user">User for which token is generated</param>
        /// <returns>Jwt token</returns>
        /// <exception cref="ArgumentNullException">Throws if one of parameters is null</exception>
        public virtual string GenerateJwtToken(AppUser user) 
        {
            if (user is null)
            {
                throw new ArgumentNullException("User is not valid");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:TokenValidityFromMinutes"]));

            var secToken = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: expires,
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(secToken);

            return token;
        }
    }
