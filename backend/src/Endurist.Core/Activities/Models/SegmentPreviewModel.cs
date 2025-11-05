namespace Endurist.Core.Activities.Models;

public class SegmentPreviewModel
{
    public DateTime StartTime { get; set; }
       
    public double Distance { get; set; }

    public double Duration { get; set; }

    public double Pace { get; set; }

    public int? AverageHeartRate { get; set; }
    
    public int? AverageCadence { get; set; }
}