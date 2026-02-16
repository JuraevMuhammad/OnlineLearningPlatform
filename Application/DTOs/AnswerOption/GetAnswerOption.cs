namespace Application.DTOs.AnswerOption;

public class GetAnswerOption
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public DateTime CreatedAt { get; set; }
}