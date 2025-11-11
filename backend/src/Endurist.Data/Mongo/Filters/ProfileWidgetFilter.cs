namespace Endurist.Data.Mongo.Filters;

public class ProfileWidgetFilter
{
    public string ProfileIdEq { get; set; }

    public string WidgetIdEq { get; set; }
    public object ProfileId { get; internal set; }
}
