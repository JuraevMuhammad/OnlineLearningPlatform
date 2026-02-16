using System.Net;
using Application.DTOs.Lesson;
using Application.Filters;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Services;

public class LessonService : ILessonService
{
    private readonly ApplicationDbContext _context;
    
    public LessonService(ApplicationDbContext context)
    {
        _context = context;
    }

    #region GetLessons

    public PaginationResponse<List<GetLesson>> GetPaginationLesson(FilterLesson filter)
    {
        var lessons = _context.Lessons.AsQueryable();
        
        if(!string.IsNullOrEmpty(filter.Title))
            lessons = lessons.Where(l => l.Title.ToLower().Contains(filter.Title.ToLower()));
        if(!string.IsNullOrEmpty(filter.Content))
            lessons = lessons.Where(l => l.Content.ToLower().Contains(filter.Content));
        
        var totalRecord =  lessons.Count();
        
        var result = lessons.Where(x => !x.IsDeleted)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).ToList().Select(x => new GetLesson()
            {
                Id = x.Id,
                CourseId = x.CourseId,
                Title = x.Title,
                Content = x.Content,
                Order = x.Order,
                CreatedAt = x.CreatedAt,
            }).ToList();
        
        return new PaginationResponse<List<GetLesson>>(filter.PageNumber, filter.PageSize,totalRecord, result);
    }

    #endregion

    #region GetLessonById

    public Response<GetLesson> GetLesson(int id)
    {
        var lesson = _context.Lessons.Find(id);
        if (lesson == null)
            return new Response<GetLesson>(HttpStatusCode.NotFound, "not found");

        var getLesson = new GetLesson()
        {
            Id = lesson.Id,
            CourseId = lesson.CourseId,
            Title = lesson.Title,
            Content = lesson.Content,
            Order = lesson.Order,
            CreatedAt = lesson.CreatedAt,
        };
        
        return new Response<GetLesson>(getLesson);
    }

    #endregion

    #region CreateLesson

    public async Task<Response<string>> CreateLesson(CreateLesson dto)
    {
        var res = _context.Lessons.FirstOrDefault(x => x.Title == dto.Title);
        if(res != null) 
            return new Response<string>(HttpStatusCode.BadRequest, "lesson already exists");

        var lesson = new Lesson()
        {
            CourseId = dto.CourseId,
            Title = dto.Title,
            Content = dto.Content,
            Order = dto.Order,
        };
        
        await _context.Lessons.AddAsync(lesson);
        var result = await _context.SaveChangesAsync();
        
        return result > 0
            ? new Response<string>(HttpStatusCode.OK, "Create Lesson")
            : new Response<string>(HttpStatusCode.BadRequest, "lesson already exists");
    }

    #endregion

    #region UpdateLesson

    public async Task<Response<string>> UpdateLesson(int id, UpdateLesson dto)
    {
        var res = _context.Lessons.FirstOrDefault(x => x.Id == id);
        if(res == null)
            return new Response<string>(HttpStatusCode.NotFound, "lesson not found");
        
        res.CourseId = dto.CourseId ?? res.CourseId;
        res.Title = dto.Title ?? res.Title;
        res.Content = dto.Content ?? res.Content;
        res.Order = dto.Order ?? res.Order;
        res.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "Update Lesson");
    }

    #endregion

    #region DeleteLesson

    public async Task<Response<string>> DeleteLesson(int id)
    {
        var lesson = _context.Lessons.Find(id);
        if (lesson == null)
            return new Response<string>(HttpStatusCode.NotFound, "not found");

        lesson.IsDeleted = true;

        await _context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "Delete Lesson");
    }

    #endregion
}