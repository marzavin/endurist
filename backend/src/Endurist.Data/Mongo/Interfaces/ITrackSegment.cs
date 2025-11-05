using SideEffect.Sport.Activities.Models;

namespace Endurist.Data.Mongo.Interfaces;

/// <summary>
/// Represents track segment information.
/// </summary>
public interface ITrackSegment
{
    /// <summary>
    /// Gets or sets segment start time.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Gets or sets segment end time
    /// </summary>
    public DateTime FinishTime { get; set; }

    /// <summary>
    /// Gets or sets segment duration.
    /// </summary>
    public double Duration { get; set; }

    /// <summary>
    /// Gets or sets segment distance.
    /// </summary>
    public double Distance { get; set; }

    /// <summary>
    /// Gets or sets the average pace for this segment. 
    /// </summary>
    public double Pace { get; set; }

    /// <summary>
    /// Gets or sets segment start GPS point.
    /// </summary>
    public Position StartPosition { get; set; }

    /// <summary>
    /// Gets or sets segment finish GPS point.
    /// </summary>
    public Position FinishPosition { get; set; }

    /// <summary>
    /// Gets or sets minimum heart rate.
    /// </summary>
    public int? MinHeartRate { get; set; }

    /// <summary>
    /// Gets or sets average heart rate.
    /// </summary>
    public int? AverageHeartRate { get; set; }

    /// <summary>
    /// Gets or sets maximum heart rate.
    /// </summary>
    public int? MaxHeartRate { get; set; }

    /// <summary>
    /// Gets or sets minimum cadence.
    /// </summary>
    public int? MinCadence { get; set; }

    /// <summary>
    /// Gets or sets average cadence.
    /// </summary>
    public int? AverageCadence { get; set; }

    /// <summary>
    /// Gets or sets maximum cadence.
    /// </summary>
    public int? MaxCadence { get; set; }

    /// <summary>
    /// Gets or sets minimum altitude.
    /// </summary>
    public double? MinAltitude { get; set; }

    /// <summary>
    /// Gets or sets average altitude.
    /// </summary>
    public double? AverageAltitude { get; set; }

    /// <summary>
    /// Gets or sets maximum altitude.
    /// </summary>
    public double? MaxAltitude { get; set; }

    /// <summary>
    /// Gets or sets cumulative positive elevation gain.
    /// </summary>
    public double? PositiveElevationGain { get; set; }

    /// <summary>
    /// Gets or sets cumulative negative elevation gain.
    /// </summary>
    public double? NegativeElevationGain { get; set; }
}
