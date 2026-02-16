using System.Net;

namespace Application.Responses;

public class PaginationResponse<T> : Response<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; } = 1;
    public int TotalPages { get; set; } = 10;
    public int TotalRecords { get; set; }

    public PaginationResponse(HttpStatusCode statusCode, string message) : base(statusCode, message)
    {}

    public PaginationResponse(int pageNumber, int pageSize, int totalRecords, T data) : base(data)
    {
        PageNumber = pageNumber <= 0 ? 1 : pageNumber;
        PageSize = pageSize <= 0 ? 1 : pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);
    }
}