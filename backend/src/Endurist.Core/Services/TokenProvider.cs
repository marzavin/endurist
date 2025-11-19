using Endurist.Common.Exceptions;
using Endurist.Core.Accounts.Models;
using Endurist.Hosting.Settings;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SideEffect.Extensions;
using System.Security.Claims;
using System.Text;

namespace Endurist.Core.Services;

public class TokenProvider
{
    private const int _refreshTokenLength = 64;

    private AuthenticationConfiguration AuthenticationConfiguration { get; }

    private IEncryptionService EncryptionService { get; }

    public TokenProvider(AuthenticationConfiguration configuration, IEncryptionService encryptionService)
    {
        AuthenticationConfiguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        EncryptionService = encryptionService ?? throw new ArgumentNullException(nameof(encryptionService));
    }

    public Task<string> CreateAccessTokenAsync(AccountModel account, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(account);

        if (AuthenticationConfiguration.Secret.IsEmpty())
        {
            throw new ApplicationConfigurationException($"Configuration key '{nameof(AuthenticationConfiguration.Secret)}' is not provided.");
        }

        if (AuthenticationConfiguration.Issuer.IsEmpty())
        {
            throw new ApplicationConfigurationException($"Configuration key '{nameof(AuthenticationConfiguration.Issuer)}' is not provided.");
        }

        if (AuthenticationConfiguration.Audience.IsEmpty())
        {
            throw new ApplicationConfigurationException($"Configuration key '{nameof(AuthenticationConfiguration.Audience)}' is not provided.");
        }

        if (AuthenticationConfiguration.ExpiresIn <= 0)
        {
            throw new ApplicationConfigurationException($"Configuration key '{nameof(AuthenticationConfiguration.ExpiresIn)}' is not valid.");
        }

        var securityKey = GetSecurityKey(AuthenticationConfiguration.Secret);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Issuer = AuthenticationConfiguration.Issuer,
            Audience = AuthenticationConfiguration.Audience,
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(AuthenticationConfiguration.ExpiresIn),
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, account.Id),
                new Claim(JwtRegisteredClaimNames.Name, account.Name)
            ])
        };

        var handler = new JsonWebTokenHandler();
        return Task.FromResult(handler.CreateToken(tokenDescriptor));
    }

    public Task<string> CreateRefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        return EncryptionService.GenerateRandomStringAsync(_refreshTokenLength, cancellationToken);
    }

    public static SecurityKey GetSecurityKey(string secret)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    }
}