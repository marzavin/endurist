using SideEffect.DataTransfer.Models;

namespace Endurist.Core.Profiles.Models;

public class DistanceProgressModel
{
    public double Distance { get; set; }

    public List<KeyValueModel<DateTime, double>> Values { get; set; }
}
