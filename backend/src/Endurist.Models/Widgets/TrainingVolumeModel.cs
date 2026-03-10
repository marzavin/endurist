using SideEffect.DataTransfer.Models;

namespace Endurist.Models.Widgets;

public class TrainingVolumeModel
{
    public List<KeyValueModel<DateOnly, double>> Weekly { get; set; }

    public List<KeyValueModel<DateOnly, double>> Monthly { get; set; }
}
