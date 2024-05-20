using System.ComponentModel.DataAnnotations;

namespace Newton.Domain.Entities.Audit;

public class ActivityLog
{
	[Key]
	public int Id { get; set; }
	public string UserId { get; set; } = string.Empty;
	public DateTime ActivityDate { get; set; } = DateTime.UtcNow;
	public string Service { get; set; } = string.Empty;
	public string Action { get; set; } = string.Empty;
	public string Metadata { get; set; } = "{}";
	public string State { get; set; } = string.Empty;  // "success", "failure", etc
}
