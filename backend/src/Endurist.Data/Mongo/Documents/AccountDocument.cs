using MongoDB.Bson.Serialization.Attributes;

namespace Endurist.Data.Mongo.Documents;

/// <summary>
/// Represents a document with user account information.
/// </summary>
[BsonIgnoreExtraElements]
public class AccountDocument : DocumentBase
{
    /// <summary>
    /// Gets or sets user account name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets salt for password.
    /// </summary>
    public string Salt { get; set; }

    /// <summary>
    /// Gets or sets user password hash.
    /// </summary>
    public string PasswordHash { get; set; }
}