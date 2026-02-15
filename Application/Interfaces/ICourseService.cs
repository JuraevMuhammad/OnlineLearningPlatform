using Application.DTOs.Course;
using Application.Filters;
using Application.Responses;

namespace Application.Interfaces;

public interface ICourseService
{
    PaginationResponse<List<GetCourse>> GetPaginationCourses(FilterCourse filter);
    Response<GetCourse> GetCourse(int id);
    Task<Response<string>> CreateCourse(CreateCourse course);
    Task<Response<string>> UpdateCourse(int id, UpdateCourse dto);
    Task<Response<string>> DeleteCourse(int id);
    Response<GetCourse> GetCourseWithLesson(int id);
}