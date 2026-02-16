namespace Application.DTOs.AnswerOption;

public class CreateAnswerOption
{
    public int QuestionId { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}