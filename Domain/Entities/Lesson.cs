using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Lesson
{
    public int CourseId { get; set; }
    [Required]
    public required string Title { get; set; }
    public int Order { get; set; }
    public DateTime UpdatedAt { get; set; }
}