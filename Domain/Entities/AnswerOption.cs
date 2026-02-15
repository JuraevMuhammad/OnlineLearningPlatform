namespace Domain.Entities;

public class AnswerOption : BaseEntity
{
    public int QuestionId { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public Question? Question { get; set; }
}