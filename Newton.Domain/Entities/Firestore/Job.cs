using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Newton.Domain.Entities.Firestore;

[FirestoreData]
public class Job : IFirestoreEntity
{
	[FirestoreDocumentId]
	public DocumentReference Reference { get; set; }

	[FirestoreProperty("employer")]
	public string EmployerName { get; set; }

	[FirestoreProperty("employerEmail")]
	public string EmployerEmail { get; set; }

	[FirestoreProperty("employerPhone")]
	public string EmployerPhone { get; set; }

	[FirestoreProperty("position")]
	public string Position { get; set; }

	[FirestoreProperty("user")]
	public DocumentReference User { get; set; }
}
