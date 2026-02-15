namespace Domain.Entities;

public class StudentExamResult : BaseEntity
{
    public int StudentId { get; set; }
    public int ExamId { get; set; }
    public int Score { get; set; }
    public bool Passed { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public User? Student { get; set; }
    public Exam? Exam { get; set; }
}