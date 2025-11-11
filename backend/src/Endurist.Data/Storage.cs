using Endurist.Data.Mongo.Repositories;

namespace Endurist.Data;

public class Storage
{
    public Storage(
        AccountRepository accountRepository,
        ActivityRepository activityRepository,
        FileRepository fileRepository,
        ProfileRepository profileRepository,
        ProfileWidgetRepository profileWidgetRepository,
        TokenRepository tokenRepository,
        WidgetRepository widgetRepository)
    {
        Accounts = accountRepository ?? throw new AbandonedMutexException(nameof(AccountRepository));
        Activities = activityRepository ?? throw new ArgumentNullException(nameof(activityRepository));
        Files = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
        Profiles = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
        ProfileWidgets = profileWidgetRepository ?? throw new ArgumentNullException(nameof(profileWidgetRepository));
        Tokens = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
        Widgets = widgetRepository ?? throw new ArgumentNullException(nameof(widgetRepository));
    }

    public AccountRepository Accounts { get; }

    public ActivityRepository Activities { get; }

    public FileRepository Files { get; }

    public ProfileRepository Profiles { get; }

    public ProfileWidgetRepository ProfileWidgets { get; set; }

    public TokenRepository Tokens { get; }

    public WidgetRepository Widgets { get; }
}
