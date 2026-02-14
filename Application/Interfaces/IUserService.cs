using Application.DTOs.User;
using Application.Filters;
using Application.Responses;

namespace Application.Interfaces;

public interface IUserService
{
    PaginationResponse<List<GetUser>> GetUsers(FilterUser filter);
    Response<GetUser> GetUser(int id);
    Task<Response<string>> CreatedUser(CreatedUser dto);
    Task<Response<string>> UpdateUser(int id, UpdateUser dto);
    Task<Response<string>> DeleteUser(int id);
}