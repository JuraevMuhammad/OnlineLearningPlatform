using Application.Filters;
using Application.Interfaces;
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
}