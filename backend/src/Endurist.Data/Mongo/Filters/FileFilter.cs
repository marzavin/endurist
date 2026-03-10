using Endurist.Models.Files;

namespace Endurist.Data.Mongo.Filters;

public class FileFilter
{
    public string IdEq { get; set; }

    public List<string> ProfileIdIn { get; set; }

    public List<FileStatus> StatusIn { get; set; }

    public string HashEq { get; set; }
}
