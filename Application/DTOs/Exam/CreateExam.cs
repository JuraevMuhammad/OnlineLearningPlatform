using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Exam;

public class CreateExam
{
    public int CourseId { get; set; }
    [Required]
    public required string Title { get; set; }
    public int MaxScore { get; set;}
}