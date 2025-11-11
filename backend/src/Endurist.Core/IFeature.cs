using Endurist.Data.Mongo.Documents;

namespace Endurist.Core;

public interface IWidgetFeature<TModel>
{
    Task<TModel> BuildModelAsync(List<ActivityDocument> activities);
}
