using Application.DTOs.Question;
using Application.Responses;
using Domain.Entities;

namespace Application.Interfaces;

public interface IQuestionService
{
    Response<List<GetQuestion>> GetQuestionsByExamId(int examId);
    Task<Response<string>> CreateQuestion(CreateQuestion dto);
    Task<Response<string>> UpdateQuestion(int id, UpdateQuestion dto);
    Task<Response<string>> DeleteQuestion(int id);
}