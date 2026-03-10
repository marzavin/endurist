namespace Endurist.Contracts.Files.Commands;

public sealed class ProcessFileCommand : CommandBase
{
    public ProcessFileCommand(string userId, string fileId)
        : base(userId)
    {
        FileId = fileId;
    }

    public string FileId { get; set; }
}
