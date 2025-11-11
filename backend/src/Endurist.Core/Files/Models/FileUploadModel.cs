using Endurist.Data.Mongo.Enums;

namespace Endurist.Core.Files.Models;

public class FileUploadModel
{
    public string Name { get; set; }

    public long Size { get; set; }

    public FileStatus FileStatus { get; set; }
}
