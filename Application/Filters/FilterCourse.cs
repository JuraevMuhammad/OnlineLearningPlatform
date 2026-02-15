using Domain.Enum;

namespace Application.Filters;

public class FilterCourse : BaseFilter
{
    public string? Title { get; set; }
    public CourseLevel? Level { get; set; }
}