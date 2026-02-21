using Application.DTOs.User;
using Application.Filters;
using Application.Interfaces;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [Authorize(Roles = nameof(UserRole.Student) + "," + nameof(UserRole.Teacher))]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsers([FromQuery]FilterUser filter)
    {
        var res = await service.GetUsers(filter);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = nameof(UserRole.Student) + "," + nameof(UserRole.Teacher))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var res = await service.GetUser(id);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Teacher))]
    [HttpPost("create")]
    public async Task<IActionResult> CreatedUser(CreatedUser dto)
    {
        var res = await service.CreatedUser(dto);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Teacher))]
    [HttpPut]
    public async Task<IActionResult> UpdateUser(int id,[FromQuery] UpdateUser dto)
    {
        var res = await service.UpdateUser(id, dto);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var res = await service.DeleteUser(id);
        return StatusCode(res.StatusCode, res);
    }
}