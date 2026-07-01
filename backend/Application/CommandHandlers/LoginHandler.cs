using TodoApi.Application.DTOs;
using TodoApi.Application.Interfaces;


namespace TodoApi.Application.CommandHandlers
{
    public class LoginHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto?> Handle(LoginCommand command)
        {
            
            var user = await _userRepository.GetByUsernameAsync(command.Username);

            if (user == null)
                return null;

            var sifreDogru = BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash);

            if (!sifreDogru)
                return null;

            var token = _tokenService.CreateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Username = user.Username
            };
        }
    }
}