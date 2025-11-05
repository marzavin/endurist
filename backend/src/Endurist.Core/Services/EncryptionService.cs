using SideEffect.Extensions;
using System.Security.Cryptography;
using System.Text;

namespace Endurist.Core.Services;

public class EncryptionService : IEncryptionService
{
    private const int _keySize = 64;
    private const int _iterationsCount = 350000;
    private static HashAlgorithmName HashAlgorithm => HashAlgorithmName.SHA512;

    public async Task<string> ComputeHashAsync(Stream content, CancellationToken cancellationToken = default)
    {
        var hash = await SHA256.HashDataAsync(content, cancellationToken);
        return Convert.ToBase64String(hash);
    }

    public Task<string> ComputeHashAsync(byte[] content, CancellationToken cancellationToken = default)
    {
        var memoryStream = new MemoryStream(content);
        return ComputeHashAsync(memoryStream, cancellationToken);
    }

    public Task<string> ComputeHashAsync(string content, CancellationToken cancellationToken = default)
    {
        var bytes = Encoding.UTF8.GetBytes(content);
        return ComputeHashAsync(bytes, cancellationToken);
    }

    public Task<string> GenerateRandomStringAsync(int length, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Convert.ToBase64String(RandomNumberGenerator.GetBytes(length)));
    }

    public Task<string> ComputePasswordHashAsync(string password, string salt, CancellationToken cancellationToken = default)
    {
        if (password.IsEmpty())
        {
            throw new ArgumentNullException(nameof(password));
        }

        var saltBytes = Convert.FromBase64String(salt);
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        var hash = Rfc2898DeriveBytes.Pbkdf2(passwordBytes, saltBytes, _iterationsCount, HashAlgorithm, _keySize);

        return Task.FromResult(Convert.ToHexString(hash));
    }

    public Task<bool> VerifyPasswordAsync(string password, string salt, string passwordHash, CancellationToken cancellationToken = default)
    {
        var saltBytes = Convert.FromBase64String(salt);
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(passwordBytes, saltBytes, _iterationsCount, HashAlgorithm, _keySize);
        return Task.FromResult(CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(passwordHash)));
    }
}
