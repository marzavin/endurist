using SideEffect.Sport.Activities.Models;

namespace SideEffect.Sport.Activities;

/// <summary>
/// Calculator for track statistics.
/// </summary>
public static class TrackCalculator
{
    #region Heart Rate

    /// <summary>
    /// Calculates maximum heart rate.
    /// </summary>
    /// <param name="track">List of <see cref="TrackPoint"/>.</param>
    /// <returns>Maximum heart rate.</returns>
    public static int? CalculateMaximumHeartRate(List<TrackPoint> track)
    {
        var points = track?.Where(x => x.HeartRate.HasValue && x.HeartRate.Value > 0).ToList();
        if (points == null || points.Count == 0)
        {
            return null;
        }

        return points.Max(x => x.HeartRate);
    }

    /// <summary>
    /// Calculates average heart rate.
    /// </summary>
    /// <param name="track">List of <see cref="TrackPoint"/>.</param>
    /// <returns>Average heart rate.</returns>
    public static int? CalculateAverageHeartRate(List<TrackPoint> track)
    {
        var points = track?.Where(x => x.HeartRate.HasValue && x.HeartRate.Value > 0).ToList();
        if (points == null || points.Count == 0)
        {
            return null;
        }

        return points.Sum(x => x.HeartRate) / points.Count;
    }

    /// <summary>
    /// Calculates minimum heart rate.
    /// </summary>
    /// <param name="track">List of <see cref="TrackPoint"/>.</param>
    /// <returns>Minimum heart rate.</returns>
    public static int? CalculateMinimumHeartRate(List<TrackPoint> track)
    {
        var points = track?.Where(x => x.HeartRate.HasValue && x.HeartRate.Value > 0).ToList();
        if (points == null || points.Count == 0)
        {
            return null;
        }

        return points.Min(x => x.HeartRate);
    }

    #endregion

    #region Cadence

    /// <summary>
    /// Calculates maximum cadence.
    /// </summary>
    /// <param name="track">List of <see cref="TrackPoint"/>.</param>
    /// <returns>Maximum cadence.</returns>
    public static int? CalculateMaximumCadence(List<TrackPoint> track)
    {
        var points = track?.Where(x => x.Cadence.HasValue && x.Cadence.Value > 0).ToList();
        if (points == null || points.Count == 0)
        {
            return null;
        }

        return points.Max(x => x.Cadence);
    }

    /// <summary>
    /// Calculates average cadence.
    /// </summary>
    /// <param name="track">List of <see cref="TrackPoint"/>.</param>
    /// <returns>Average cadence.</returns>
    public static int? CalculateAverageCadence(List<TrackPoint> track)
    {
        var points = track?.Where(x => x.Cadence.HasValue && x.Cadence.Value > 0).ToList();
        if (points == null || points.Count == 0)
        {
            return null;
        }

        return points.Sum(x => x.Cadence) / points.Count;
    }

    /// <summary>
    /// Calculates minimum cadence.
    /// </summary>
    /// <param name="track">List of <see cref="TrackPoint"/>.</param>
    /// <returns>Minimum cadence.</returns>
    public static int? CalculateMinimumCadence(List<TrackPoint> track)
    {
        var points = track?.Where(x => x.Cadence.HasValue && x.Cadence.Value > 0).ToList();
        if (points == null || points.Count == 0)
        {
            return null;
        }

        return points.Min(x => x.Cadence);
    }

    #endregion

    #region Altitude

    /// <summary>
    /// Calculates maximum altitude.
    /// </summary>
    /// <param name="track">List of <see cref="TrackPoint"/>.</param>
    /// <returns>Maximum altitude of the track.</returns>
    public static double? CalculateMaximumAltitude(List<TrackPoint> track)
    {
        var points = track?.Where(x => x.Altitude.HasValue).ToList();
        if (points == null || points.Count == 0)
        {
            return null;
        }

        return points.Max(x => x.Altitude);
    }

    /// <summary>
    /// Calculates average altitude.
    /// </summary>
    /// <param name="track">List of <see cref="TrackPoint"/>.</param>
    /// <returns>Average altitude of the track.</returns>
    public static double? CalculateAverageAltitude(List<TrackPoint> track)
    {
        var points = track?.Where(x => x.Altitude.HasValue).ToList();
        if (points == null || points.Count == 0)
        {
            return null;
        }

        return points.Sum(x => x.Altitude) / points.Count;
    }

    /// <summary>
    /// Calculates minimum altitude.
    /// </summary>
    /// <param name="track">List of <see cref="TrackPoint"/>.</param>
    /// <returns>Minimum altitude of the track.</returns>
    public static double? CalculateMinimumAltitude(List<TrackPoint> track)
    {
        var points = track?.Where(x => x.Altitude.HasValue).ToList();
        if (points == null || points.Count == 0)
        {
            return null;
        }

        return points.Min(x => x.Altitude);
    }

