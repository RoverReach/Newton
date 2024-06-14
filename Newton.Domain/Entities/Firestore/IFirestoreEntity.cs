using Google.Cloud.Firestore;

namespace Newton.Domain.Entities.Firestore;

public interface IFirestoreEntity
{
	/// <summary>
	/// Gets and set the Id.
	/// </summary>
	[FirestoreDocumentId]
	public DocumentReference Reference { get; set; }

}
