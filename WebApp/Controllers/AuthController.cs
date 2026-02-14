using Application.DTOs.Auth;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService service) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(Register dto)
    {
        var res = await service.Register(dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPost("login")]
    public IActionResult Login(Login dto)
    {
        var res = service.Login(dto);
        return StatusCode(res.StatusCode, res);
    }
}