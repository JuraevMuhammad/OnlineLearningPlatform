using Application.DTOs.Lesson;
using Application.Filters;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonController(ILessonService service) : ControllerBase
{
    [HttpGet("filter")]
    public IActionResult GetFilterLesson([FromQuery]FilterLesson filter)
    {
        var res = service.GetPaginationLesson(filter);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("id")]
    public IActionResult GetLessonById(int id)
    {
        var res = service.GetLesson(id);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<IActionResult> CreateLesson([FromQuery]CreateLesson dto)
    {
        var res = await service.CreateLesson(dto);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPut]
    public async Task<IActionResult> UpdateLesson(int id, [FromQuery] UpdateLesson dto)
    {
        var res = await service.UpdateLesson(id, dto);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Teacher")]
    [HttpDelete]
    public async Task<IActionResult> DeleteLesson(int id)
    {
        var res = await service.DeleteLesson(id);
        return StatusCode(res.StatusCode, res);
    }
}