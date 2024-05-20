using Microsoft.AspNetCore.Identity;

namespace Newton.Domain.Entities.Identity;

public class ApplicationUserToken : IdentityUserToken<string>
{
	public virtual ApplicationUser User { get; set; }
}
