using SideEffect.DataTransfer.Models;

namespace Endurist.Core.Activities.Models;

public class ActivityModel : ActivityPreviewModel
{
    public List<SegmentPreviewModel> Segments { get; set; }

    public List<KeyValueModel<double, int?>> HeartRateGraph { get; set; }

    public List<KeyValueModel<double, int>> PaceGraph { get; set; }

    public List<KeyValueModel<double, double?>> AltitudeGraph { get; set; }

    public List<KeyValueModel<double, int?>> CadenceGraph { get; set; }
}
