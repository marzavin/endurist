namespace Endurist.Hosting.Settings;

/// <summary>
/// Configuration of Redis data storage.
/// </summary>
public class RedisStorageConfiguration
{
    /// <summary>
    /// Gets or sets connection string to Redis storage.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets password for Redis storage.
    /// </summary>
    public string Password { get; set; }
}
