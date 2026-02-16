using Application.DTOs.Exam;
using Application.Responses;

namespace Application.Interfaces;

public interface IExamService
{
    Response<List<GetExam>> GetExams();
    Response<GetExam> GetExam(int id);
    Task<Response<string>> CreateExam(CreateExam dto);
    Task<Response<string>> UpdateExam(int id, UpdateExam dto);
    Task<Response<string>> DeleteExam(int id);
}