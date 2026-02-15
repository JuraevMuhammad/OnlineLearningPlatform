using Domain.Enum;

namespace Application.DTOs.Course;

public class UpdateCourse
{
    public int? TeacherId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public CourseLevel? Level { get; set; }
    public decimal? Price { get; set; }
}