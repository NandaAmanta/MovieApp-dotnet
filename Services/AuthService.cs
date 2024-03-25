using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Authentication.Requests;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Models;

namespace MovieApp.Services;

public class AuthService(MovieAppDataContext context)
{

    private readonly MovieAppDataContext _context = context;

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

    public async Task<IUser?> Login(LoginRequest request)
    {
        User? user = await _context.User
        .Where(user => user.Email == request.Email && user.CheckPassword(request.Password))
        .FirstOrDefaultAsync();

        // to be continue hehe
        return user;
    }
}