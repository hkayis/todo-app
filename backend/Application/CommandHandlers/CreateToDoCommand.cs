namespace TodoApi.Application.CommandHandlers
{
    public class CreateToDoCommand
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}