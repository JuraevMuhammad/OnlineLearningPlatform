using Application.DTOs.AnswerOption;
using Application.Responses;

namespace Application.Interfaces;

public interface IAnswerOptionService
{
    Task<Response<string>> CreateAnswerOption(CreateAnswerOption dto);
    Task<Response<string>> UpdateAnswerOption(int id, UpdateAnswerOption dto);
    Task<Response<string>> DeleteAnswerOption(int id);
}