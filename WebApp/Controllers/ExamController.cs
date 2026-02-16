using Application.DTOs.Exam;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamController(IExamService service) : ControllerBase
{
    [HttpGet("all")]
    public IActionResult GetAllExams()
    {
        var res = service.GetExams();
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("{id}")]
    public IActionResult GetExam(int id)
    {
        var res = service.GetExam(id);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<IActionResult> CreateExam([FromQuery] CreateExam dto)
    {
        var res = await service.CreateExam(dto);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPut]
    public async Task<IActionResult> UpdateExam(int id, [FromQuery] UpdateExam dto)
    {
        var res = await service.UpdateExam(id, dto);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Teacher")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExam(int id)
    {
        var res = await service.DeleteExam(id);
        return StatusCode(res.StatusCode, res);
    }
}