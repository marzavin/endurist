using Endurist.Data.Mongo.Enums;

namespace Endurist.Data.Mongo.Filters;

public class FileFilter
{
    public List<string> ProfileIdIn { get; set; }

    public List<FileStatus> StatusIn { get; set; }

    public string HashEq { get; set; }
}
