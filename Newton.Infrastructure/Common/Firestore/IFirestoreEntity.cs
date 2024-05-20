using Google.Cloud.Firestore;

namespace Newton.Infrastructure.Common.Firestore;

public interface IFirestoreEntity
{
	/// <summary>
	/// Gets and set the Id.
	/// </summary>
	[FirestoreDocumentId]
	public DocumentReference? Reference { get; set; }

}
