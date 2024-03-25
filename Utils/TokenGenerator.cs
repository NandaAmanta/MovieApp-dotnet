
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Dtos;
using MovieApp.Enums;
using MovieApp.Models;

namespace MovieApp.Utils;

public class TokenGenerator(IConfiguration config)
{


    private IConfiguration _config = config;
    public string generate(TokenType type, User user)
    {
        string? secret = null;
        DateTime? expires = null;
        string issuer = _config["Jwt:Issuer"];
        switch (type)
        {
            case TokenType.ACCESS:
                secret = _config["Jwt:AccessTokenKey"];
                expires = DateTime.Now.AddMinutes(120);
                break;
            case TokenType.REFRESH:
                secret = _config["Jwt:RefreshTokenKey"];
                expires = DateTime.Now.AddDays(1);
                break;
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new[]
             {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            Issuer = issuer,
            SigningCredentials = new SigningCredentials
             (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
             SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }
}