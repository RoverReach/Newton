using Newton.Domain.DTOs.Authentication;
using Newton.Domain.Entities.Identity;

namespace Newton.Infrastructure.Authentication.Services;

public interface IUserService
{
	Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model);
	Task<ApplicationUser?> GetById(string id);
}
