namespace Endurist.Models.Files;

public class FileUploadModel
{
    public string Name { get; set; }

    public long Size { get; set; }

    public FileStatus FileStatus { get; set; }
}
