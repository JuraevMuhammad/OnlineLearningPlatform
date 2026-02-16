using System.Net;
using Application.DTOs.Exam;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Services;

public class ExamService : IExamService
{
    private readonly ApplicationDbContext _context;
    
    public ExamService(ApplicationDbContext context)
    {
        _context = context;
    }

    #region GetExams

    public Response<List<GetExam>> GetExams()
    {
        var res =  _context.Exams.Where(x => !x.IsDeleted)
            .ToList().Select(x => new GetExam()
            {
                Id = x.Id,
                CourseId = x.CourseId,
                Title = x.Title,
                MaxScore = x.MaxScore,
                CreatedAt = x.CreatedAt,
            }).ToList();
        
        return new Response<List<GetExam>>(res);
    }

    #endregion

    #region GetExamById

    public Response<GetExam> GetExam(int id)
    {
        var exam = _context.Exams.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if(exam == null)
            return new Response<GetExam>(HttpStatusCode.NotFound, "not found");

        var getExam = new GetExam()
        {
            Id = exam.Id,
            CourseId = exam.CourseId,
            Title = exam.Title,
            MaxScore = exam.MaxScore,
            CreatedAt = exam.CreatedAt,
        };
        return new Response<GetExam>(getExam);
    }

    #endregion

    #region CreateExam

    public async Task<Response<string>> CreateExam(CreateExam dto)
    {
        var exam = new Exam()
        {
            CourseId = dto.CourseId,
            Title = dto.Title,
            MaxScore = dto.MaxScore,
        };
        
        await _context.Exams.AddAsync(exam);
        await _context.SaveChangesAsync();
        
        return new Response<string>(HttpStatusCode.Created, "Created");
    }

    #endregion

    #region UpdateExam

    public async Task<Response<string>> UpdateExam(int id, UpdateExam dto)
    {
        var exam = _context.Exams.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if(exam == null)
            return new Response<string>(HttpStatusCode.NotFound, "not found");
        
        exam.CourseId = dto.CourseId ?? exam.CourseId;
        exam.Title = dto.Title ?? exam.Title;
        exam.MaxScore = dto.MaxScore ?? exam.MaxScore;
        exam.UpdateAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "Updated");
    }

    #endregion

    #region DeleteExam

    public async Task<Response<string>> DeleteExam(int id)
    {
        var exam = _context.Exams.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (exam == null)
            return new Response<string>(HttpStatusCode.NotFound, "not found");

        exam.IsDeleted = true;
        await _context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "Deleted");
    }

    #endregion
}