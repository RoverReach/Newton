﻿using Newton.Domain.Entities.Identity;

namespace Newton.Domain.DTOs.Authentication;

public class AuthenticateResponse
{
	public AuthenticateResponse(ApplicationUser user, string token)
	{
		Id = user.Id;
		Username = user.UserName;
		Token = token;
	}

	public string Id { get; set; }
	public string Username { get; set; }
	public string Token { get; set; }
}