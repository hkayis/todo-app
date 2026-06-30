using TodoApi.Domain.Entities;

namespace TodoApi.Application.Interfaces
{
	public interface ITokenService
	{
		string CreateToken(User user);
	}
}