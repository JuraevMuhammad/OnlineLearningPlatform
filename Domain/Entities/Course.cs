using System.ComponentModel.DataAnnotations;
using Domain.Enum;

namespace Domain.Entities;

public class Course : BaseEntity
{
    [Required]
    public required string Title { get; set; }
    public string? Description { get; set; }
    public CourseLevel Level { get; set; }
    [Required]
    public required decimal Price { get; set; }
    public DateTime UpdatedAt { get; set; }
}