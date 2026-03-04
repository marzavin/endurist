namespace Endurist.Contracts.Commands;

public sealed class UploadFileCommand : CommandBase
{
    public string Path { get; set; }
}

