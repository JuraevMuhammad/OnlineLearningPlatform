using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    
}