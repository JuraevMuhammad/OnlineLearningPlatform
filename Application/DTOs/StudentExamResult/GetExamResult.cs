namespace Application.DTOs.StudentExamResult;

public class GetExamResult
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int ExamId { get; set; }
    public int Score { get; set; }
    public bool Passed { get; set; }
    public DateTime CreatedAt { get; set; }
}