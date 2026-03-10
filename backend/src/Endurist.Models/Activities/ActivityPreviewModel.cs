namespace Endurist.Models.Activities;

public class ActivityPreviewModel : SegmentPreviewModel
{
    public string Id { get; set; }

    public ActivityCategory Category { get; set; }

    public int? Calories { get; set; }
}
