using System.ComponentModel.DataAnnotations;
using Domain.Enum;

namespace Application.DTOs.Course;

public class CreateCourse
{
    public int TeacherId { get; set; }
    [Required]
    public required string Title { get; set; }
    public string? Description { get; set; }
    public CourseLevel Level { get; set; }
    [Required]
    public required decimal Price { get; set; }
}