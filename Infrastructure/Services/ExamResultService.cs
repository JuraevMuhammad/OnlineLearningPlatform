using System.Net;
using Application.DTOs.StudentExamResult;
using Application.Filters;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ExamResultService : IExamResultService
{
    private readonly ApplicationDbContext _context;

    public ExamResultService(ApplicationDbContext context)
    {
        _context = context;
    }

    #region GetFilterExamResult

    public PaginationResponse<List<GetExamResult>> GetFilterExamResult(FilterExamResult filter)
    {
        var result = _context.StudentExamResults.AsQueryable();

        if (filter.ExamId != null)
            result = result.Where(x => x.ExamId == filter.ExamId);
        if (filter.StudentId != null)
            result = result.Where(x => x.StudentId == filter.StudentId);
        if (filter.Passed != null)
            result = result.Where(x => x.Passed == filter.Passed);
        
        var totalRecords = result.Count();
        var get = result.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).ToList().Select(x => new GetExamResult()
            {
                Id = x.Id,
                StudentId = x.StudentId,
                Passed = x.Passed,
                ExamId = x.ExamId,
                Score = x.Score,
                CreatedAt = x.CreatedAt,
            }).ToList();

        return new PaginationResponse<List<GetExamResult>>(filter.PageNumber, filter.PageSize, totalRecords, get);
    }

    #endregion

    #region GetExamResult

    public Response<GetExamResult> GetExamResult(int id)
    {
        var result = _context.StudentExamResults.Find(id);
        if (result == null)
            return new Response<GetExamResult>(HttpStatusCode.NotFound, "Exam Result Not Found");

        var get = new GetExamResult()
        {
            Id = result.Id,
            StudentId = result.StudentId,
            Passed = result.Passed,
            CreatedAt = result.CreatedAt,
            ExamId = result.ExamId,
            Score = result.Score,
        };
        
        return new Response<GetExamResult>(get);
    }

    #endregion

    #region CreateExamResult

    public async Task<Response<string>> CreateExamResult(CreateExamResult dto)
    {
        var create = new StudentExamResult()
        {
            StudentId = dto.StudentId,
            Passed = dto.Passed,
            ExamId = dto.ExamId,
            Score = dto.Score,
        };
        _context.StudentExamResults.Add(create);
        await _context.SaveChangesAsync();
        
        return new Response<string>(HttpStatusCode.Created, "Created Exam Result");
    }

    #endregion

    #region UpdateExamResult

    public async Task<Response<string>> UpdateExamResult(int id, UpdateExamResult dto)
    {
        var res = _context.StudentExamResults.Find(id);
        if (res == null)
            return new Response<string>(HttpStatusCode.NotFound, "not found");
        
        res.ExamId = dto.ExamId ?? res.ExamId;
        res.StudentId = dto.StudentId ?? res.StudentId;
        res.Score = dto.Score ?? res.Score;
        res.Passed = dto.Passed ?? res.Passed;
        res.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "Updated Exam Result");
    }

    #endregion
}