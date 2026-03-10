using SideEffect.DataTransfer.Paging;
using SideEffect.DataTransfer.Sorting;

namespace Endurist.Contracts;

public class QueryPageReply<TData> : QueryReply<List<TData>>
{
    public PagingInfo Paging { get; set; }

    public SortingInfo Sorting { get; set; }
}
