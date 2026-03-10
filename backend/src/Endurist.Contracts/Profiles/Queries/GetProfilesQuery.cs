using Endurist.Models.Profiles;
using SideEffect.DataTransfer.Paging;
using SideEffect.DataTransfer.Sorting;

namespace Endurist.Contracts.Profiles.Queries;

public class GetProfilesQuery : QueryBase<QueryPageReply<ProfilePreviewModel>>
{
    public GetProfilesQuery(string userId, PagingInfo paging = null, SortingInfo sorting = null)
        : base(userId)
    {     
        Paging = paging;
        Sorting = sorting;
    }
    
    public PagingInfo Paging { get; }

    public SortingInfo Sorting { get; }
}