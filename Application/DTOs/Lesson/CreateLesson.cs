using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Lesson;

public class CreateLesson
{
    public int CourseId { get; set; }
    [Required]
    public required string Title { get; set; }
    [Required]
    public required string Content { get; set; }
    public int Order { get; set; }
}