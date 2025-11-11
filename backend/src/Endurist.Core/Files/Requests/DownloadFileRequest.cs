using Endurist.Common.Models;
using Endurist.Core.Files.Models;
using MediatR;

namespace Endurist.Core.Files.Requests;

public class DownloadFileRequest : IRequest<DataResponse<FileDownloadModel>>
{
    public DownloadFileRequest(string fileId)
    {
        FileId = fileId;
    }

    public string FileId { get; set; }
}
