using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Application.Interfaces;   // Application'daki interface'i uyguluyor
using TodoApi.Domain.Entities;

namespace TodoApi.Infrastructure.Services
{
	public class TokenService : ITokenService   // ← interface'i implement et
	{
		private readonly IConfiguration _config;

		public TokenService(IConfiguration config)
		{
			_config = config;
		}

		public string CreateToken(User user)
		{
			// ... içerik aynı, değişmiyor ...
			var key = _config["Jwt:Key"]!;
			var issuer = _config["Jwt:Issuer"];
			var audience = _config["Jwt:Audience"];

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.Username)
			};

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: issuer,
				audience: audience,
				claims: claims,
				expires: DateTime.Now.AddHours(2),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}