using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Newton.Domain.Entities.Firestore;

[FirestoreData]
public class Sport : IFirestoreEntity
{
	[FirestoreDocumentId]
	public DocumentReference Reference { get; set; }

	[FirestoreProperty("coachEmail")]
	public string CoachEmail { get; set; }

	[FirestoreProperty("coachLastName")]
	public string CoachLastName { get; set; }

	[FirestoreProperty("level")]
	public string Level { get; set; }

	[FirestoreProperty("name")]
	public string Name { get; set; }

	[FirestoreProperty("user")]
	public DocumentReference User { get; set; }
}