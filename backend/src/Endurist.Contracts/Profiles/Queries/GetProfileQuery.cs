using Endurist.Models.Profiles;

namespace Endurist.Contracts.Profiles.Queries;

public class GetProfileQuery : QueryBase<QueryReply<ProfileModel>>
{
    public GetProfileQuery(string userId, string profileId)
        : base(userId)
    {
        ProfileId = profileId;
    }

    public string ProfileId { get; }
}