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

    public Task<Response<string>> UpdateAnswerOption(int id, UpdateAnswerOption dto)
    {
        throw new NotImplementedException();
    }

    public Task<Response<string>> DeleteAnswerOption(int id)
    {
        throw new NotImplementedException();
    }
}