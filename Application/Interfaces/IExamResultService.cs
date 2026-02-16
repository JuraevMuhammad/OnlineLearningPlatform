using Application.DTOs.Exam;
using Application.DTOs.StudentExamResult;
using Application.Filters;
using Application.Responses;

namespace Application.Interfaces;

public interface IExamResultService
{
    PaginationResponse<List<GetExamResult>> GetFilterExamResult(FilterExamResult filter);
    Response<GetExamResult> GetExamResult(int id);
    Task<Response<string>> CreateExamResult(CreateExamResult dto);
    Task<Response<string>> UpdateExamResult(int id, UpdateExamResult dto);
}