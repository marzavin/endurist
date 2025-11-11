namespace SideEffect.Sport.Activities.Models;

/// <summary>
/// Information about time result for specific distance.
/// </summary>
public class DistanceDurationInfo
{
    /// <summary>
    /// Gets or sets distance.
    /// </summary>
    public double Distance { get; set; }

    /// <summary>
    /// Gets or sets time duration.
    /// </summary>
    public double Duration { get; set; }

    /// <summary>
    /// Gets or sets start point index.
    /// </summary>
    public int StartIndex { get; set; }

    /// <summary>
    /// Gets or sets start point index.
    /// </summary>
    public int FinishIndex { get; set; }
}
