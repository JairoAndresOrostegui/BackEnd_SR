using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CESDE.DataAdapter
{
    public static class ConfigureJWT
    {
        public static string GetToken(long id_usuario, IConfiguration config)
        {
            string SecretKey = config["Jwt:SecretKey"];
            string Issuer = config["Jwt:Issuer"];
            string Audience = config["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                      new Claim(JwtRegisteredClaimNames.Sub, id_usuario.ToString()),
                      new Claim("id_usuario", id_usuario.ToString())
                  };

            var token = new JwtSecurityToken(
                  issuer: Issuer,
                  audience: Audience,
                  claims,
                  expires: DateTime.Now.AddHours(10),
                  signingCredentials: credentials
                  );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static int GetTokenIdUsuario(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                foreach (var claim in claims)
                {
                    if (claim.Type == "id_usuario")
                    {
                        return int.Parse(claim.Value);
                    }
                }
            }
            return 0;
        }


        public static void AddConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor().AddAuthorization()
                  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuer = true,
                          ValidateAudience = true,
                          ValidateLifetime = true,
                          ValidateIssuerSigningKey = true,
                          ValidIssuer = configuration["Jwt:Issuer"],
                          ValidAudience = configuration["Jwt:Audience"],
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                          ClockSkew = TimeSpan.Zero
                      };
                  });
        }
    }
}