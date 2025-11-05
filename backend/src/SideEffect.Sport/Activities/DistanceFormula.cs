namespace SideEffect.Sport.Activities;

/// <summary>
/// Distance calculation algorithm.
/// </summary>
public enum DistanceFormula
{
    /// <summary>
    /// A less precise but faster formula representing the Earth as a sphere.
    /// </summary>
    Haversine = 1,

    /// <summary>
    /// An accurate but slow formula representing the Earth as an ellipsoid.
    /// </summary>
    Vincenty = 2
}
