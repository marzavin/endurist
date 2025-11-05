using Endurist.Core.Widgets.Models;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Filters;
using SideEffect.DataTransfer.Models;
using SideEffect.Extensions;
using System.Linq.Expressions;
using System.Text.Json;

namespace Endurist.Core.Widgets.Features;

public class TrainingVolumeWidget : WidgetBase
{
    private const int NumberOfMonths = 12;
    private const int NumberOfWeeks = 52;

    public override string Id => Constants.WidgetIdentifiers.TrainingVolume;

    protected override async Task<object> GetWidgetDataAsync(Storage storage, WidgetSettingsModel settings, CancellationToken cancellationToken = default)
    {
        var filter = new ProfileWidgetFilter { ProfileIdEq = settings.ProfilleId, WidgetIdEq = Id };
        var queryFilter = storage.ProfileWidgets.BuildFilter(filter);
        var trainingVolumeWidget = await storage.ProfileWidgets.GetFirstOrDefaultAsync(queryFilter, cancellationToken);

        var calculatedData = trainingVolumeWidget?.Data is null
            ? new TrainingVolumeModel { Monthly = [], Weekly = [] }
            : JsonSerializer.Deserialize<TrainingVolumeModel>(trainingVolumeWidget.Data, SerializerOptions);

        var utcNow = DateTime.UtcNow;

        var result = new TrainingVolumeModel { Monthly = [], Weekly = [] };

        var firstDayOfMonth = utcNow.FirstDayOfMonth();

        for (var i = 0; i < NumberOfMonths; i++)
        {
            result.Monthly.Add(new KeyValueModel<DateOnly, double>
            {
                Key = firstDayOfMonth,
                Value = calculatedData.Monthly.FirstOrDefault(x => x.Key == firstDayOfMonth)?.Value ?? 0
            });

            firstDayOfMonth = firstDayOfMonth.AddMonths(-1);
        }

        result.Monthly = result.Monthly.OrderBy(x => x.Key).ToList();

        var firstDayOfWeek = utcNow.FirstDayOfWeek(DayOfWeek.Monday);

        for (var i = 0; i < NumberOfWeeks; i++)
        {
            result.Weekly.Add(new KeyValueModel<DateOnly, double>
            {
                Key = firstDayOfWeek,
                Value = calculatedData.Weekly.FirstOrDefault(x => x.Key == firstDayOfWeek)?.Value ?? 0
            });

            firstDayOfWeek = firstDayOfWeek.AddDays(-7);
        }

        result.Weekly = result.Weekly.OrderBy(x => x.Key).ToList();

        return result;
    }

    public async Task<string> CalculateWidgetDataAsync(Storage storage, string profileId, CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;

        var activityFilter = new ActivityFilter
        {
            ProfileIdIn = [profileId],
            StartTimeGte = utcNow.AddYears(-1)
        };
        var activityQueryFilter = storage.Activities.BuildFilter(activityFilter);

        Expression<Func<ActivityDocument, KeyValueModel<DateTime, double>>> distanceMapper = x =>
            new KeyValueModel<DateTime, double>
            {
                Key = x.StartTime,
                Value = x.Distance
            };

        var activities = await storage.Activities.SearchAsync(distanceMapper, activityQueryFilter, cancellationToken: cancellationToken);

        var trainingVolume = new TrainingVolumeModel { Monthly = [], Weekly = [] };

        var firstDayOfMonth = new DateTime(utcNow.FirstDayOfMonth(), new TimeOnly(0, 0), DateTimeKind.Utc);
        var firstDayOfNextMonth = firstDayOfMonth.AddMonths(1); //TODO: Check if this is UTC Time

        for (var i = 0; i < NumberOfMonths; i++)
        {
            var monthlyVolume = activities
                 .Where(x => x.Key >= firstDayOfMonth && x.Key < firstDayOfNextMonth)
                 .Sum(x => x.Value);
            trainingVolume.Monthly.Add(new KeyValueModel<DateOnly, double> { Key = DateOnly.FromDateTime(firstDayOfMonth), Value = monthlyVolume });

            firstDayOfNextMonth = firstDayOfMonth;
            firstDayOfMonth = firstDayOfMonth.AddMonths(-1);
        }

        trainingVolume.Monthly = trainingVolume.Monthly.OrderBy(x => x.Key).ToList();

        var firstDayOfWeek = new DateTime(utcNow.FirstDayOfWeek(DayOfWeek.Monday), new TimeOnly(0, 0), DateTimeKind.Utc);
        var firstDayOfNextWeek = firstDayOfWeek.AddDays(7); //TODO: Check if this is UTC Time

        for (var i = 0; i < NumberOfWeeks; i++)
        {
            var weeklyVolume = activities
                 .Where(x => x.Key >= firstDayOfWeek && x.Key < firstDayOfNextWeek)
                 .Sum(x => x.Value);
            trainingVolume.Monthly.Add(new KeyValueModel<DateOnly, double> { Key = DateOnly.FromDateTime(firstDayOfWeek), Value = weeklyVolume });

            firstDayOfNextWeek = firstDayOfWeek;
            firstDayOfWeek = firstDayOfWeek.AddDays(-7);
        }

        trainingVolume.Weekly = trainingVolume.Weekly.OrderBy(x => x.Key).ToList();

        return SerializeData(trainingVolume);
    }
}
