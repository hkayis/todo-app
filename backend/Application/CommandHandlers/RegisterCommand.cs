namespace TodoApi.Application.CommandHandlers
{
    public class RegisterCommand
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}