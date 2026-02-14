using Application.DTOs.User;
using Application.Filters;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpGet("all")]
    public IActionResult GetAllUsers(FilterUser filter)
    {
        var res = service.GetUsers(filter);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var res = service.GetUser(id);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public async Task<IActionResult> CreatedUser(CreatedUser dto)
    {
        var res = await service.CreatedUser(dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(int id, UpdateUser dto)
    {
        var res = await service.UpdateUser(id, dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var res = await service.DeleteUser(id);
        return StatusCode(res.StatusCode, res);
    }
}