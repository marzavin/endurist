using MongoDB.Bson.Serialization.Attributes;

namespace Endurist.Data.Mongo.Documents;

/// <summary>
/// Represents a document with user profile information.
/// </summary>
[BsonIgnoreExtraElements]
public class ProfileDocument : DocumentBase
{
    /// <summary>
    /// Gets or sets user profile name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets user heart rate zones.
    /// </summary>
    public List<HeartRateZoneDocument> HeartRateZones { get; set; }
}