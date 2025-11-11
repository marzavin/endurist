using Endurist.Common.Models;
using Endurist.Core.Files.Models;
using MediatR;

namespace Endurist.Core.Files.Requests;

public class UploadFileRequest : IRequest<DataResponse<FileUploadModel>>
{
    public UploadFileRequest(string name, string filePath)
    {
        Name = name;
        FilePath = filePath;
    }

    public string Name { get; set; }

    public string FilePath { get; set; }
}
