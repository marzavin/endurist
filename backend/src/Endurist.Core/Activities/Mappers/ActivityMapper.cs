using Endurist.Core.Activities.Models;
using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Interfaces;
using SideEffect.DataTransfer.Models;
using SideEffect.Extensions;

namespace Endurist.Core.Activities.Mappers;

internal static class ActivityMapper
{
    public static void Map(ActivityDocument document, ActivityModel model)
    {
        Map(document, model as ActivityPreviewModel);

        if (!document.Segments.IsEmpty())
        {
            model.Segments = [];

            var sortedSegments = document.Segments.OrderBy(x => x.StartIndex).ToList();
            foreach (var segment in sortedSegments)
            {
                var segmentModel = new SegmentPreviewModel();
                Map(segment, segmentModel);
                model.Segments.Add(segmentModel);
            }
        }

        if (!document.Track.IsEmpty())
        {        
            model.AltitudeGraph = [];
            model.CadenceGraph = [];
            model.HeartRateGraph = [];
            model.PaceGraph = [];

            var sortedPoints = document.Track.OrderBy(x => x.Timestamp).ToList();

            var startTime = document.Track.First().Timestamp;
            foreach (var point in sortedPoints)
            {
                var offset = (point.Timestamp - startTime).TotalMilliseconds;

                model.AltitudeGraph.Add(new KeyValueModel<double, double?> { Key = offset, Value = point.Altitude });
                model.CadenceGraph.Add(new KeyValueModel<double, int?> { Key = offset, Value = point.Cadence });
                model.HeartRateGraph.Add(new KeyValueModel<double, int?> { Key = offset, Value = point.HeartRate });
            }
        }
    }

    public static void Map(ActivityDocument document, ActivityPreviewModel model)
    {
        Map(document, model as SegmentPreviewModel);

        model.Id = document.Id.ToString();
        model.Category = document.Category;
        model.Calories = document.Calories;
    }

    public static void Map(ITrackSegment document, SegmentPreviewModel model)
    {
        model.StartTime = document.StartTime;
        model.Distance = document.Distance;
        model.Duration = document.Duration;
        model.Pace = document.Pace;
        model.AverageCadence = document.AverageCadence;
        model.AverageHeartRate = document.AverageHeartRate;
    }
}
