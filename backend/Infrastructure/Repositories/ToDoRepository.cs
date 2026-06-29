using Microsoft.EntityFrameworkCore;
using TodoApi.Domain.Entities;
using TodoApi.Infrastructure.Persistence;
using TodoApi.Application.Interfaces;
namespace TodoApi.Infrastructure.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly AppDbContext _context;

        public ToDoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ToDo>> GetAllAsync()
        {
            return await _context.ToDos.ToListAsync();
        }

        public async Task<ToDo?> GetByIdAsync(Guid id)
        {
            return await _context.ToDos.FindAsync(id);
        }

        public async Task<ToDo> AddAsync(ToDo todo)
        {
            _context.ToDos.Add(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<bool> UpdateAsync(ToDo todo)
        {
            _context.ToDos.Update(todo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var todo = await _context.ToDos.FindAsync(id);
            if (todo == null)
                return false;

            _context.ToDos.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}