namespace Application.Filters;

public class FilterUser : BaseFilter
{
    public string? FirstName { get; set; }
    public int? MaxAge { get; set; }
    public int? MinAge { get; set; }
}