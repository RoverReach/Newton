using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Newton.Domain.Entities.Firestore;

[FirestoreData]
public class Club : IFirestoreEntity
{
	[FirestoreDocumentId]
	public DocumentReference Reference { get; set; }

	[FirestoreProperty("name")]
	public string Name { get; set; }

	[FirestoreProperty("yearsInvolved")]
	public int YearsInvolved { get; set; }

	[FirestoreProperty("advisorName")]
	public string AdvisorName { get; set; }

	[FirestoreProperty("advisorEmail")]
	public string AdvisorEmail { get; set; }

	[FirestoreProperty("user")]
	public DocumentReference User { get; set; }
}