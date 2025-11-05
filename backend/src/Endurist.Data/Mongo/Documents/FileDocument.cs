using Endurist.Data.Mongo.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Endurist.Data.Mongo.Documents;

[BsonIgnoreExtraElements]
public class FileDocument : DocumentBase
{
    public string Name { get; set; }

    public string Extension { get; set; }

    public string Content { get; set; }

    public int Size { get; set; }

    public string Hash { get; set; }

    public FileStatus Status { get; set; }

    public string Error { get; set; }

    public ObjectId ProfileId { get; set; }

    public DateTime UploadedAt { get; set; }

    public DateTime? ProcessedAt { get; set; }
}
