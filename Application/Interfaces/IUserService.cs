using Application.DTOs.User;
using Application.Filters;
using Application.Responses;

namespace Application.Interfaces;

public interface IUserService
{
    Task<PaginationResponse<List<GetUser>>> GetUsers(FilterUser filter);
    Task<Response<GetUser>> GetUser(int id);
    Task<Response<string>> CreatedUser(CreatedUser dto);
    Task<Response<string>> UpdateUser(int id, UpdateUser dto);
    Task<Response<string>> DeleteUser(int id);
}