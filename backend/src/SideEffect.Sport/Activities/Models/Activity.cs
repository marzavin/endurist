namespace SideEffect.Sport.Activities.Models;

/// <summary>
/// Information about activity.
/// </summary>
public class Activity
{
    /// <summary>
    /// Gets or sets name of activity.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets type of activity.
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Gets or sets list of activity track points.
    /// </summary>
    public List<TrackPoint> Track { get; set; }

    /// <summary>
    /// Gets or sets list of activity segments.
    /// </summary>
    public List<Segment> Segments { get; set; }

    /// <summary>
    /// Gets or sets list of activity devices.
    /// </summary>
    public List<Device> Devices { get; set; }

    /// <summary>
    /// Gets or sets number of calories burned for activity.
    /// </summary>
    public int? Calories { get; set; }

    /// <summary>
    /// Gets or sets total activity distance.
    /// </summary>
    public double? Duration { get; set; }

    /// <summary>
    /// Gets or sets total activity duration.
    /// </summary>
    public double? Distance { get; set; }
}
