using Microsoft.EntityFrameworkCore;
using MovieApp.Authentication.Requests;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Dtos.ModelInterfaces;
using MovieApp.Enums;
using MovieApp.Models;
using MovieApp.Utils;

namespace MovieApp.Services;

public class AuthService(MovieAppDataContext context, TokenGenerator tokenGenerator)
{

    private readonly MovieAppDataContext _context = context;
    private readonly TokenGenerator tokenGenerator = tokenGenerator;

    public async Task<IUser> Register(RegistrationRequest request)
    {
        User user = new User();
        user.Email = request.Email;
        user.Name = request.Name;
        user.SetHashedPassword(request.Password);
        user.IsAdmin = false;
        user = _context.User.Add(user).Entity;
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<Token> Login(LoginRequest request)
    {
        User? user = await _context.User
        .Where(user => user.Email == request.Email)
        .FirstOrDefaultAsync();

        if (user == null || !user.CheckPassword(request.Password))
        {
            throw new BadHttpRequestException("Wrong Credentials");
        }

        string accessToken = tokenGenerator.generate(TokenType.ACCESS,user);
        string refreshToken = tokenGenerator.generate(TokenType.REFRESH,user);
        Token token = new(accessToken: accessToken, refreshToken: refreshToken);
        return token;

    }
}