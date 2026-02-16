namespace Application.DTOs.Exam;

public class GetExam
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int MaxScore { get; set; }
    public DateTime CreatedAt { get; set; }
}