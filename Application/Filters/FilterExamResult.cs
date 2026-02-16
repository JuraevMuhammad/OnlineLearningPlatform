namespace Application.Filters;

public class FilterExamResult : BaseFilter
{
    public int? StudentId { get; set; }
    public int? ExamId { get; set; }
    public bool? Passed { get; set; }
}