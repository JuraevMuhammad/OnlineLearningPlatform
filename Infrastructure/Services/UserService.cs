using System.Net;
using Application.DTOs.User;
using Application.Filters;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.PasswordHash;
using Infrastructure.Repositories.User;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private IUserRepository _repository;
    private readonly IPasswordHashed _hashed;

    public UserService(IUserRepository repository,
        IPasswordHashed hashed)
    {
        _repository = repository;
        _hashed = hashed;
    }
    
    #region GetUsers

    public async Task<PaginationResponse<List<GetUser>>> GetUsers(FilterUser filter)
    {
        var users = await _repository.GetFilterUser(filter);
        
        var totalRecords = users.Count;
        
        var res = users.Select(x => new GetUser()
        {
            FirstName = x.FirstName,
            Age = x.Age,
            Id = x.Id,
            Phone = x.Phone,
            CreatedAt = x.CreatedAt,
        }).ToList();
        
        return new PaginationResponse<List<GetUser>>(filter.PageNumber, filter.PageSize, totalRecords, res);
    }

    #endregion

    #region GetUser

    public async Task<Response<GetUser>> GetUser(int id)
    {
        var user = await _repository.GetUserById(id);
        if(user == null) 
            return new Response<GetUser>(HttpStatusCode.NotFound, "not found");
        
        var getUser = new GetUser()
        {
            Id = user.Id, 
            FirstName = user.FirstName, 
            Age = user.Age, 
            Phone = user.Phone, 
            CreatedAt = user.CreatedAt,
        };
        return new Response<GetUser>(getUser);
    }

    #endregion

    #region CreatedUser

    public async Task<Response<string>> CreatedUser(CreatedUser dto)
    {
        if(dto.Password.Length < 8) return new Response<string>(HttpStatusCode.BadRequest, "Password is too short.");
        
        var createdUser = new User()
        {
            FirstName = dto.FirstName,
            Email = dto.Email,
            PasswordHash = _hashed.HashPassword(dto.Password),
            Role = dto.Role
        };
        var result = await _repository.CreateUser(createdUser);
        
        return result > 0
            ? new Response<string>(HttpStatusCode.Created, $"User created: {createdUser.Id}")
            : new Response<string>(HttpStatusCode.BadRequest, "User not found");
    }

    #endregion

    #region UpdateUser

    public async Task<Response<string>> UpdateUser(int id, UpdateUser dto)
    {
        var user = await _repository.GetUserById(id);
        if(user == null) return new Response<string>(HttpStatusCode.NotFound, "not found");
        
        user.FirstName = dto.FirstName ??  user.FirstName;
        user.Email = dto.Email ?? user.Email;
        user.LastName = dto.LastName ??  user.LastName;
        user.Phone = dto.Phone ??  user.Phone;
        user.Age = dto.Age ?? user.Age;
        user.Address = dto.Address ?? user.Address;
        user.UpdatedAt = DateTime.UtcNow;

        if (dto.LastPassword != null && dto.NewPassword != null && dto.LastPassword.Length >= 8)
        {
            if (!_hashed.VerifyHashedPassword(dto.LastPassword, user.PasswordHash))
                return new Response<string>(HttpStatusCode.BadRequest, "Wrong password");

            user.PasswordHash = _hashed.HashPassword(dto.NewPassword);
        }

        await _repository.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, $"User {id} has been updated");
    }

    #endregion

    #region DeleteUser

    public async Task<Response<string>> DeleteUser(int id)
    {
        var user = await _repository.GetUserById(id);
        if (user == null) return new Response<string>(HttpStatusCode.NotFound, "not found");

        user.IsDeleted = true;
        await _repository.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "User has been deleted");
    }

    #endregion
}