    /// <summary>
    /// Calculates cumulative positive elevation gain.
    /// </summary>
    /// <param name="track">List of <see cref="TrackPoint"/>.</param>
    /// <returns>Cumulative positive elevation gain.</returns>
    public static double? CalculateCumulativePositiveElevationGain(List<TrackPoint> track)
    {
        var points = track?.OrderBy(x => x.Timestamp).Where(x => x.Altitude.HasValue).ToList();
        if (points == null || points.Count < 2)
        {
            return null;
        }

        var cumulativeSum = 0D;

        for (var i = 0; i < points.Count - 1; i++)
        {
            if (points[i].Altitude.Value < points[i + 1].Altitude.Value)
            {
                cumulativeSum += points[i + 1].Altitude.Value - points[i].Altitude.Value;
            }
        }

        return cumulativeSum;
    }

    /// <summary>
    /// Calculates cumulative negative elevation gain.
    /// </summary>
    /// <param name="track">List of <see cref="TrackPoint"/>.</param>
    /// <returns>Cumulative negative elevation gain.</returns>
    public static double? CalculateCumulativeNegativeElevationGain(List<TrackPoint> track)
    {
        var points = track?.OrderBy(x => x.Timestamp).Where(x => x.Altitude.HasValue).ToList();
        if (points == null || points.Count < 2)
        {
            return null;
        }

        var cumulativeSum = 0D;

        for (var i = 0; i < points.Count - 1; i++)
        {
            if (points[i].Altitude.Value > points[i + 1].Altitude.Value)
            {
                cumulativeSum += points[i].Altitude.Value - points[i + 1].Altitude.Value;
            }
        }

        return cumulativeSum;
    }

    #endregion

    /// <summary>
    /// Calculates distance between two GPS points.
    /// </summary>
    /// <param name="startPoint">Start point.</param>
    /// <param name="finishPoint">Finish point.</param>
    /// <param name="settings">See <see cref="DistanceCalculationSettings"/> for more informaiton.</param>
    /// <returns>Distance between two GPS points in metres.</returns>
    public static double CalculateDistanceBetweenCoordinates(Position startPoint, Position finishPoint, DistanceCalculationSettings settings = null)
    {
        ArgumentNullException.ThrowIfNull(startPoint);
        ArgumentNullException.ThrowIfNull(finishPoint);

        var calculationSettings = settings ?? DistanceCalculationSettings.Default;

        var result = 0D;

        switch (calculationSettings.Formula)
        {
            case DistanceFormula.Haversine:
                result = CalculateDistanceUsingHarvesineAlgorithm(startPoint, finishPoint);
                break;
            case DistanceFormula.Vincenty:
                result = CalculateDistanceUsingVincentyAlgorithm(startPoint, finishPoint);
                break;
        }

        return result;
    }

    /// <summary>
    /// Calculates distance between two <see cref="TrackPoint"/>.
    /// </summary>
    /// <param name="startPoint">Start point.</param>
    /// <param name="finishPoint">Finish point.</param>
    /// <param name="settings">See <see cref="DistanceCalculationSettings"/> for more informaiton.</param>
    /// <returns>Distance between two GPS <see cref="TrackPoint"/> in metres.</returns>
    public static double CalculateDistanceBetweenTrackPoints(TrackPoint startPoint, TrackPoint finishPoint, DistanceCalculationSettings settings = null)
    {
        ArgumentNullException.ThrowIfNull(startPoint);
        ArgumentNullException.ThrowIfNull(finishPoint);

        var calculationSettings = settings ?? DistanceCalculationSettings.Default;

        var coordinatesDistance = CalculateDistanceBetweenCoordinates(startPoint.Position, finishPoint.Position, calculationSettings);

        return calculationSettings.WithAltitude && startPoint.Altitude.HasValue && finishPoint.Altitude.HasValue
            ? Math.Sqrt(Math.Pow(Math.Abs(finishPoint.Altitude.Value - startPoint.Altitude.Value), 2D) + Math.Pow(coordinatesDistance, 2D))
            : coordinatesDistance;
    }

