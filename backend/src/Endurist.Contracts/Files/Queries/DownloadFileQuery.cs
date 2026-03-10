using Endurist.Models.Files;

namespace Endurist.Contracts.Files.Queries;

public class DownloadFileQuery : QueryBase<QueryReply<FileDownloadModel>>
{
    public DownloadFileQuery(string userId, string fileId)
        : base(userId)
    {
        FileId = fileId;
    }

    public string FileId { get; set; }
}
