using TodoApi.Domain.Entities;

namespace TodoApi.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> ExistsByUsernameAsync(string username);
        Task<User> AddAsync(User user);
    }
}