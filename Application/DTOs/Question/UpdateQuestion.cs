using Domain.Enum;

namespace Application.DTOs.Question;

public class UpdateQuestion
{
    public int? ExamId { get; set; }
    public string? Text { get; set; }
    public QuestionType? Type { get; set; }
}