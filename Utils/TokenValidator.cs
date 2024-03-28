
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Enums;

namespace MovieApp.Utils;

public class TokenValidator(IConfiguration config)
{
    private IConfiguration _config = config;

    public JwtSecurityToken validate(string token, TokenType type)
    {
        string issuer = _config["Jwt:Issuer"];
        string secret = type == TokenType.ACCESS ? _config["Jwt:AccessTokenKey"] : _config["Jwt:RefreshTokenKey"];
        var tokenValidatinParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.ValidateToken(token, tokenValidatinParameters, out SecurityToken validatedToken);
        
        return tokenHandler.ReadJwtToken(token);
    }


}