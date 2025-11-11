using Endurist.Common.Models;
using Endurist.Core.Profiles.Models;
using MediatR;
using SideEffect.DataTransfer.Paging;
using SideEffect.DataTransfer.Sorting;

namespace Endurist.Core.Profiles.Requests;

public class GetProfilesRequest : IRequest<DataPageResponse<ProfilePreviewModel>>
{
    public GetProfilesRequest(PagingInfo paging = null, SortingInfo sorting = null)
    {     
        Paging = paging;
        Sorting = sorting;
    }
    
    public PagingInfo Paging { get; }

    public SortingInfo Sorting { get; }
}