using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace IntPaymentAPI.Helpers
{
    public static class TokenHelper
    {
        // Secret key used to sign the JWT token
        private static readonly string SecretKey = "your_secret_key";  // Replace with a strong key
        private static readonly string Issuer = "your_issuer";
        private static readonly string Audience = "your_audience";

        // Method to generate JWT token for the user
        public static string GenerateToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Method to validate and parse the JWT token
        public static ClaimsPrincipal GetPrincipalFromToken(string tokenString)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(SecretKey);
                var parameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidIssuer = Issuer,
                    ValidateAudience = true,
                    ValidAudience = Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                var principal = tokenHandler.ValidateToken(tokenString, parameters, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    // You can handle token-specific validation here if needed
                    return principal;
                }
                else
                {
                    throw new Exception("Invalid token.");
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
