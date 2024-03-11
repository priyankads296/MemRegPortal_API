using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MemRegPortal.Model
{
    public class JWTService
    {
        public string SecretKey { get; set; }
        public int TokenDuration { get; set; }
        private readonly IConfiguration config;

        public JWTService(IConfiguration _config)
        {
            config = _config;
            this.SecretKey = config.GetSection("jwtConfig").GetSection("Key").Value;
            this.TokenDuration = Int32.Parse(config.GetSection("jwtConfig").GetSection("Duration").Value);
        }

        public string GenerateToken(string UserID,string Firstname, string LastName,string Username,string PhoneNo)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var payload = new[]
            {
                new Claim("UserID",UserID),
                new Claim("Firstname",Firstname),
                new Claim("Lastname",LastName),
                new Claim("Username",Username),
                new Claim("PhoneNo",PhoneNo)
            };

            var jwtToken = new JwtSecurityToken(
                issuer:"localhost",
                audience:"localhost",
                claims:payload,
                expires:DateTime.Now.AddMinutes(TokenDuration),
                signingCredentials:signature
                );

            string token=new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;

        }
    }
}
