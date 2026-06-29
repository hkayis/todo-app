using TodoApi.Application.Interfaces;

namespace TodoApi.Application.CommandHandlers
{
    public class UpdateToDoHandler
    {
        private readonly IToDoRepository _repository;

        public UpdateToDoHandler(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateToDoCommand command)
        {
            var todo = await _repository.GetByIdAsync(command.Id);
            if (todo == null)
                return false;

            todo.Title = command.Title;
            todo.Description = command.Description;
            todo.IsCompleted = command.IsCompleted;

            await _repository.UpdateAsync(todo);
            return true;
        }
    }
}