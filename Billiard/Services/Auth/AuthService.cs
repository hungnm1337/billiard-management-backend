using Billiard.DTO;
using Billiard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Billiard.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly Prn232ProjectContext _context;
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<Models.Account> _passwordHasher;

        public AuthService(Prn232ProjectContext context, IConfiguration config, IPasswordHasher<Models.Account> passwordHasher)
        {
            _context = context;
            _config = config;
            _passwordHasher = passwordHasher;
        }
        public async Task<(bool Success, string Token, string Message)> LoginAsync(LoginModel model)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(x => x.Username.Equals(model.Username));
            if (user == null)
                return (false, null, "Sai tên đăng nhập hoặc mật khẩu!");

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);
            if (result == PasswordVerificationResult.Failed)
                return (false, null, "Sai tên đăng nhập hoặc mật khẩu!");

            User ret = await _context.Users.FirstOrDefaultAsync(x => x.AccountId == user.AccountId);
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("AccountId", user.AccountId.ToString()),
            new Claim("UserId", ret.UserId.ToString()),
            new Claim("RoleId", ret.RoleId.ToString()),
            new Claim("Status", ret.Account.Status)

        };

            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["Expires"])),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return (true, tokenString, "Đăng nhập thành công!");
        }
    }
}
