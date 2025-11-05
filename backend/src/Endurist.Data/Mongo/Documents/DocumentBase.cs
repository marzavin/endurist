using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Endurist.Data.Mongo.Documents;

/// <summary>
/// Base class for all root entities in document storage.
/// </summary>
[BsonIgnoreExtraElements]
public abstract class DocumentBase
{
    /// <summary>
    /// Gets or sets the unique document identifier.
    /// </summary>
    [BsonId]
    public ObjectId Id { get; set; }

    /// <summary>
    /// Gets or sets the unique document identifier (as string).
    /// </summary>
    [BsonIgnore]
    public string EntityId
    {
        get => Id == ObjectId.Empty ? null : Id.ToString();
        set => Id = string.IsNullOrEmpty(value) ? ObjectId.Empty : new ObjectId(value);
    }
}
