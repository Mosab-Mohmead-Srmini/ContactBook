using Application.ContactBook.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.ContactBook.Identity
{
    public class GenerateToken : IGenerateToken
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationDbContext _dbContext;

        public GenerateToken(IConfiguration configuration, IApplicationDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }
        public async Task<string> GenerateTokenAsync(int userId)
        {
            if (userId == -1)
            {
                throw new InvalidOperationException("User Not Found...");
            }
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new InvalidOperationException("User Not Found...");
            }
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                _configuration["Authentication:SecretKey"]!));

            var claims = new List<Claim>();
            // Add claims here (if any)
            claims.Add(new Claim("family_name", user.LastName));
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim("sub", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, user.UserType.ToString()));

            var signingCred = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var utcNow = DateTime.UtcNow;

            var secureToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                utcNow,
                utcNow.AddHours(10),
                signingCred
            );

            var finalToken = new JwtSecurityTokenHandler().WriteToken(secureToken);

            return finalToken;
        }
    }
}
