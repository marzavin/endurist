namespace Endurist.Contracts.Files.Commands;

public sealed class UploadFileCommand : CommandBase
{
    public UploadFileCommand(string name, string path)
    {
        Name = name;
        Path = path;
    }

    public string Name { get; set; }

    public string Path { get; set; }
}

