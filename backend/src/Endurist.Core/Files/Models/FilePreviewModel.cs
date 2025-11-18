using Endurist.Data.Mongo.Enums;

namespace Endurist.Core.Files.Models;

public class FilePreviewModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Extension { get; set; }

    public int Size { get; set; }

    public FileStatus Status { get; set; }

    public DateTime UploadedAt { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public DateTime? ActivityStartedAt { get; set; }
}
