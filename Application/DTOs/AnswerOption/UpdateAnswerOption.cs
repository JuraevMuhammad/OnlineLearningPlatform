namespace Application.DTOs.AnswerOption;

public class UpdateAnswerOption
{
    public int? QuestionId { get; set; }
    public string? Text { get; set; }
    public bool? IsCorrect { get; set; }
}