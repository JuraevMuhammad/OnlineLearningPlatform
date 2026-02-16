using System.Net;
using Application.DTOs.AnswerOption;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Services;

public class AnswerOptionService : IAnswerOptionService
{
    private readonly ApplicationDbContext _context;

    public AnswerOptionService(ApplicationDbContext context)
    {
        _context = context;
    }

    #region CreateAnswerOption

    public async Task<Response<string>> CreateAnswerOption(CreateAnswerOption dto)
    {
        var create = new AnswerOption()
        {
            QuestionId = dto.QuestionId,
            Text = dto.Text,
            IsCorrect = dto.IsCorrect
        };
        
        _context.AnswerOptions.Add(create);
        await _context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.Created, "created");
    }

    #endregion

    #region UpdateAnswerOption

    public async Task<Response<string>> UpdateAnswerOption(int id, UpdateAnswerOption dto)
    {
        var res = _context.AnswerOptions.Find(id);
        if (res == null)
            return new Response<string>(HttpStatusCode.NotFound, "not found");
        
        res.QuestionId = dto.QuestionId ?? res.QuestionId;
        res.Text = dto.Text ??  res.Text;
        res.IsCorrect = dto.IsCorrect ?? res.IsCorrect;
        res.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "updated");
    }

    #endregion

    #region DeleteAnswerOption

    public async Task<Response<string>> DeleteAnswerOption(int id)
    {
        var res = _context.AnswerOptions.Find(id);
        if (res == null)
            return new Response<string>(HttpStatusCode.NotFound, "not found");

        res.IsDeleted = true;
        await _context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "deleted");
    }

    #endregion
}