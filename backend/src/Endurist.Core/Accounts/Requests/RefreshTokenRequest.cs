using Endurist.Common.Models;
using Endurist.Core.Accounts.Models;
using MediatR;

namespace Endurist.Core.Accounts.Requests;

public class RefreshTokenRequest : IRequest<DataResponse<TokenModel>>
{
    public RefreshTokenRequest(string token)
    {
        Token = token;
    }

    public string Token { get; }
}