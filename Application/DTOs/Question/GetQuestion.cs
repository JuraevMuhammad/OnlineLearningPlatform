using Domain.Enum;

namespace Application.DTOs.Question;

public class GetQuestion
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public string Text { get; set; } = string.Empty;
    public QuestionType Type { get; set; }
    public DateTime CreatedAt { get; set; }
}