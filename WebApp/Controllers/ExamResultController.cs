using Application.DTOs.StudentExamResult;
using Application.Filters;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class ExamResultController(IExamResultService service) : ControllerBase
{
    [HttpGet("filter")]
    public IActionResult GetExamResults([FromQuery] FilterExamResult filter)
    {
        var res = service.GetFilterExamResult(filter);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("id")]
    public IActionResult GetExamResultById(int id)
    {
        var res = service.GetExamResult(id);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExamResult([FromQuery] CreateExamResult dto)
    {
        var res = await service.CreateExamResult(dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateExamResult(int id, [FromQuery] UpdateExamResult dto)
    {
        var res = await service.UpdateExamResult(id, dto);
        return StatusCode(res.StatusCode, res);
    }
}