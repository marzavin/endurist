using Endurist.Core.Events;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Enums;
using Endurist.Data.Mongo.Filters;
using Endurist.Data.Mongo.Interfaces;
using Endurist.Worker.Tasks.Profiles.TrainingVolume;
using SideEffect.Extensions;
using SideEffect.Messaging;
using SideEffect.Sport.Activities;
using SideEffect.Sport.Activities.Files.TCX;
using SideEffect.Sport.Activities.Models;

namespace Endurist.Worker;

internal class FileProcessingWorker : BackgroundService
{
    private const int Interval = 1000;

    private readonly Storage _storage;
    private readonly IServiceBus _serviceBus;
    private readonly ILogger<FileProcessingWorker> _logger;
    
    public FileProcessingWorker(Storage storage, IServiceBus serviceBus, ILogger<FileProcessingWorker> logger)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        _serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("File processing worker running at: {time}", DateTimeOffset.Now);
            }

            var fileToProcess = await GetFileToProcessAsync(cancellationToken);
            if (fileToProcess is not null)
            {
                var data = Convert.FromBase64String(fileToProcess.Content);
                var tcx = new TrainingCenterFileContainer(data);

                var activities = await tcx.LoadAsync();
                if (!activities.IsEmpty())
                {
                    await DeleteActivitiesFromFileAsync(fileToProcess.EntityId, cancellationToken);

                    foreach (var activity in activities)
                    {
                        await MapAndInsertActivityAsync(fileToProcess, activity, cancellationToken);
                    }
                }

                //TODO:AMZ: Process negative case here
                await SetProcessingResultAsync(fileToProcess, null, cancellationToken);
            }

            await Task.Delay(Interval, cancellationToken);
        }
    }

    private async Task<FileDocument> GetFileToProcessAsync(CancellationToken cancellationToken = default)
    {
        var filter = new FileFilter { StatusIn = [FileStatus.Uploaded] };
        var queryFilter = _storage.Files.BuildFilter(filter);

        return await _storage.Files.SetStatusAndReturnAsync(queryFilter, FileStatus.Processing, cancellationToken);
    }

    private async Task SetProcessingResultAsync(FileDocument file, string message, CancellationToken cancellationToken = default)
    {
        if (message.IsEmpty())
        {
            file.Status = FileStatus.Parsed;
        }
        else
        {
            file.Error = message;
            file.Status = FileStatus.ParsingFailed;
        }

        file.ProcessedAt = DateTime.UtcNow;

        await _storage.Files.UpdateAsync(file, cancellationToken);
    }

    private async Task DeleteActivitiesFromFileAsync(string fileId, CancellationToken cancellationToken = default)
    {
        var filter = new ActivityFilter { FileIdEq = fileId };
        var queryFilter = _storage.Activities.BuildFilter(filter);

        await _storage.Activities.DeleteAsync(queryFilter, cancellationToken);
    }

    private async Task MapAndInsertActivityAsync(FileDocument file, Activity activity, CancellationToken cancellationToken = default)
    {
        var category = TryParseActivityCategory(activity.Category);
        if (!category.HasValue)
        {
            _logger.LogWarning("Activity category '{category}' is not supported. Activity will be ignored.", activity.Category);
            return;
        }

        var track = activity.Track;

        var document = new ActivityDocument
        {
            FileId = file.Id,
            ProfileId = file.ProfileId,
            Type = ActivityType.Training,
            Category = category.Value,
            Calories = activity.Calories,
            Track = track,
            Segments = activity.Segments?.Select(x => MapSegment(x.StartIndex, x.FinishIndex, track)).ToList()
        };

        CalculateSegmentStatistics(document, track);

        await _storage.Activities.InsertAsync(document, cancellationToken);

        await _serviceBus.PublishEventAsync(new TrainingVolumeCalculationEvent { ProfileId = file.ProfileId.ToString() }, cancellationToken);
    }

    private static SegmentDocument MapSegment(int startIndex, int finishIndex, List<TrackPoint> track)
    {
        var segmentTrack = track.Skip(startIndex).Take(finishIndex - startIndex + 1).ToList();

        var segmentDocument = new SegmentDocument { StartIndex = startIndex, FinishIndex = finishIndex };

        CalculateSegmentStatistics(segmentDocument, segmentTrack);

        return segmentDocument;
    }

    private static void CalculateSegmentStatistics(ITrackSegment segment, List<TrackPoint> track)
    {
        var startPoint = track.First();
        var finishPoint = track.Last();

        segment.StartTime = startPoint.Timestamp;
        segment.StartPosition = startPoint.Position;
        segment.FinishTime = finishPoint.Timestamp;
        segment.FinishPosition = finishPoint.Position;
        segment.MaxAltitude = TrackCalculator.CalculateMaximumAltitude(track);
        segment.AverageAltitude = TrackCalculator.CalculateAverageAltitude(track);
        segment.MinAltitude = TrackCalculator.CalculateMinimumAltitude(track);
        segment.PositiveElevationGain = TrackCalculator.CalculateCumulativePositiveElevationGain(track);
        segment.NegativeElevationGain = TrackCalculator.CalculateCumulativeNegativeElevationGain(track);
        segment.MaxCadence = TrackCalculator.CalculateMaximumCadence(track);
        segment.AverageCadence = TrackCalculator.CalculateAverageCadence(track);
        segment.MinCadence = TrackCalculator.CalculateMinimumCadence(track);
        segment.MaxHeartRate = TrackCalculator.CalculateMaximumHeartRate(track);
        segment.AverageHeartRate = TrackCalculator.CalculateAverageHeartRate(track);
        segment.MinHeartRate = TrackCalculator.CalculateMinimumHeartRate(track);
        segment.Distance = TrackCalculator.CalculateTrackDistance(track);
        segment.Duration = (segment.FinishTime - segment.StartTime).TotalMilliseconds;
        segment.Pace = segment.Duration / segment.Distance * 1000;
    }

    private static ActivityCategory? TryParseActivityCategory(string category)
    {
        var supportedCategories = Enum.GetNames(typeof(ActivityCategory));
        var enumName = supportedCategories.FirstOrDefault(x => string.Equals(category, x, StringComparison.InvariantCultureIgnoreCase));
        return enumName is null ? null : (ActivityCategory?)Enum.Parse(typeof(ActivityCategory), enumName);
    }
}
