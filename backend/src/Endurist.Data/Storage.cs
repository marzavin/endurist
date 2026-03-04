using Endurist.Data.Mongo.Repositories;

namespace Endurist.Data;

public class Storage
{
    public Storage(
        ActivityRepository activityRepository,
        FileRepository fileRepository,
        ProfileRepository profileRepository,
        ProfileWidgetRepository profileWidgetRepository,
        WidgetRepository widgetRepository)
    {
        Activities = activityRepository ?? throw new ArgumentNullException(nameof(activityRepository));
        Files = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
        Profiles = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
        ProfileWidgets = profileWidgetRepository ?? throw new ArgumentNullException(nameof(profileWidgetRepository));
        Widgets = widgetRepository ?? throw new ArgumentNullException(nameof(widgetRepository));
    }

    public ActivityRepository Activities { get; }

    public FileRepository Files { get; }

    public ProfileRepository Profiles { get; }

    public ProfileWidgetRepository ProfileWidgets { get; set; }

    public WidgetRepository Widgets { get; }
}
