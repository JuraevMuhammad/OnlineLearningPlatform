using System.ComponentModel.DataAnnotations;
using Domain.Enum;

namespace Domain.Entities;

public class Course : BaseEntity
{
    public int TeacherId { get; set; }
    [Required]
    public required string Title { get; set; }
    public string? Description { get; set; }
    public CourseLevel Level { get; set; }
    [Required]
    public required decimal Price { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public List<Exam>? Exams { get; set; }
    public List<Lesson>? Lessons { get; set; }
    
    public List<StudentCourse>? Students { get; set; }
    public User? Teacher { get; set; }
}