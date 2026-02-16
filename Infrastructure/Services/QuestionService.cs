using System.Net;
using Application.DTOs.Question;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Services;

public class QuestionService : IQuestionService
{
    private readonly ApplicationDbContext _context;

    public QuestionService(ApplicationDbContext context)
    {
        _context = context;
    }

    #region GetQuestionsByExamId

    public Response<List<GetQuestion>> GetQuestionsByExamId(int examId)
    {
        var res = _context.Questions.Where(q => q.ExamId == examId && !q.IsDeleted).ToList();
        if (res.Count == 0)
            return new Response<List<GetQuestion>>(HttpStatusCode.NotFound, "not found");

        var get = res.Select(x => new GetQuestion()
        {
            Id = x.Id,
            Text = x.Text,
            ExamId = x.ExamId,
            Type = x.Type,
            CreatedAt = x.CreatedAt,
        }).ToList();

        return new Response<List<GetQuestion>>(get);
    }

    #endregion

    #region CreateQuestion

    public async Task<Response<string>> CreateQuestion(CreateQuestion dto)
    {
        var create = new Question()
        {
            Text = dto.Text,
            ExamId = dto.ExamId,
            Type = dto.Type,
        };
        _context.Questions.Add(create);
        await _context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.Created, "created");
    }

    #endregion

    #region UpdateQuestion

    public async Task<Response<string>> UpdateQuestion(int id, UpdateQuestion dto)
    {
        var res = _context.Questions.FirstOrDefault(q => q.Id == id && !q.IsDeleted);
        if (res == null)
            return new Response<string>(HttpStatusCode.NotFound, "not found");
        
        res.Text = dto.Text ?? res.Text;
        res.Type = dto.Type ?? res.Type;
        res.ExamId = dto.ExamId ?? res.ExamId;
        res.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "updated");
    }

    #endregion

    #region DeleteQuestion

    public async Task<Response<string>> DeleteQuestion(int id)
    {
        var res = _context.Questions.Find(id);
        if (res == null)
            return new Response<string>(HttpStatusCode.NotFound, "not found");

        res.IsDeleted = true;
        await _context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "deleted");
    }

    #endregion
}