namespace SideEffect.Sport.Activities;

/// <summary>
/// Distance calculation settings.
/// </summary>
public class DistanceCalculationSettings
{
    /// <summary>
    /// Creates new instance of <see cref="DistanceCalculationSettings"/>.
    /// </summary>
    /// <param name="formula">See <see cref="DistanceFormula"/> for more information.</param>
    /// <param name="withAltitude">Flag of altitude should be taken into account.</param>
    public DistanceCalculationSettings(DistanceFormula formula, bool withAltitude)
    {
        Formula = formula;
        WithAltitude = withAltitude;
    }

    /// <summary>
    /// Returns default calculation settings.
    /// </summary>
    public static DistanceCalculationSettings Default => new DistanceCalculationSettings(DistanceFormula.Vincenty, false);

    /// <summary>
    /// Gets distance calculation formula.
    /// </summary>
    public DistanceFormula Formula { get; }

    /// <summary>
    /// Gets if altitude should be taken into account.
    /// </summary>
    public bool WithAltitude { get; }
}
