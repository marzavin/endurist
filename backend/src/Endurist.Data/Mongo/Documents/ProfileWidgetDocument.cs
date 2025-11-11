using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Endurist.Data.Mongo.Documents;

/// <summary>
/// Represents a document with the data for profile widget.
/// </summary>
[BsonIgnoreExtraElements]
public class ProfileWidgetDocument : DocumentBase
{
    /// <summary>
    /// Gets or sets unique identifier of the user profile.
    /// </summary>
    public ObjectId ProfileId { get; set; }

    /// <summary>
    /// Gets or sets unique identifier of the widget.
    /// </summary>
    public ObjectId WidgetId { get; set; }

    /// <summary>
    /// Gets or sets string representation of data.
    /// </summary>
    public string Data { get; set; }
}
