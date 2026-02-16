using Application.DTOs.Lesson;
using Application.Filters;
using Application.Responses;

namespace Application.Interfaces;

public interface ILessonService
{
    PaginationResponse<List<GetLesson>> GetPaginationLesson(FilterLesson filter);
    Response<GetLesson> GetLesson(int id);
    Task<Response<string>> CreateLesson(CreateLesson dto);
    Task<Response<string>> UpdateLesson(int id, UpdateLesson dto);
    Task<Response<string>> DeleteLesson(int id);
}