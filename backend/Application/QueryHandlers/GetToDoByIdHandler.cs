using TodoApi.Application.DTOs;
using TodoApi.Application.Interfaces;

namespace TodoApi.Application.QueryHandlers
{
	public class GetToDoByIdHandler
	{
		private readonly IToDoRepository _repository;

		public GetToDoByIdHandler(IToDoRepository repository)
		{
			_repository = repository;
		}

		public async Task<ToDoResponseDto?> Handle(GetToDoByIdQuery query)
		{
			var todo = await _repository.GetByIdAsync(query.Id);
			if (todo == null)
				return null;

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