using Endurist.Common.Models;
using Endurist.Core.Activities.Models;
using MediatR;
using SideEffect.DataTransfer.Paging;
using SideEffect.DataTransfer.Sorting;

namespace Endurist.Core.Activities.Requests;

public class GetActivitiesRequest : IRequest<DataPageResponse<ActivityPreviewModel>>
{
    public GetActivitiesRequest(PagingInfo paging = null, SortingInfo sorting = null)
    {     
        Paging = paging;
        Sorting = sorting;
    }
    
    public PagingInfo Paging { get; }

    public SortingInfo Sorting { get; }
}
