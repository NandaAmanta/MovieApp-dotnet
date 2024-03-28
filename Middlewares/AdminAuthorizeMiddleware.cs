
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MovieApp.Attributes;
using MovieApp.Data;
using MovieApp.Enums;
using MovieApp.Models;
using MovieApp.Utils;

namespace MovieApp.Middlewares
{
    internal class AdminAuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        public AdminAuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, MovieAppDataContext dataContext)
        {
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var attribute = endpoint?.Metadata.GetMetadata<AdminAuthorize>();
            if (attribute == null)
            {
                await _next(context);
                return;
            }
            
            string? rawToken = await context.GetTokenAsync("access_token") ?? context.Request.Headers["Authorization"];
            if (rawToken == null)
            {
                throw new UnauthorizedAccessException();
            }
            string token = rawToken.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var tokenValidator = new TokenValidator(configuration);
            JwtSecurityToken decodedToken = tokenValidator.validate(token, TokenType.ACCESS);
            string userEmail = decodedToken.Payload["email"].ToString();
            User user = await dataContext.User.Where(user => user.Email == userEmail).FirstAsync();
            if (!user.IsAdmin)
            {
                throw new UnauthorizedAccessException("This user is not admin");
            }

            await _next(context);
        }
    }

}