    /// <summary>
    /// Calculates full distance of the track.
    /// </summary>
    /// <param name="track">List of <see cref="TrackPoint"/>.</param>
    /// <param name="settings">See <see cref="DistanceCalculationSettings"/> for more informaiton.</param>
    /// <returns>Full distance of the track.</returns>
    /// <exception cref="ArgumentNullException">Track parameter is 'null'.</exception>
    public static double CalculateTrackDistance(List<TrackPoint> track, DistanceCalculationSettings settings = null)
    {
        ArgumentNullException.ThrowIfNull(track);

        var calculationSettings = settings ?? DistanceCalculationSettings.Default;

        var distance = 0D;

        var points = track.Where(x => x.Position is not null).OrderBy(x => x.Timestamp).ToList();

        if (points.Count < 2)
        {
            return distance;
        }

        for (var i = 1; i < points.Count; i++)
        {
            distance += CalculateDistanceBetweenTrackPoints(points[i - 1], points[i], calculationSettings);
        }

        return distance;
    }

    /// <summary>
    /// Calculates best result for the specific distance length.
    /// </summary>
    /// <param name="track">List of <see cref="TrackPoint"/>.</param>
    /// <param name="distance">Distance to calculate best result.</param>
    /// <param name="settings">See <see cref="DistanceCalculationSettings"/> for more informaiton.</param>
    /// <returns>See <see cref="DistanceDurationInfo"/> for more information.</returns>
    /// <exception cref="ArgumentNullException">Track parameter is 'null'.</exception>
    public static DistanceDurationInfo CalculateDistanceBestResult(List<TrackPoint> track, double distance, DistanceCalculationSettings settings = null)
    {
        ArgumentNullException.ThrowIfNull(track);

        DistanceDurationInfo bestResult = null;

        var startIndex = 0;
        int finishIndex;

        while (startIndex < track.Count - 1)
        {
            finishIndex = startIndex + 1;

            var segmentDistance = 0D;

            double lastSegmentDistance = CalculateDistanceBetweenTrackPoints(track[startIndex], track[startIndex + 1], settings);
            double lastSegmentDuration = (track[startIndex + 1].Timestamp - track[startIndex].Timestamp).TotalMilliseconds;

            for (; finishIndex < track.Count && segmentDistance < distance; finishIndex++)
            {
                lastSegmentDistance = CalculateDistanceBetweenTrackPoints(track[finishIndex - 1], track[finishIndex], settings);
                lastSegmentDuration = (track[finishIndex].Timestamp - track[finishIndex - 1].Timestamp).TotalMilliseconds;

                segmentDistance += lastSegmentDistance;
            }

            if (segmentDistance < distance)
            {
                break;
            }

            var result = new DistanceDurationInfo
            {
                StartIndex = startIndex,
                FinishIndex = finishIndex,
                Distance = distance,
                Duration = (track[finishIndex].Timestamp - track[startIndex].Timestamp).TotalMilliseconds
            };

            if (segmentDistance != distance)
            {
                var timeToCut = (segmentDistance - distance) / lastSegmentDistance * lastSegmentDuration;
                result.Duration -= timeToCut;
            }

            if (bestResult is null || result.Duration < bestResult.Duration)
            {
                bestResult = result;
            }

            startIndex++;
        }

        finishIndex = track.Count - 1;

        while (finishIndex > 0)
        {
            startIndex = finishIndex - 1;

            var segmentDistance = 0D;

            var lastSegmentDistance = CalculateDistanceBetweenTrackPoints(track[finishIndex - 1], track[finishIndex], settings);
            var lastSegmentDuration = (track[finishIndex].Timestamp - track[finishIndex - 1].Timestamp).TotalMilliseconds;

            for (; startIndex >= 0 && segmentDistance < distance; startIndex--)
            {
                lastSegmentDistance = CalculateDistanceBetweenTrackPoints(track[startIndex], track[startIndex + 1], settings);
                lastSegmentDuration = (track[startIndex + 1].Timestamp - track[startIndex].Timestamp).TotalMilliseconds;

                segmentDistance += lastSegmentDistance;
            }

            if (segmentDistance < distance)
            {
                break;
            }

            var result = new DistanceDurationInfo
            {
                StartIndex = startIndex,
                FinishIndex = finishIndex,
                Distance = distance,
                Duration = (track[finishIndex].Timestamp - track[startIndex].Timestamp).TotalMilliseconds
            };

            if (segmentDistance != distance)
            {
                var timeToCut = (segmentDistance - distance) / lastSegmentDistance * lastSegmentDuration;
                result.Duration -= timeToCut;
            }

            if (bestResult is null || result.Duration < bestResult.Duration)
            {
                bestResult = result;
            }

            finishIndex--;
        }

        return bestResult;
    }

