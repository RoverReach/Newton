using Google.Cloud.Firestore;
using Newton.Domain.Entities.Firestore;

namespace Newton.Infrastructure.Common.Firestore;

public class FirestoreProvider
{
	private readonly FirestoreDb _fireStoreDb = null!;

	public FirestoreProvider(FirestoreDb fireStoreDb)
	{
		_fireStoreDb = fireStoreDb;
	}

	private string GetCollectionName<T>()
	{
		var attrib = typeof(T)
			.GetCustomAttributes(typeof(FirestoreMapToCollectionAttribute), false)
			.Select(x => x as FirestoreMapToCollectionAttribute)
			.FirstOrDefault();

		return attrib is null ? typeof(T).Name : attrib.Name;

	}

	public async Task<bool> Add<T>(T entity, CancellationToken ct = default) where T : class, IFirestoreEntity
	{
		var collection = _fireStoreDb.Collection(GetCollectionName<T>());

		if (collection is null) return false;

		var document = await collection.AddAsync(entity, ct);

		if (document is null) return false;

		entity.Reference = document;

		await document.SetAsync(entity, cancellationToken: ct);

		return true;
	}

	public async Task<bool> Update<T>(T entity, CancellationToken ct = default) where T : class, IFirestoreEntity
	{
		if (entity.Reference is null)
		{
			throw new Exception("Firestore entity is missing reference when trying to update");
		}

		var collection = _fireStoreDb.Collection(GetCollectionName<T>());
		if (collection is null) return false;

		var document = collection.Document(entity.Reference.Id);

		if (document is null) return false;

		await document.SetAsync(entity, cancellationToken: ct);

		return true;
	}
	
	public async Task<T?> Get<T>(string id, CancellationToken ct = default) where T : class, IFirestoreEntity
	{
		var document = _fireStoreDb.Collection("users").Document(id);

		if (document is null) return null;

		var snapshot = await document.GetSnapshotAsync(ct);
		return snapshot.ConvertTo<T>();
	}
	public async Task<IReadOnlyCollection<T>?> GetSports<T>(DocumentReference userReference, CancellationToken ct = default) where T : class, IFirestoreEntity
	{
		var collection = _fireStoreDb.Collection("sports").WhereEqualTo("user", userReference);

		if (collection is null) return null;

		var snapshot = await collection.GetSnapshotAsync(ct);

		return snapshot?.Documents.Select(x => x.ConvertTo<T>()).ToList();
	}

	public async Task<IReadOnlyCollection<T>?> GetClubs<T>(DocumentReference userReference, CancellationToken ct = default) where T : class, IFirestoreEntity
	{
		var collection = _fireStoreDb.Collection("clubs").WhereEqualTo("user", userReference);

		if (collection is null) return null;

		var snapshot = await collection.GetSnapshotAsync(ct);

		return snapshot?.Documents.Select(x => x.ConvertTo<T>()).ToList();
	}

	public async Task<IReadOnlyCollection<T>?> GetJobs<T>(DocumentReference userReference, CancellationToken ct = default) where T : class, IFirestoreEntity
	{
		var collection = _fireStoreDb.Collection("jobs").WhereEqualTo("user", userReference);

		if (collection is null) return null;

		var snapshot = await collection.GetSnapshotAsync(ct);

		return snapshot?.Documents.Select(x => x.ConvertTo<T>()).ToList();
	}

	public async Task<IReadOnlyCollection<T>?> GetAllUsers<T>(CancellationToken ct = default) where T : class, IFirestoreEntity
	{
		var collection = _fireStoreDb.Collection("users");

		if (collection is null) return null;

		var snapshot = await collection.GetSnapshotAsync(ct);

		return snapshot?.Documents.Select(x => x.ConvertTo<T>()).ToList();
	}

	public async Task<IReadOnlyCollection<T>> WhereEqualTo<T>(string fieldPath, string value, CancellationToken ct = default) where T : class, IFirestoreEntity
	{
		return await GetList<T>(_fireStoreDb.Collection("sports").WhereEqualTo(fieldPath, value), ct);
	}

	// just add here any method you need here WhereGreaterThan, WhereIn etc ...

	private static async Task<IReadOnlyCollection<T>> GetList<T>(Query query, CancellationToken ct = default) where T : class, IFirestoreEntity
	{
		var snapshot = await query.GetSnapshotAsync(ct);
		return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
	}
}
