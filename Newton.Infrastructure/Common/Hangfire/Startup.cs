﻿using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newton.Infrastructure.Common.Hangfire.Filters;

namespace Newton.Infrastructure.Common.Hangfire;

public static class Startup
{
	public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		services.AddHangfire(config => config
			.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
			.UseSimpleAssemblyNameTypeSerializer()
			.UseRecommendedSerializerSettings()
			.UseSQLiteStorage("Data Source=database.db")
			);

		services.AddHangfireServer();
	}

	public static void Configure(IApplicationBuilder app, IConfiguration configuration)
	{
		// Set up hangfire capabilities
		var options = new DashboardOptions
		{
			Authorization = new[] { new HangfireAuthorizationFilter() },
			AppPath = "#",
			DashboardTitle = "",
			DisplayStorageConnectionString = false
		};

		app.UseHangfireDashboard("/admin/job/hangfire", options);
	}
}

