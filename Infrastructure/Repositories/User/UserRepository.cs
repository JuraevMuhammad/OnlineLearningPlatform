using Application.Filters;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.User;

public class UserRepository : IUserRepository
{
    ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<int> CreateUser(Domain.Entities.User user)
    {
        var res = await _context.Users.FirstOrDefaultAsync(x => x.FirstName == user.FirstName);
        if(res != null)
            throw new Exception("User already exists");
        
        await _context.Users.AddAsync(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<Domain.Entities.User?> GetUserById(int id) 
        => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<List<Domain.Entities.User>> GetUsers()
        => await _context.Users.ToListAsync();

    public async Task<List<Domain.Entities.User>> GetFilterUser(FilterUser filter)
    {
        var users = _context.Users
            .Where(x => !x.IsDeleted).AsQueryable();
        
        if(!string.IsNullOrEmpty(filter.FirstName))
            users = users.Where(u => u.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
        
        if(filter.MaxAge!=null)
            users = users.Where(u => u.Age <= filter.MaxAge.Value);
        
        if(filter.MinAge != null)
            users = users.Where(u => u.Age >= filter.MinAge.Value);

        return await users
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).ToListAsync();
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}