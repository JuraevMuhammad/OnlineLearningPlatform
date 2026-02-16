namespace Application.DTOs.StudentExamResult;

public class CreateExamResult
{
    public int StudentId { get; set; }
    public int ExamId { get; set; }
    public int Score { get; set; }
    public bool Passed { get; set; }
}