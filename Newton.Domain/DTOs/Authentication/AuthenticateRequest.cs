using System.ComponentModel.DataAnnotations;

namespace Newton.Domain.DTOs.Authentication;

public class AuthenticateRequest
{
	[Required] public string Username { get; set; } = string.Empty;

	[Required] public string Password { get; set; } = string.Empty;
}