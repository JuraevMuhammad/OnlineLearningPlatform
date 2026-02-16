namespace Application.DTOs.Exam;

public class UpdateExam
{
    public int? CourseId { get; set; }
    public string? Title { get; set; }
    public int? MaxScore { get; set;}
}