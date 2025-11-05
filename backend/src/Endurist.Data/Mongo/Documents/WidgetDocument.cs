using MongoDB.Bson.Serialization.Attributes;

namespace Endurist.Data.Mongo.Documents;

[BsonIgnoreExtraElements]
public class WidgetDocument : DocumentBase
{
    /// <summary>
    /// Gets or sets widget name.
    /// </summary>
    public string Name { get; set; }
}
