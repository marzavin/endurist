namespace SideEffect.Sport.Activities;

/// <summary>
/// Distance calculation type.
/// </summary>
public enum DistanceType
{
    /// <summary>
    /// Distance between points without altitude.
    /// </summary>
    Flat = 1,

    /// <summary>
    /// Distance between points with altitude (if possible).
    /// </summary>
    WithAltitude = 2
}
