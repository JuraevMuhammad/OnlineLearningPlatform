using System.Net;

namespace Application.Responses;

public class PaginationResponse<T> : Response<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }

    public PaginationResponse(HttpStatusCode statusCode, string message) : base(statusCode, message)
    {}

    public PaginationResponse(int pageNumber, int pageSize, int totalRecords, T data) : base(data)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);
    }
}