namespace Endurist.Contracts.Profiles.Commands;

public sealed class ProcessProfileCommand : CommandBase
{
    public ProcessProfileCommand(string profileId)
    {
        ProfileId = profileId;
    }

    public string ProfileId { get; set; }
}
