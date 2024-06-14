using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
using Google.Cloud.Firestore;
using Newton.Domain.Entities.Firestore;
using Newton.Infrastructure.Common.Firestore;
using RoverCore.ToastNotification.Helpers;

namespace Newton.Web.Areas.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Produces("application/json")]
[Area("Api")]
public class UsersController : Controller
{
	private readonly ISettingsService<ApplicationSettings> _settingsService;
	private readonly CacheService _cache;
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly FirestoreProvider _firestore;

	public UsersController(ISettingsService<ApplicationSettings> settingsService, CacheService cache, FirestoreProvider firestoreProvider, UserManager<ApplicationUser> userManager)
	{
		_settingsService = settingsService;
		_cache = cache;
		_userManager = userManager;
		_firestore = firestoreProvider;
	}

	// GET: api/users/GetUser?uid=uid
	[AllowAnonymous]  // Don't require JWT authentication to access this method
	public object GetUser(string uid)
	{
		var ct = new CancellationToken();
		var user = _firestore.Get<User>(uid, ct).Result;

		return new
		{
			firstName = user.FirstName,
			lastName = user.LastName,
			grade = user.Grade,
			email = user.Email
		};
	}

	// GET: api/users/GetUsers
	[AllowAnonymous]  // Don't require JWT authentication to access this method
	public object GetUsers()
	{
		var ct = new CancellationToken();
		var userList = _firestore.GetAllUsers<User>(ct).Result;

		return userList?.Select(x => new
		{
			x.FirstName,
			x.LastName,
			x.Email,
			x.Grade
		});
	}

	// GET: api/users/GetUserSports?uid=UID
	[AllowAnonymous]
	public object GetUserSports(string uid)
	{
		var ct = new CancellationToken();
		var user = _firestore.Get<User>(uid, ct).Result;
		var userRef = user?.Reference;

		if (userRef == null)
		{
			return null;
		}

		var sportsList = _firestore.GetSports<Sport>(userRef, ct).Result;

		if (sportsList == null) return null;

		return sportsList?.Select(x => new
		{
			x.Name,
			x.Level,
			x.CoachLastName,
			x.CoachEmail
		});
	}
	// GET: api/users/GetUserClubs?uid=UID
	[AllowAnonymous]
	public object GetUserClubs(string uid)
	{
		var ct = new CancellationToken();
		var user = _firestore.Get<User>(uid, ct).Result;
		var userRef = user?.Reference;

		if (userRef == null)
		{
			return null;
		}

		var clubsList = _firestore.GetClubs<Club>(userRef, ct).Result;

		if (clubsList == null) return null;

		return clubsList?.Select(x => new
		{
			x.Name,
			x.YearsInvolved,
			x.AdvisorName,
			x.AdvisorEmail
		});
	}

	// GET: api/users/GetUserJobs?uid=UID
	[AllowAnonymous]
	public object GetUserJobs(string uid)
	{
		var ct = new CancellationToken();
		var user = _firestore.Get<User>(uid, ct).Result;
		var userRef = user?.Reference;

		if (userRef == null)
		{
			return null;
		}

		var clubsList = _firestore.GetJobs<Job>(userRef, ct).Result;

		if (clubsList == null) return null;

		return clubsList?.Select(x => new
		{
			x.EmployerName,
			x.EmployerEmail,
			x.EmployerPhone,
			x.Position
		});
	}

}
