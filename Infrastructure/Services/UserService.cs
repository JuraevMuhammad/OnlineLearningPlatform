using System.Net;
using Application.DTOs.User;
using Application.Filters;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.PasswordHash;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHashed _hashed;

    public UserService(ApplicationDbContext context, IPasswordHashed hashed)
    {
        _context = context;
        _hashed = hashed;
    }
    
    #region GetUsers

    public PaginationResponse<List<GetUser>> GetUsers(FilterUser filter)
    {
        var users = _context.Users.Where(x => !x.IsDeleted).AsQueryable();
        
        if(!string.IsNullOrEmpty(filter.FirstName))
            users = users.Where(u => u.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
        
        if(filter.MaxAge!=null)
            users = users.Where(u => u.Age <= filter.MaxAge.Value);
        
        if(filter.MinAge != null)
            users = users.Where(u => u.Age >= filter.MinAge.Value);
        
        var totalRecords = users.Count();
        var res = users
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).ToList().Select(x => new GetUser()
                {
                    FirstName = x.FirstName,
                    Age = x.Age,
                    Id = x.Id,
                    Phone = x.Phone,
                    CreatedAt = x.CreatedAt,
                }
            ).ToList();
        
        return new PaginationResponse<List<GetUser>>(filter.PageNumber, filter.PageSize, totalRecords, res);
    }

    #endregion

    #region GetUser

    public Response<GetUser> GetUser(int id)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if(user == null) return new Response<GetUser>(HttpStatusCode.NotFound, "User not found");

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
        var result = _context.Users.FirstOrDefault(x => x.FirstName == dto.FirstName);
        if(result != null) return new Response<string>(HttpStatusCode.BadRequest, "User not found");

        if(dto.Password.Length < 8) return new Response<string>(HttpStatusCode.BadRequest, "Password is too short.");
        
        var createdUser = new User()
        {
            FirstName = dto.FirstName,
            Email = dto.Email,
            PasswordHash = _hashed.HashPassword(dto.Password),
            Role = dto.Role
        };
        
        _context.Users.Add(createdUser);
        await _context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.Created, $"User created: {createdUser.Id}");
    }

    #endregion

    #region UpdateUser

    public async Task<Response<string>> UpdateUser(int id, UpdateUser dto)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if(user == null) return new Response<string>(HttpStatusCode.NotFound, "User not found");
        
        user.FirstName = dto.FirstName ??  user.FirstName;
        user.Email = dto.Email ?? user.Email;
        user.LastName = dto.LastName ??  user.LastName;
        user.Phone = dto.Phone ??  user.Phone;
        user.Age = dto.Age ?? user.Age;
        user.Address = dto.Address ?? user.Address;
        user.UpdatedAt = dto.UpdatedAt;
        
        if(dto.LastPassword != null && dto.LastPassword.Length >= 8 && dto.NewPassword != null)
            if(_hashed.VerifyHashedPassword(dto.LastPassword, user.PasswordHash))
                user.PasswordHash = _hashed.HashPassword(dto.NewPassword);
        
        await _context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.Created, $"User {id} has been updated");
    }

    #endregion

    #region DeleteUser

    public async Task<Response<string>> DeleteUser(int id)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (user == null) return new Response<string>(HttpStatusCode.NotFound, "not found");

        user.IsDeleted = true;
        await _context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "User has been deleted");
    }

    #endregion
}
