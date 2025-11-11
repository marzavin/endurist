namespace Endurist.Core.Services;

public interface IEncryptionService
{
    Task<string> ComputeHashAsync(Stream content, CancellationToken cancellationToken = default);

    Task<string> ComputeHashAsync(string content, CancellationToken cancellationToken = default);

    Task<string> ComputeHashAsync(byte[] content, CancellationToken cancellationToken = default);

    Task<string> GenerateRandomStringAsync(int length, CancellationToken cancellationToken = default);

    Task<string> ComputePasswordHashAsync(string password, string salt, CancellationToken cancellationToken = default);

    Task<bool> VerifyPasswordAsync(string password, string salt, string passwordHash, CancellationToken cancellationToken = default);
}
