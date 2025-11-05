namespace Endurist.Hosting.Settings;

/// <summary>
/// Configuration of Mongo DB data storage.
/// </summary>
public class MongoStorageConfiguration
{
    /// <summary>
    /// Gets or sets connection string to Mongo DB storage.
    /// </summary>
    public string ConnectionString { get; set; }
}
