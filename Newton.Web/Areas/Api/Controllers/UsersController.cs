using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newton.Domain.Entities.Identity;
using Newton.Domain.Entities.Settings;
using Newton.Infrastructure.Common.Cache.Services;
using RoverCore.Abstractions.Settings;
using System.Threading.Tasks;

namespace Newton.Web.Areas.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Produces("application/json")]
[Area("Api")]
public class UsersController : Controller
{
	private readonly ISettingsService<ApplicationSettings> _settingsService;
	private readonly CacheService _cache;
	private readonly UserManager<ApplicationUser> _userManager;

	public UsersController(ISettingsService<ApplicationSettings> settingsService, CacheService cache, UserManager<ApplicationUser> userManager)
	{
		_settingsService = settingsService;
		_cache = cache;
		_userManager = userManager;
	}

	// GET: api/users/notprotected
	[AllowAnonymous]  // Don't require JWT authentication to access this method
	public object NotProtected(string uid)
	{
		UserRecord userRecord = FirebaseAuth.DefaultInstance.GetUserAsync(uid).Result;


		return new
		{
			Result = "Hello!",
			uid = uid,
			email = userRecord.Email,
			firstName = userRecord.DisplayName

		};
	}

	// GET: api/users/protected
	public async Task<object> Protected()
	{
		var user = await _userManager.GetUserAsync(User);

		return new
		{
			Result = $"Hello authenticated user {user.UserName}!"
		};
	}

}
