using SideEffect.DataTransfer.Models;

namespace Endurist.Core.Widgets.Models;

public class TrainingVolumeModel
{
    public List<KeyValueModel<DateOnly, double>> Weekly { get; set; }

    public List<KeyValueModel<DateOnly, double>> Monthly { get; set; }
}
