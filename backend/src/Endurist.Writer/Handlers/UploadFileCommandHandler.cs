using Endurist.Contracts.Files.Commands;
using Endurist.Core.Services;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using Endurist.Models;
using Endurist.Models.Files;
using MongoDB.Bson;
using SideEffect.Extensions;
using SideEffect.Files.XML;
using SideEffect.Messaging;
using SideEffect.Messaging.PubSub;

namespace Endurist.Writer.Handlers;

internal class UploadFileCommandHandler : EventHandlerBase<UploadFileCommand>
{
    private readonly Storage _storage;

    private readonly IMessageHubClient _hubClient;

    private readonly IEncryptionService _encryptionService;

    private readonly ILogger _logger;

    public UploadFileCommandHandler(
        Storage storage,
        IMessageHubClient hubClient, 
        IEncryptionService encryptionService, 
        ILogger<ProcessFileCommandHandler> logger)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        _hubClient = hubClient ?? throw new ArgumentNullException(nameof(hubClient));
        _encryptionService = encryptionService ?? throw new ArgumentNullException(nameof(encryptionService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task HandleAsync(UploadFileCommand message, CancellationToken cancellationToken = default)
    {
        var filePath = message.Path;

        var extension = Path.GetExtension(message.Name);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(message.Name);

        var file = new FileDocument
        {
            Name = fileNameWithoutExtension,
            Extension = string.IsNullOrWhiteSpace(extension) ? null : extension.Trim('.').ToLower(),
            ProfileId = message.UserId.IsEmpty() ? ObjectId.Empty : ObjectId.Parse(message.UserId),
            UploadedAt = DateTime.UtcNow
        };

        var contentResult = await LoadFileContentAsync(filePath, cancellationToken);
        if (contentResult.Error is null)
        {
            var hash = await _encryptionService.ComputeHashAsync(contentResult.Data, cancellationToken);

            file.Content = Convert.ToBase64String(contentResult.Data);
            file.Size = contentResult.Data.Length;
            file.Hash = hash;

            var existingFile = await _storage.Files.GetByHashAsync(hash);
            if (existingFile is null)
            {
                file.Status = FileStatus.Uploaded;
            }
            else 
            {
                file.Status = FileStatus.Duplicated;
                file.CopyOfId = existingFile.Id;

                _logger.LogWarning("File '{file}' is already uploaded to the database.", Path.GetFileName(message.Name));
            }
        }
        else
        {
            file.Status = FileStatus.UploadFailed;
            file.Error = contentResult.Error;
        }

        await _storage.Files.InsertAsync(file, cancellationToken);

        if (file.Status == FileStatus.Uploaded)
        {
            await InitiateFileProcessingAsync(message.UserId, file.EntityId, cancellationToken);
        }

        //if (System.IO.File.Exists(filePath))
        //{
        //    System.IO.File.Delete(filePath);
        //}

        //TODO: Move file to another folder (processed/failed)
    }

    private async Task InitiateFileProcessingAsync(string userId, string fileId, CancellationToken cancellationToken)
    {
        var processFileEvent = new ProcessFileCommand(userId, fileId);
        await _hubClient.PublishEventAsync(processFileEvent, cancellationToken);
    }

    private static async Task<OperationResult<byte[]>> LoadFileContentAsync(string filePath, CancellationToken cancellationToken)
    {
        try
        {
            var content = await File.ReadAllBytesAsync(filePath, cancellationToken);
            content = XmlHelper.Minify(content);
            return new OperationResult<byte[]> { Data = content };
        }
        catch (Exception ex)
        {
            return new OperationResult<byte[]> { Error = ex.Message };
        }
    }
}

