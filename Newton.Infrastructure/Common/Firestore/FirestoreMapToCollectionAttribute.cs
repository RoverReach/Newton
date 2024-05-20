namespace Newton.Infrastructure.Common.Firestore;

[AttributeUsage(AttributeTargets.Class)]
public class FirestoreMapToCollectionAttribute : Attribute
{
	private string _name = string.Empty;

	public string Name => _name;

	public FirestoreMapToCollectionAttribute(string name)
	{
		_name = name;
	}
}

