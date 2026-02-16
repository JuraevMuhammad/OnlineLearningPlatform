using Application.DTOs.Course;
using Application.Filters;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController(ICourseService service) : ControllerBase
{
    [HttpGet("filter")]
    public IActionResult GetPaginationCourses([FromQuery]FilterCourse filter)
    {
        var res = service.GetPaginationCourses(filter);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("{id}")]
    public IActionResult GetCourse([FromRoute] int id)
    {
        var res = service.GetCourse(id);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromQuery] CreateCourse course)
    {
        var res = await service.CreateCourse(course);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPut]
    public async Task<IActionResult> UpdateCourse(int id, [FromQuery] UpdateCourse dto)
    {
        var res = await service.UpdateCourse(id, dto);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Teacher")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourseById(int id)
    {
        var res = await service.DeleteCourse(id);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("id/lessons")]
    public IActionResult GetCourseWithLessons(int id)
    {
        var res = service.GetCourseWithLesson(id);
        return StatusCode(res.StatusCode, res);
    }
}