    private static double ConvertDegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180D;
    }

    private static double CalculateDistanceUsingHarvesineAlgorithm(Position startPoint, Position finishPoint)
    {
        ArgumentNullException.ThrowIfNull(startPoint);
        ArgumentNullException.ThrowIfNull(finishPoint);

        var startLatitudeRadians = ConvertDegreesToRadians(startPoint.Latitude);
        var finishLatitudeRadians = ConvertDegreesToRadians(finishPoint.Latitude);
        var startLongitudeRadians = ConvertDegreesToRadians(startPoint.Longitude);
        var finishLongitudeRadians = ConvertDegreesToRadians(finishPoint.Longitude);

        var latitudeDiff = finishLatitudeRadians - startLatitudeRadians;
        var longitudeDiff = finishLongitudeRadians - startLongitudeRadians;

        var a = Math.Pow(Math.Sin(latitudeDiff / 2D), 2D)
            + Math.Pow(Math.Sin(longitudeDiff / 2D), 2D) * Math.Cos(startLatitudeRadians) * Math.Cos(finishLatitudeRadians);

        var angle = Math.Asin(Math.Sqrt(a));

        return 2D * Constants.Earth.AverageRadius * 1000D * angle;
    }

    private static double CalculateDistanceUsingVincentyAlgorithm(Position startPoint, Position finishPoint, double precision = 1e-12)
    {
        const int iterationLimit = 250;

        var a = Constants.Earth.WGS84.SemiMajorAxis;
        var b = Constants.Earth.WGS84.SemiMinorAxis;
        var f = 1 / Constants.Earth.WGS84.Flattening;

        var startLatitudeRadians = ConvertDegreesToRadians(startPoint.Latitude);
        var finishLatitudeRadians = ConvertDegreesToRadians(finishPoint.Latitude);
        var longitudeDifferenceRadians = ConvertDegreesToRadians(finishPoint.Longitude - startPoint.Longitude);

        var U1 = Math.Atan((1.0D - f) * Math.Tan(startLatitudeRadians));
        var U2 = Math.Atan((1.0D - f) * Math.Tan(finishLatitudeRadians));

        var sinU1 = Math.Sin(U1);
        var cosU1 = Math.Cos(U1);
        var sinU2 = Math.Sin(U2);
        var cosU2 = Math.Cos(U2);

        var sinU1sinU2 = sinU1 * sinU2;
        var cosU1sinU2 = cosU1 * sinU2;
        var sinU1cosU2 = sinU1 * cosU2;
        var cosU1cosU2 = cosU1 * cosU2;

        var lambda = longitudeDifferenceRadians;

        double previousLambda;
        var iteration = 0;

        var sigma = 0D;
        var sinSigma = 0D;
        var cosSigma = 0D;
        var sinAlpha = 0D;
        var cosSqAlpha = 0D;
        var cos2SigmaM = 0D;
        var C = 0D;

        do
        {
            var sinLambda = Math.Sin(lambda);
            var cosLambda = Math.Cos(lambda);

            sinSigma = Math.Sqrt(Math.Pow(cosU2 * sinLambda, 2D) + Math.Pow(cosU1sinU2 - (sinU1cosU2 * cosLambda), 2D));
            if (sinSigma == 0D)
            {
                return 0D;
            }

            cosSigma = sinU1sinU2 + (cosU1cosU2 * cosLambda);
            sigma = Math.Atan2(sinSigma, cosSigma);


            sinAlpha = cosU1cosU2 * sinLambda / sinSigma;
            cosSqAlpha = 1D - Math.Pow(sinAlpha, 2D);
            cos2SigmaM = Math.Abs(cosSqAlpha) < precision ? 0D : cosSigma - (2D * sinU1sinU2 / cosSqAlpha);

            C = (f / 16D) * cosSqAlpha * (4D + f * (4D - 3D * cosSqAlpha));

            previousLambda = lambda;
            lambda = longitudeDifferenceRadians + (1D - C) * f * sinAlpha *
                (sigma + C * sinSigma * (cos2SigmaM + C * cosSigma * (-1D + 2D * Math.Pow(cos2SigmaM, 2D))));

            iteration++;

        }
        while (Math.Abs(lambda - previousLambda) > precision && iteration < iterationLimit);

        if (iteration >= iterationLimit)
        {
            throw new ApplicationException("Vincenty's formula failed to converge.");
        }

        var uSq = cosSqAlpha * (Math.Pow(a, 2D) - Math.Pow(b, 2D)) / Math.Pow(b, 2D);

        var A = 1D + uSq / 16384D * (4096D + uSq * (-768D + uSq * (320D - 175D * uSq)));
        var B = uSq / 1024D * (256D + uSq * (-128D + uSq * (74D - 47D * uSq)));

        double deltaSigma = B * sinSigma * (cos2SigmaM + (B / 4D) * (cosSigma * (-1D + 2D * Math.Pow(cos2SigmaM, 2D)) -
            (B / 6D) * cos2SigmaM * (-3D + 4D * sinSigma * sinSigma) * (-3D + 4D * Math.Pow(cos2SigmaM, 2D))));

        return b * A * (sigma - deltaSigma);
    }
}
