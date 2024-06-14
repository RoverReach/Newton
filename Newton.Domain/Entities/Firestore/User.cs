using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Newton.Domain.Entities.Firestore;

[FirestoreData]
public class User : IFirestoreEntity
{
	/// <summary>
	/// Gets and set the Id.
	/// </summary>
	[FirestoreDocumentId]
	public DocumentReference Reference { get; set; }

	[FirestoreProperty("firstName")]
	public string FirstName { get; set; }

	[FirestoreProperty("lastName")]
	public string LastName { get; set; }

	[FirestoreProperty("email")]
	public string Email { get; set; }

	[FirestoreProperty("grade")]
	public int Grade { get; set; }
}
