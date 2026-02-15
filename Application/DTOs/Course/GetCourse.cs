using System.ComponentModel.DataAnnotations;
using Application.DTOs.Lesson;
using Domain.Enum;

namespace Application.DTOs.Course;

public class GetCourse
{
    public int Id { get; set; }
    public int TeacherId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public CourseLevel Level { get; set; }
    public required decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<GetLesson>? Lessons { get; set; }
}