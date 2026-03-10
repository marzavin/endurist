namespace Endurist.Contracts.Files.Commands;

public sealed class ProcessFileCommand : CommandBase
{
    public ProcessFileCommand(string fileId)
    {
        FileId = fileId;
    }

    public string FileId { get; set; }
}
