using BlazorFormsAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorFormsAPI.Utils.JWT
{
    public static class JwtHandler
    {
        public static string GenerateJwtToken(User user, string jwtKey)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("roles", user.Rol),
                new Claim("SesionId", Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var encodedKey = new SigningCredentials(key, SecurityAlgorithms.HmacSha384Signature);

            var token = new JwtSecurityToken(
                issuer: "BlazorForms",
                audience: "BlazorForms",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: encodedKey
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
