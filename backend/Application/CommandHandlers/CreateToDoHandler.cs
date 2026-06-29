using TodoApi.Application.DTOs;
using TodoApi.Domain.Entities;
using TodoApi.Application.Interfaces;

namespace TodoApi.Application.CommandHandlers
{
    public class CreateToDoHandler
    {
        private readonly IToDoRepository _repository;

        public CreateToDoHandler(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ToDoResponseDto> Handle(CreateToDoCommand command)
        {
            var todo = new ToDo
            {
                Id = Guid.NewGuid(),   // yeni Guid üret
                Title = command.Title,
                Description = command.Description,
                IsCompleted = command.IsCompleted,
                CreatedAt = DateTime.Now
            };

            await _repository.AddAsync(todo);

            return new ToDoResponseDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
                CreatedAt = todo.CreatedAt
            };
        }
    }
}