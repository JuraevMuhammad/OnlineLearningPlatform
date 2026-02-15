using System.Net;
using Application.DTOs.Course;
using Application.DTOs.Lesson;
using Application.Filters;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourseService : ICourseService
{
    #region Constructor

    private readonly ApplicationDbContext _context;

    public CourseService(ApplicationDbContext context)
    {
        _context = context;
    }

    #endregion

    #region GetPaginationCourses

    public PaginationResponse<List<GetCourse>> GetPaginationCourses(FilterCourse filter)
    {
        var courses = _context.Courses.AsQueryable();
        if(courses.Count() == 0) 
            return new PaginationResponse<List<GetCourse>>(HttpStatusCode.NotFound, "not found");
        
        if(!string.IsNullOrEmpty(filter.Title))
            courses = courses.Where(c => c.Title.ToLower().Contains(filter.Title.ToLower()));
        
        if(filter.Level != null)
            courses = courses.Where(c => c.Level == filter.Level);
        
        var totalRecords = courses.Count();
        var result = courses.Where(x => !x.IsDeleted)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).ToList().Select(x => new GetCourse()
            {
                Id = x.Id,
                TeacherId = x.TeacherId,
                Title = x.Title,
                Description = x.Description,
                Level = x.Level,
                Price = x.Price,
                CreatedAt = x.CreatedAt,
            }).ToList();
        
        return new PaginationResponse<List<GetCourse>>(filter.PageNumber, filter.PageSize, totalRecords, result);
    }

    #endregion

    #region GetCourseById

    public Response<GetCourse> GetCourse(int id)
    {
        var course = _context.Courses.Find(id);
        if(course == null) 
            return new Response<GetCourse>(HttpStatusCode.NotFound, "not found");

        var getCourse = new GetCourse()
        {
            Id = course.Id,
            TeacherId = course.TeacherId,
            Title = course.Title,
            Description = course.Description,
            Level = course.Level,
            Price = course.Price,
            CreatedAt = course.CreatedAt,
        };
        return new Response<GetCourse>(getCourse);
    }

    #endregion

    #region CreateCourse

    public async Task<Response<string>> CreateCourse(CreateCourse course)
    {
        if(course.Price <= 1)
            return new Response<string>(HttpStatusCode.BadRequest, "Price must be greater than 1");

        var create = new Course()
        {
            TeacherId = course.TeacherId,
            Title = course.Title,
            Description = course.Description,
            Level = course.Level,
            Price = course.Price,
        };
        
        await _context.Courses.AddAsync(create);
        var result = await _context.SaveChangesAsync();

        return result > 0
            ? new Response<string>(HttpStatusCode.Created, "Created Course")
            : new Response<string>(HttpStatusCode.BadRequest, "Error");
    }

    #endregion

    #region UpdateCourse

    public async Task<Response<string>> UpdateCourse(int id, UpdateCourse dto)
    {
        var course = _context.Courses.Find(id);
        if(course == null) 
            return new Response<string>(HttpStatusCode.NotFound, "not found");
        
        course.TeacherId = dto.TeacherId ?? course.TeacherId;
        course.Title = dto.Title ?? course.Title;
        course.Description = dto.Description ?? course.Description;
        course.Level = dto.Level ?? course.Level;
        course.Price = dto.Price ?? course.Price;
        course.UpdatedAt = DateTime.UtcNow;
        
        var result = await _context.SaveChangesAsync();
        return result > 0
            ? new Response<string>(HttpStatusCode.NoContent, "Course Updated")
            : new Response<string>(HttpStatusCode.BadRequest, "Error");
    }

    #endregion

    #region DeleteCourse

    public async Task<Response<string>> DeleteCourse(int id)
    {
        var course = _context.Courses.Find(id);
        if(course == null)
            return new Response<string>(HttpStatusCode.NotFound, "not found");
        
        course.IsDeleted = true;
        var result = await _context.SaveChangesAsync();
        
        return result > 0
            ? new Response<string>(HttpStatusCode.NoContent, "Course Deleted")
            : new Response<string>(HttpStatusCode.BadRequest, "Error");
    }

    #endregion

    public Response<GetCourse> GetCourseWithLesson(int id)
    {
        var course = _context.Courses.Include(x => x.Lessons)
            .FirstOrDefault(x => x.Id == id);
        if(course == null)
            return new Response<GetCourse>(HttpStatusCode.NotFound, "not found");

        var getCourse = new GetCourse()
        {
            Id = course.Id,
            TeacherId = course.TeacherId,
            Title = course.Title,
            Description = course.Description,
            Level = course.Level,
            Price = course.Price,
            Lessons = course.Lessons!.Where(x => !x.IsDeleted)
                .Select(x => new GetLesson()
            {
                Id = x.Id,
                CourseId = x.CourseId,
                Title = x.Title,
                Order = x.Order,
                CreatedAt = x.CreatedAt
            }).ToList()
        };
        
        return new Response<GetCourse>(getCourse);
    }
}