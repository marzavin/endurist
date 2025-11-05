using Endurist.Data.Mongo.Enums;

namespace Endurist.Data.Mongo.Filters;

public class ActivityFilter
{
    public string FileIdEq { get; set; }

    public List<string> ProfileIdIn { get; set; }

    public DateTime? StartTimeGte { get; set; }
    
    public List<ActivityCategory> CategoryIn { get; set; }

    public List<ActivityType> TypeIn { get; set; }

    public double? DistanceGte { get; set; }
}