using Domain.Enum;

namespace Domain.Entities;

public class Question : BaseEntity
{
    public int ExamId { get; set; }
    public string Text { get; set; } = string.Empty;
    public QuestionType Type { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Exam? Exam { get; set; }
    public List<AnswerOption>?  AnswerOptions { get; set; }
}