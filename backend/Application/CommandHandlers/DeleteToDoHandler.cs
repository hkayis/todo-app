using TodoApi.Application.Interfaces;
namespace TodoApi.Application.CommandHandlers
{
    public class DeleteToDoHandler
    {
        private readonly IToDoRepository _repository;

        public DeleteToDoHandler(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteToDoCommand command)
        {
            return await _repository.DeleteAsync(command.Id);
        }
    }
}