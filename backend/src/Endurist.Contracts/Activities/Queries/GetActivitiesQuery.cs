using Endurist.Models.Activities;
using SideEffect.DataTransfer.Paging;
using SideEffect.DataTransfer.Sorting;

namespace Endurist.Contracts.Activities.Queries;

public class GetActivitiesQuery : QueryBase<QueryPageReply<ActivityPreviewModel>>
{
    public GetActivitiesQuery(PagingInfo paging = null, SortingInfo sorting = null)
    {     
        Paging = paging;
        Sorting = sorting;
    }
    
    public PagingInfo Paging { get; }

    public SortingInfo Sorting { get; }
}
