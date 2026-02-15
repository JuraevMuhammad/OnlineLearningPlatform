using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Exam : BaseEntity
{
    public int CourseId { get; set; }
    [Required]
    public required string Title { get; set; }
    public int MaxScore { get; set; }
    public DateTime? UpdateAt { get; set; }
    
    public Course? Course { get; set; }
    public List<Question>? Questions { get; set; }
    public List<StudentExamResult>?  StudentExamResults { get; set; }
}