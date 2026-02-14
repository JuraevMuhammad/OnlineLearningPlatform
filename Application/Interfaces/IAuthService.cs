using Application.DTOs.Auth;
using Application.Responses;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<Response<string>> Register(Register dto);
    Response<string> Login(Login dto);
}