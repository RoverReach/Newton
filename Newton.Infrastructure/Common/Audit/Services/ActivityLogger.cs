﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Newton.Domain.Entities.Audit;
using Newton.Infrastructure.Identity.Extensions;
using Newton.Infrastructure.Persistence.DbContexts;
using Serviced;
using System.Text.Json;

namespace Newton.Infrastructure.Common.Audit.Services;

public class ActivityLogger : IScoped
{
	private readonly ApplicationDbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public ActivityLogger(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
	{
		_context = context;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task AddLog(string service, string action, string userId, string state = "success", object? metadata = null)
	{
		_context!.ActivityLog.Add(new ActivityLog
		{
			Service = service,
			Action = action,
			UserId = userId,
			State = state,
			Metadata = JsonSerializer.Serialize(metadata)
		});
		await _context.SaveChangesAsync();
	}

	/// <summary>
	/// Adds a log entry based on the MVC action that was utilized and the currently logged in user
	/// </summary>
	/// <param name="state"></param>
	/// <param name="action">Custom action name, if null then uses the name of the current action method</param>
	/// <returns></returns>
	public async Task AddAction(string state = "success", string? action = null)
	{
		var httpContext = _httpContextAccessor.HttpContext;

		if (httpContext != null)
		{
			var controllerActionDescriptor = httpContext
				.GetEndpoint()?
				.Metadata
				.GetMetadata<ControllerActionDescriptor>();

			if (controllerActionDescriptor != null)
			{
				var controllerName = controllerActionDescriptor.ControllerName;

				var actionName = action ?? controllerActionDescriptor.ActionName;
				var userId = string.Empty;

				var identity = httpContext.User.Identity;
				if (identity is { IsAuthenticated: true })
				{
					userId = httpContext.User.GetUserId();
				}

				var rd = httpContext.GetRouteData();

				await AddLog(controllerName, actionName, userId, state, new
				{
					RouteData = rd
				});
			}
		}
	}
}
