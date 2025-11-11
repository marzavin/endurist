using Endurist.Core.Activities.Models;
using Endurist.Data.Mongo.Documents;
using SideEffect.DataTransfer.Models;
using SideEffect.Extensions;
using SideEffect.Sport.Activities.Models;

namespace Endurist.Core.Activities.Mappers;

internal class SegmentMapper
{
    public static void Map(SegmentDocument document, SegmentModel model, List<TrackPoint> track)
    {
        model.StartTime = document.StartTime;
        model.Distance = document.Distance;
        model.Duration = document.Duration;
        model.Pace = document.Pace;
        model.AverageCadence = document.AverageCadence;
        model.AverageHeartRate = document.AverageHeartRate;

        var segmentTrack = track.Skip(document.StartIndex).Take(document.FinishIndex - document.StartIndex + 1).ToList();
        if (!segmentTrack.IsEmpty())
        {
            model.AltitudeGraph = [];
            model.CadenceGraph = [];
            model.HeartRateGraph = [];
            model.PaceGraph = [];

            var sortedPoints = segmentTrack.OrderBy(x => x.Timestamp).ToList();

            var startTime = segmentTrack.First().Timestamp;
            foreach (var point in sortedPoints)
            {
                var offset = (point.Timestamp - startTime).TotalMilliseconds;

                model.AltitudeGraph.Add(new KeyValueModel<double, double?> { Key = offset, Value = point.Altitude });
                model.CadenceGraph.Add(new KeyValueModel<double, int?> { Key = offset, Value = point.Cadence });
                model.HeartRateGraph.Add(new KeyValueModel<double, int?> { Key = offset, Value = point.HeartRate });
            }
        }
    }
}
