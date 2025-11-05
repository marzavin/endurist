using MongoDB.Bson.Serialization.Attributes;

namespace Endurist.Data.Mongo.Documents;

[BsonIgnoreExtraElements]
public class HeartRateZoneDocument
{
    public int HeartRate { get; set; }

    public string Name { get; set; }
}
