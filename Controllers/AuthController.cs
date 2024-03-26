using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Dtos;
using MovieApp.Dtos.ModelInterfaces;
using MovieApp.Requests.Authentication;
using MovieApp.Services;
using MovieApp.Utils;

namespace MovieApp.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthController(AuthService authService) : ControllerBase
{
    private readonly AuthService _authService = authService;

    [HttpPost("registration")]
    public async Task<ApiResponser<IUser>> Registration(RegistrationRequest request)
    {
        IUser user = await _authService.Register(request);
        return new ApiResponser<IUser>(HttpStatusCode.Created, "registration success", user);
    }

    [HttpPost("login")]
    public async Task<ApiResponser<Token>> Login(LoginRequest request)
    {
        Token token = await _authService.Login(request);
        return new ApiResponser<Token>(HttpStatusCode.Accepted, "Loggin success", token);
    }


}