using Endurist.Common.Models;
using Endurist.Core.Services;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Enums;
using Endurist.Hosting.Settings;
using MongoDB.Bson;
using SideEffect.Extensions;

namespace Endurist.Worker;

internal class FileParsingWorker : BackgroundService
{
    private const int Interval = 1500;

    private readonly Storage _storage;
    private readonly FileStorageConfiguration _configuration;
    private readonly IEncryptionService _encryptionService;
    private readonly ILogger<FileParsingWorker> _logger;

    public FileParsingWorker(
        Storage storage,
        FileStorageConfiguration configuration,
        IEncryptionService encryptionService,
        ILogger<FileParsingWorker> logger)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _encryptionService = encryptionService ?? throw new ArgumentNullException(nameof(encryptionService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(Interval, cancellationToken);

            _logger.LogInformation("File parsing worker running at: {time}", DateTimeOffset.Now);

            if (_configuration.Path.IsEmpty() || !Directory.Exists(_configuration.Path))
            {
                _logger.LogError("File storage path is invalid.");
                continue;
            }

            var filePath = Directory.GetFiles(_configuration.Path, "*.tcx").FirstOrDefault();
            if (filePath.IsEmpty())
            {
                _logger.LogInformation("There are no new files to upload.");
                continue;
            }

            var extension = Path.GetExtension(filePath);

            var file = new FileDocument
            {
                Name = Path.GetFileNameWithoutExtension(filePath),
                Extension = string.IsNullOrWhiteSpace(extension) ? null : extension.Trim('.').ToLower(),
                ProfileId = ObjectId.Empty
            };

            var contentResult = await LoadFileContentAsync(filePath, cancellationToken);
            if (contentResult.Error is null)
            {
                var hash = await _encryptionService.ComputeHashAsync(contentResult.Data, cancellationToken);

                if (await _storage.Files.DocumentExistsByHashAsync(hash))
                {
                    _logger.LogWarning("File '{file}' is already uploaded to the database.", Path.GetFileName(filePath));
                }

                file.Status = FileStatus.Uploaded;
                file.Content = Convert.ToBase64String(contentResult.Data);
                file.Size = contentResult.Data.Length;
                file.Hash = hash;
            }
            else
            {
                file.Status = FileStatus.UploadFailed;
                file.Error = contentResult.Error;
            }

            file.UploadedAt = DateTime.UtcNow;

            await _storage.Files.InsertAsync(file, cancellationToken);

            File.Delete(filePath);
        }
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
