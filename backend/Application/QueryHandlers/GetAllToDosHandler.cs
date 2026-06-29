using TodoApi.Application.DTOs;
using TodoApi.Application.Interfaces;

namespace TodoApi.Application.QueryHandlers
{
    public class GetAllToDosHandler
    {
        private readonly IToDoRepository _repository;

        public GetAllToDosHandler(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ToDoResponseDto>> Handle(GetAllToDosQuery query)
        {
            var todos = await _repository.GetAllAsync();

            return todos.Select(t => new ToDoResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt
            }).ToList();
        }
    }
}