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
    [Authorize(Roles = "Student")]
    [HttpGet("all")]
    public IActionResult GetAllUsers([FromQuery]FilterUser filter)
    {
        var res = service.GetUsers(filter);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Student")]
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

    [Authorize(Roles = "Student")]
    [HttpPut]
    public async Task<IActionResult> UpdateUser(int id, UpdateUser dto)
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