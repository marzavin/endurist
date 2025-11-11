using Endurist.Common.Models;
using Endurist.Core.Files.Models;
using MediatR;
using SideEffect.DataTransfer.Paging;
using SideEffect.DataTransfer.Sorting;

namespace Endurist.Core.Files.Requests;

public class GetFilesRequest : IRequest<DataPageResponse<FilePreviewModel>>
{
    public GetFilesRequest(PagingInfo paging = null, SortingInfo sorting = null)
    {
        Paging = paging;
        Sorting = sorting;
    }

    public PagingInfo Paging { get; }

    public SortingInfo Sorting { get; }
}
