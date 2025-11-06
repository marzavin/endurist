using Endurist.Core;
using Endurist.Core.Widgets.Features;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Filters;
using MongoDB.Bson;

namespace Endurist.Worker.Tasks.Profiles.TrainingVolume;

internal class TrainingVolumeCalculationTask : ProfileBackgroundTaskBase<TrainingVolumeCalculationInput>
{
    private readonly Storage _storage;

    public TrainingVolumeCalculationTask(Storage storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public override async Task ExecuteAsync(TrainingVolumeCalculationInput input, CancellationToken cancellationToken = default)
    {
        var widget = new TrainingVolumeWidget();
        var data = await widget.CalculateWidgetDataAsync(_storage, input.ProfileId, cancellationToken);

        var profileWidgetFilter = new ProfileWidgetFilter
        {
            ProfileIdEq = input.ProfileId,
            WidgetIdEq = Constants.WidgetIdentifiers.TrainingVolume
        };

        var profileWidgetQueryFilter = _storage.ProfileWidgets.BuildFilter(profileWidgetFilter);
        var profileWidget = await _storage.ProfileWidgets.GetFirstOrDefaultAsync(profileWidgetQueryFilter, cancellationToken);
        if (profileWidget is null)
        {
            profileWidget = new ProfileWidgetDocument
            {
                ProfileId = ObjectId.Parse(input.ProfileId),
                WidgetId = ObjectId.Parse(Constants.WidgetIdentifiers.TrainingVolume),
                Data = data
            };

            await _storage.ProfileWidgets.InsertAsync(profileWidget, cancellationToken);
        }
        else
        {
            profileWidget.Data = data;

            await _storage.ProfileWidgets.UpdateAsync(profileWidget, cancellationToken);
        }
    }
}
