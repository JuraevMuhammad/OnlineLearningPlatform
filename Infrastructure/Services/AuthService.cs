using System.Net;
using Application.DTOs.Auth;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using Domain.Enum;
using Infrastructure.Data;
using Infrastructure.Jwt;
using Infrastructure.PasswordHash;
using Microsoft.JSInterop.Infrastructure;

namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    #region Constructor

    private readonly ApplicationDbContext _context;
    private readonly IPasswordHashed _hashed;
    private readonly IJwtProvider _jwt;

    public AuthService(ApplicationDbContext context,
        IPasswordHashed hashed, IJwtProvider jwt)
    {
        _context = context;
        _hashed = hashed;
        _jwt = jwt;
    }

    #endregion

    #region Register

    public async Task<Response<string>> Register(Register dto)
    {
        var user = _context.Users.FirstOrDefault(x => x.FirstName == dto.FirstName);
        if(user != null) return new Response<string>(HttpStatusCode.BadRequest, "bad request");

        if (dto.Password.Length < 8) return new Response<string>(HttpStatusCode.BadRequest, "problem in your password");

        var newUser = new User()
        {
            FirstName = dto.FirstName,
            Email = dto.Email,
            PasswordHash = _hashed.HashPassword(dto.Password),
            Role = dto.Role
        };

        _context.Users.Add(newUser);
        var res = await _context.SaveChangesAsync();
        return res > 0
            ? new Response<string>(HttpStatusCode.OK, "created user")
            : new Response<string>(HttpStatusCode.BadRequest, "bad request");
    }

    #endregion

    #region Login

    public Response<string> Login(Login dto)
    {
        var user = _context.Users.FirstOrDefault(x => x.FirstName == dto.FirstName);
        if (user == null) return new Response<string>(HttpStatusCode.NotFound, "not found");

        var result = _hashed.VerifyHashedPassword(dto.Password, user.PasswordHash);
        if (!result) return new Response<string>(HttpStatusCode.BadRequest, "invalid password");

        var token = _jwt.GenerateJwt(user);
        return new Response<string>(HttpStatusCode.OK, token);
    }

    #endregion
}