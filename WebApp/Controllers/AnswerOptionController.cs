using Application.DTOs.AnswerOption;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnswerOptionController(IAnswerOptionService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAnswerOption([FromBody] CreateAnswerOption dto)
    {
        var res = await service.CreateAnswerOption(dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAnswerOption(int id, [FromQuery] UpdateAnswerOption dto)
    {
        var res = await service.UpdateAnswerOption(id, dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAnswerOption(int id)
    {
        var res = await service.DeleteAnswerOption(id);
        return StatusCode(res.StatusCode, res);
    }
}