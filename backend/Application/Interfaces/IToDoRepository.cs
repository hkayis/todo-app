using TodoApi.Domain.Entities;

namespace TodoApi.Application.Interfaces
{
    public interface IToDoRepository
    {
        Task<List<ToDo>> GetAllAsync();
        Task<ToDo?> GetByIdAsync(Guid id);
        Task<ToDo> AddAsync(ToDo todo);
        Task<bool> UpdateAsync(ToDo todo);
        Task<bool> DeleteAsync(Guid id);
    }
}