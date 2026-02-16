using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Lesson : BaseEntity
{
    public int CourseId { get; set; }
    [Required]
    public required string Title { get; set; }
    [Required]
    public required string Content { get; set; }
    public int Order { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public Course? Course { get; set; }
}