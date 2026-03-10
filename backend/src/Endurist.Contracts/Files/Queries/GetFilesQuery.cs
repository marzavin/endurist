using Endurist.Models.Files;
using SideEffect.DataTransfer.Paging;
using SideEffect.DataTransfer.Sorting;

namespace Endurist.Contracts.Files.Queries;

public class GetFilesQuery : QueryBase<QueryPageReply<FilePreviewModel>>
{
    public GetFilesQuery(PagingInfo paging = null, SortingInfo sorting = null)
    {
        Paging = paging;
        Sorting = sorting;
    }

    public PagingInfo Paging { get; }

    public SortingInfo Sorting { get; }
}
