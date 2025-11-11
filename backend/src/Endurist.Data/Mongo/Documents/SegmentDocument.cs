using Endurist.Data.Mongo.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using SideEffect.Sport.Activities.Models;

namespace Endurist.Data.Mongo.Documents;

/// <summary>
/// Track segment document.
/// </summary>
[BsonIgnoreExtraElements]
public class SegmentDocument : ITrackSegment
{
    /// <summary>
    /// Gets or sets start index of the segment.
    /// </summary>
    public int StartIndex { get; set; }

    /// <summary>
    /// Gets or sets finish index of the segemnt.
    /// </summary>
    public int FinishIndex { get; set; }

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
}
