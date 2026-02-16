using Application.DTOs.Question;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController(IQuestionService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetQuestions(int  examId)
    {
        var res = service.GetQuestionsByExamId(examId);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuestion(CreateQuestion dto)
    {
        var res = await service.CreateQuestion(dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateQuestion(int id, UpdateQuestion dto)
    {
        var res = await service.UpdateQuestion(id, dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        var res = await service.DeleteQuestion(id);
        return StatusCode(res.StatusCode, res);
    }
}