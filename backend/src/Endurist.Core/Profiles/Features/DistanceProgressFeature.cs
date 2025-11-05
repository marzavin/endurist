using Endurist.Core.Profiles.Models;
using Endurist.Data.Mongo.Documents;
using SideEffect.DataTransfer.Models;
using SideEffect.Extensions;

namespace Endurist.Core.Profiles.Features;

public class DistanceProgressFeature : IWidgetFeature<DistanceProgressModel>
{
    private double Distance { get; }

    public DistanceProgressFeature(double distance)
    {
        Distance = distance;
    }

    public Task<DistanceProgressModel> BuildModelAsync(List<ActivityDocument> activities)
    {
        var model = new DistanceProgressModel { Distance = Distance, Values = [] };

        var timeLine = CalculateTimeLine(activities);
        if (timeLine.IsEmpty())
        {
            return Task.FromResult(model);
        }

        foreach (var record in timeLine)
        {
            model.Values.Add(new KeyValueModel<DateTime, double> { Key = record.StartTime, Value = record.Duration });
        }

        return Task.FromResult(model);
    }

    private List<ActivityDocument> CalculateTimeLine(List<ActivityDocument> activities)
    {
        var sortedActivities = activities?.Where(x => x.Distance >= Distance).OrderBy(x => x.StartTime).ToList() ?? [];

        var timeLine = new List<ActivityDocument>();
        var durationBest = 0D;

        foreach (var activity in sortedActivities)
        {
            if (timeLine.IsEmpty())
            {
                timeLine.Add(activity);
                durationBest = activity.Duration;
                continue;
            }

            if (activity.Duration < durationBest)
            {
                timeLine.Add(activity);
                durationBest = activity.Duration;
            }
        }

        return timeLine;
    }
}
