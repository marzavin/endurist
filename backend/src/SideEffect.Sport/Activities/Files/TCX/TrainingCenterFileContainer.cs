using SideEffect.Files.XML.TCX;
using SideEffect.Files.XML.TCX.Models;
using SideEffect.Sport.Activities.Models;
using Activity = SideEffect.Sport.Activities.Models.Activity;
using Position = SideEffect.Sport.Activities.Models.Position;
using TrackPoint = SideEffect.Sport.Activities.Models.TrackPoint;

namespace SideEffect.Sport.Activities.Files.TCX;

/// <summary>
/// Training Center XML file.
/// </summary>
public class TrainingCenterFileContainer : IActivityContainer
{
    /// <summary>
    /// Gets path to file with activities.
    /// </summary>
    protected string Path { get; private set; }

    /// <summary>
    /// Gets file content as byte array.
    /// </summary>
    protected byte[] Content { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TrainingCenterFileContainer"/> class.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    public TrainingCenterFileContainer(string path)
    {
        Path = path;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TrainingCenterFileContainer"/> class.
    /// </summary>
    /// <param name="content">File content as byte array.</param>
    public TrainingCenterFileContainer(byte[] content)
    {
        Content = content;
    }

    /// <summary>
    /// Loads data from .tcx file as <see cref="Activity"/> object.
    /// </summary>
    /// <returns><see cref="Activity"/> for more information.</returns>
    public async Task<List<Activity>> LoadAsync()
    {
        var file = new TrainingCenterFile();

        if (Content is not null)
        {
            await file.LoadAsync(Content);
        }
        else
        {
            await file.LoadAsync(Path);
        }

        return file.Content.Activities?.Select(MapToActivity).ToList() ?? [];
    }

    private Activity MapToActivity(SideEffect.Files.XML.TCX.Models.Activity source)
    {
        ArgumentNullException.ThrowIfNull(source);

        var trackPoints = source.Laps?.SelectMany(x => x.Track).OrderBy(x => x.Time).ToList() ?? [];
        if (trackPoints is null || trackPoints.Count == 0)
        {
            throw new Exception("Activity does not contain valid track points.");
        }

        var startTimestamp = trackPoints.First().Time;
        var startPosition = trackPoints.Where(x => x.Position is not null).FirstOrDefault()?.Position;

        var finishTimestamp = trackPoints.Last().Time;
        var finishPosition = trackPoints.Where(x => x.Position is not null).LastOrDefault()?.Position;

        var deviceName = source.Creator?.Name;

        var activityName = $"{source.Sport} (from {startTimestamp.ToLongTimeString()} to {finishTimestamp.ToLongTimeString()})";

        var track = trackPoints.Select(MapToTrackPoint).ToList();

        return new Activity
        {
            Name = activityName,
            Category = source.Sport.ToUpper(),
            Calories = source.Laps?.Sum(x => x.Calories),
            Distance = source.Laps?.Sum(x => x.DistanceMeters),
            Duration = source.Laps?.Sum(x => x.TotalTimeSeconds * 1000),
            Devices = string.IsNullOrEmpty(deviceName) ? null : [new Device { Name = deviceName }],
            Track = track,
            Segments = MapToSegments(source.Laps, track)
        };
    }

    private Position MapToPosition(SideEffect.Files.XML.TCX.Models.Position source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return new Position
        {
            Latitude = source.LatitudeDegrees,
            Longitude = source.LongitudeDegrees
        };
    }

    private TrackPoint MapToTrackPoint(SideEffect.Files.XML.TCX.Models.TrackPoint source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return new TrackPoint
        {
            Timestamp = source.Time,
            Altitude = source.AltitudeMeters,
            Cadence = source.Cadence,
            HeartRate = source.HeartRate,
            Position = source.Position is null ? null : MapToPosition(source.Position),
        };
    }

    private List<Segment> MapToSegments(List<Lap> laps, List<TrackPoint> track)
    {
        if (laps is null || laps.Count <= 1)
        {
            return null;
        }

        var segments = new List<Segment>();

        for (int i = 0; i < laps.Count; i++)
        {
            var startPoint = laps[i].Track.First();
            var finishPoint = i == laps.Count - 1 ? laps[i].Track.Last() : laps[i + 1].Track.First();

            var startIndex = track.IndexOf(track.First(x => x.Timestamp == startPoint.Time));
            var finishIndex = track.IndexOf(track.First(x => x.Timestamp == finishPoint.Time));

            segments.Add(new Segment { StartIndex = startIndex, FinishIndex = finishIndex });
        }

        return segments;
    }
}
