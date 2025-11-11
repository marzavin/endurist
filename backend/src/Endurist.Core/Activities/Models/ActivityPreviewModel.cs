using Endurist.Data.Mongo.Enums;

namespace Endurist.Core.Activities.Models;

public class ActivityPreviewModel : SegmentPreviewModel
{
    public string Id { get; set; }

    public ActivityCategory Category { get; set; }

    public int? Calories { get; set; }
}
