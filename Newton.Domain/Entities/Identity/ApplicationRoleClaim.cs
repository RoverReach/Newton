using Microsoft.AspNetCore.Identity;

namespace Newton.Domain.Entities.Identity;

public class ApplicationRoleClaim : IdentityRoleClaim<string>
{
	public virtual ApplicationRole Role { get; set; }
}
