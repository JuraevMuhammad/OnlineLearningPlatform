using Application.Filters;
using Infrastructure.Data;
using Infrastructure.Redis;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IRedisCache _cache;
    
    public UserRepository(ApplicationDbContext context,
        IRedisCache cache)
    {
        _context = context;
        _cache = cache;
    }
    
    public async Task<int> CreateUser(Domain.Entities.User user)
    {
        var res = await _context.Users.FirstOrDefaultAsync(x => x.FirstName == user.FirstName);
        if(res != null)
            throw new Exception("User already exists");
        
        await _context.Users.AddAsync(user);
        var result = await _context.SaveChangesAsync();
        if (result > 0)
        {
            Console.WriteLine("==================REMOVE CACHE===================");
            await _cache.RemoveDataAsync("users:all");
        }
        return result;
    }

    public async Task<Domain.Entities.User?> GetUserById(int id)
    {
        var cacheKay = $"user:{id}";

        var cacheUser = await _cache.GetDataAsync<Domain.Entities.User>(cacheKay);
        if (cacheUser != null)
        {
            Console.WriteLine("==================GET IN CACHE===================");
            return cacheUser;
        }
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        Console.WriteLine("==================GET IN DATABASE===================");
        if (user == null) 
            return null;
        
        await _cache.SetDataAsync(cacheKay, user);
        return user;
    }
    public async Task<List<Domain.Entities.User>> GetUsers()
    {
        var users = await _cache.GetDataAsync<List<Domain.Entities.User>>("users:all");
        if (users != null)
        {
            Console.WriteLine("==================GET IN CACHE==================="); 
            return users;
        }
        users = await _context.Users
            .Where(x => !x.IsDeleted).ToListAsync();
        await _cache.SetDataAsync("users:all", users);
        Console.WriteLine("==================GET IN DATABASE===================");
        return users;
    }
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
    {
        var res = await _context.SaveChangesAsync();
        if (res > 0)
            await _cache.RemoveDataAsync("user:all");
        return res;
    }
}