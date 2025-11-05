using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Endurist.Data.Mongo.Documents;

/// <summary>
/// Represents a document with refresh token information.
/// </summary>
[BsonIgnoreExtraElements]
public class TokenDocument : DocumentBase
{
    /// <summary>
    /// Gets or sets user account identifier.
    /// </summary>
    public ObjectId AccountId { get; set; }

    /// <summary>
    /// Gets or sets refresh token.
    /// </summary>
    public string Token { get; set; }
}
