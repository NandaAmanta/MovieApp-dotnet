using Microsoft.AspNetCore.Mvc;
using MovieApp.Authentication.Requests;
using MovieApp.Dtos;
using MovieApp.Services;

namespace MovieApp.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthController(AuthService authService) : ControllerBase
{
    private readonly AuthService _authService = authService;

    [HttpPost("registration")]
    public async Task<IUser> Registration(RegistrationRequest request)
    {
        IUser user = await _authService.Register(request);
        return user;
    }

}