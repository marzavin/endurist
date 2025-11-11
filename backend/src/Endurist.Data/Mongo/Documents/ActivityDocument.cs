using Endurist.Data.Mongo.Enums;
using Endurist.Data.Mongo.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SideEffect.Sport.Activities.Models;

namespace Endurist.Data.Mongo.Documents;

/// <summary>
/// Activity document.
/// </summary>
[BsonIgnoreExtraElements]
public class ActivityDocument : DocumentBase, ITrackSegment
{
    /// <summary>
    /// Gets or sets identifier of the source file.
    /// </summary>
    public ObjectId FileId { get; set; }

    /// <summary>
    /// Gets or sets unique identifier of the user profile.
    /// </summary>
    public ObjectId ProfileId { get; set; }

    /// <summary>
    /// Gets or sets activity category.
    /// </summary>
    public ActivityCategory Category { get; set; }

    /// <summary>
    /// Gets or seta activity type.
    /// </summary>
    public ActivityType Type { get; set; }

    /// <summary>
    /// Gets or sets the number of calories burned.
    /// </summary>
    public int? Calories { get; set; }

    /// <summary>
    /// Gets or sets the list of track points.
    /// </summary>
    public List<TrackPoint> Track { get; set; }

    /// <summary>
    /// Gets or sets the list of segments.
    /// </summary>
    public List<SegmentDocument> Segments { get; set; }

    #region ITrackSegment members

    /// <inheritdoc />
    public DateTime StartTime { get; set; }

    /// <inheritdoc />
    public DateTime FinishTime { get; set; }

    /// <inheritdoc />
    public double Duration { get; set; }

    /// <inheritdoc />
    public double Distance { get; set; }

    /// <inheritdoc />
    public double Pace { get; set; }

    /// <inheritdoc />
    public Position StartPosition { get; set; }

    /// <inheritdoc />
    public Position FinishPosition { get; set; }

    /// <inheritdoc />
    public int? MinHeartRate { get; set; }

    /// <inheritdoc />
    public int? AverageHeartRate { get; set; }

    /// <inheritdoc />
    public int? MaxHeartRate { get; set; }

    /// <inheritdoc />
    public int? MinCadence { get; set; }

    /// <inheritdoc />
    public int? AverageCadence { get; set; }

    /// <inheritdoc />
    public int? MaxCadence { get; set; }

    /// <inheritdoc />
    public double? MinAltitude { get; set; }

    /// <inheritdoc />
    public double? AverageAltitude { get; set; }

    /// <inheritdoc />
    public double? MaxAltitude { get; set; }

    /// <inheritdoc />
    public double? PositiveElevationGain { get; set; }

    /// <inheritdoc />
    public double? NegativeElevationGain { get; set; }

    #endregion
}
