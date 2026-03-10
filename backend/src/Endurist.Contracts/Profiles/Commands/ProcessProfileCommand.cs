namespace Endurist.Contracts.Profiles.Commands;

public sealed class ProcessProfileCommand : CommandBase
{
    public ProcessProfileCommand(string userId, string profileId)
        : base(userId)
    {
        ProfileId = profileId;
    }

    public string ProfileId { get; set; }
}
