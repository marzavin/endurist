using Endurist.Common.Models;
using Endurist.Common.Models.Activities;
using SideEffect.Messaging.RPC;

namespace Endurist.Contracts.Queries;

public class GetActivitiesResponse : ResponseBase
{
    public DataPageResponse<ActivityPreviewModel> Data { get; set; }
}
