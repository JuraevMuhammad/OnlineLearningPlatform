using Application.Filters;

namespace Infrastructure.Repositories.User;

public interface IUserRepository
{
    Task<int> CreateUser(Domain.Entities.User user);
    Task<Domain.Entities.User?> GetUserById(int id);
    Task<List<Domain.Entities.User>> GetUsers();
    Task<List<Domain.Entities.User>> GetFilterUser(FilterUser filter);
    Task<int> SaveChangesAsync();
}