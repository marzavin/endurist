using SideEffect.DataTransfer.Models;

namespace Endurist.Common.Models.Activities;

public class SegmentModel : SegmentPreviewModel
{
    public List<KeyValueModel<double, int?>> HeartRateGraph { get; set; }

    public List<KeyValueModel<double, int>> PaceGraph { get; set; }

    public List<KeyValueModel<double, double?>> AltitudeGraph { get; set; }

    public List<KeyValueModel<double, int?>> CadenceGraph { get; set; }
}
