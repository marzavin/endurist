using Endurist.Common.Models;
using Endurist.Core.Files.Models;
using Endurist.Core.Files.Requests;
using Endurist.Core.Services;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Enums;
using Endurist.Data.Mongo.Filters;
using MediatR;
using MongoDB.Bson;
using System.Linq.Expressions;
using ExecutionContext = Endurist.Core.Services.ExecutionContext;

namespace Endurist.Core.Files;

public class FileHandler : HandlerBase,
    IRequestHandler<GetFilesRequest, DataPageResponse<FilePreviewModel>>,
    IRequestHandler<UploadFileRequest, DataResponse<FileUploadModel>>,
    IRequestHandler<DownloadFileRequest, DataResponse<FileDownloadModel>>
{
    protected ExecutionContext Context { get; }
    private IEncryptionService EncryptionService { get; }
    
    public FileHandler(Storage storage, ExecutionContext context, IEncryptionService encryptionService)
        : base(storage)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        EncryptionService = encryptionService ?? throw new ArgumentNullException(nameof(encryptionService));
    }

    public async Task<DataPageResponse<FilePreviewModel>> Handle(GetFilesRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<FileDocument, FilePreviewModel>> mapper = document =>
            new FilePreviewModel
            {
                Id = document.Id.ToString(),
                Name = document.Name,
                Extension = document.Extension,
                Size = document.Size,
                Status = document.Status,
                UploadedAt = document.UploadedAt,
                ProcessedAt = document.ProcessedAt,
                ActivityStartedAt = document.ActivityStartedAt
            };

        var filter = new FileFilter { ProfileIdIn = [Context.UserId] };
        var queryFilter = Storage.Files.BuildFilter(filter);

        var items = await Storage.Files.SearchAsync(mapper, queryFilter, request.Paging, request.Sorting, cancellationToken);
        return new DataPageResponse<FilePreviewModel> { Data = items, Paging = request.Paging };
    }

    public async Task<DataResponse<FileUploadModel>> Handle(UploadFileRequest request, CancellationToken cancellationToken)
    {
        var extension = Path.GetExtension(request.Name);

        var operationResult = await LoadFileContentAsync(request.FilePath, cancellationToken);
        var hash = await EncryptionService.ComputeHashAsync(operationResult.Data, cancellationToken);

        var filter = new FileFilter { HashEq = hash };
        var queryFilter = Storage.Files.BuildFilter(filter);
        var file = await Storage.Files.GetFirstOrDefaultAsync(queryFilter, cancellationToken);
        if (file is not null)
        {
            return new DataResponse<FileUploadModel>
            {
                Data = new FileUploadModel
                {
                    Name = file.Name,
                    Size = file.Size,
                    FileStatus = file.Status
                }
            };
        }

        file = new FileDocument
        {
            Name = Path.GetFileNameWithoutExtension(request.Name),
            Extension = string.IsNullOrWhiteSpace(extension) ? null : extension.Trim('.').ToLower(),
            ProfileId = ObjectId.Parse(Context.UserId)
        };
      
        if (operationResult.Error is null)
        {
            file.Status = FileStatus.Uploaded;
            file.Content = Convert.ToBase64String(operationResult.Data);
            file.Size = operationResult.Data.Length;
            file.Hash = hash;
        }
        else
        {
            file.Status = FileStatus.UploadFailed;
            file.Error = operationResult.Error;
        }

        file.UploadedAt = DateTime.UtcNow;

        await Storage.Files.InsertAsync(file, cancellationToken);

        var fileInfo = new FileInfo(request.FilePath);

        return new DataResponse<FileUploadModel>
        {
            Data = new FileUploadModel
            {
                Name = request.Name,
                Size = fileInfo.Length,
                FileStatus = FileStatus.Uploaded
            }
        };
    }

    public async Task<DataResponse<FileDownloadModel>> Handle(DownloadFileRequest request, CancellationToken cancellationToken)
    {
        var file = await Storage.Files.GetByIdAsync(request.FileId, cancellationToken);
        //TODO: Process negative cases here (file not found or can not be parsed)
        var filePath = Path.GetTempFileName();

        using (var memoryStream = new MemoryStream())
        {
            await memoryStream.WriteAsync(Convert.FromBase64String(file.Content), cancellationToken);
            memoryStream.Seek(0, SeekOrigin.Begin);

            using var stream = File.Create(filePath);
            await memoryStream.CopyToAsync(stream, cancellationToken);
        }

        var data = new FileDownloadModel { Name = file.Name, Extension = file.Extension, FilePath = filePath };

        return new DataResponse<FileDownloadModel> { Data = data };
    }

    private static async Task<OperationResult<byte[]>> LoadFileContentAsync(string filePath, CancellationToken cancellationToken)
    {
        try
        {
            var content = await File.ReadAllBytesAsync(filePath, cancellationToken);
            return new OperationResult<byte[]> { Data = content };
        }
        catch (Exception ex)
        {
            return new OperationResult<byte[]> { Error = ex.Message };
        }
    }
}
