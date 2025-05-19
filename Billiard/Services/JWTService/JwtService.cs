using Billiard.Handlers;
using Billiard.Models;
using Billiard.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Billiard.Services.JWTService
{
    public class JwtService
    {
        private readonly Prn232ProjectContext _projectContext;
        private readonly IConfiguration _configuration;
        public JwtService(Prn232ProjectContext projectContext, IConfiguration configuration) {
            this._projectContext = projectContext;
            this._configuration = configuration;
        }

        public async Task<LoginResponceModel?> Authenticate(LoginModel request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password)) return null;

            var user = _projectContext.Accounts.FirstOrDefault(x => x.Users.Equals(request.Username));
            if (user == null || !EncriptPassword.VerifyPassword(request.Password,user.Password)) return null;

            var issuer = _configuration["Jwtconfig : Issuer"];
            var audience = _configuration["Jwtconfig : Audience"];
            var key = _configuration["Jwtconfig : Key"];
            var token = _configuration.GetValue<int>("Jwtconfig : Token");
            var tokenTimeStamp = DateTime.UtcNow.AddMinutes(token);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, request.Username),
                }),

                Expires = tokenTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescription);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new LoginResponceModel
            {
                AccessToken = accessToken,
                Username = request.Username,
                ExpiresIn = (int)tokenTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
            };
        }

    }
}
