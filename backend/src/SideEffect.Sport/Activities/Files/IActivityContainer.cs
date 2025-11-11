using SideEffect.Sport.Activities.Models;

namespace SideEffect.Sport.Activities.Files;

/// <summary>
/// Object interface for activity storage.
/// </summary>
public interface IActivityContainer
{
    /// <summary>
    /// Loads activities from storage.
    /// </summary>
    /// <returns><see cref="Activity"/> for more information.</returns>
    public Task<List<Activity>> LoadAsync();
}
