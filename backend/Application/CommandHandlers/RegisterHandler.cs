using TodoApi.Domain.Entities;
using TodoApi.Application.Interfaces;

namespace TodoApi.Application.CommandHandlers
{
    public class RegisterHandler
    {
        private readonly IUserRepository _userRepository;

        // Artık DbContext değil, IUserRepository enjekte ediliyor
        public RegisterHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(RegisterCommand command)
        {
            // Repository üzerinden kontrol
            var varMi = await _userRepository.ExistsByUsernameAsync(command.Username);

            if (varMi)
                return false;

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = command.Username,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.Now
            };

            // Repository üzerinden ekle
            await _userRepository.AddAsync(user);
            return true;
        }
    }
}