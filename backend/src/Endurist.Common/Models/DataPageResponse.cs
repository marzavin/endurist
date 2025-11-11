using SideEffect.DataTransfer.Paging;

namespace Endurist.Common.Models;

public class DataPageResponse<TItem>
{
    public List<TItem> Data { get; set; }
    
    public PagingInfo Paging { get; set; }
}