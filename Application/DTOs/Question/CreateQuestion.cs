using Domain.Enum;

namespace Application.DTOs.Question;

public class CreateQuestion
{
    public int ExamId { get; set; }
    public string Text { get; set; } = string.Empty;
    public QuestionType Type { get; set; }
}