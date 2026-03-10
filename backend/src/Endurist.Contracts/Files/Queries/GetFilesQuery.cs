using Endurist.Models.Files;
using SideEffect.DataTransfer.Paging;
using SideEffect.DataTransfer.Sorting;

namespace Endurist.Contracts.Files.Queries;

public class GetFilesQuery : QueryBase<QueryPageReply<FilePreviewModel>>
{
    public GetFilesQuery(string userId, PagingInfo paging = null, SortingInfo sorting = null)
        : base(userId)
    {
        Paging = paging;
        Sorting = sorting;
    }

    public PagingInfo Paging { get; }

    public SortingInfo Sorting { get; }
}
