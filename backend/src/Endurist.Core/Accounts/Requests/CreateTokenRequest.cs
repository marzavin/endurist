using Endurist.Common.Models;
using Endurist.Core.Accounts.Models;
using MediatR;

namespace Endurist.Core.Accounts.Requests;

public class CreateTokenRequest : IRequest<DataResponse<TokenModel>>
{
    public CreateTokenRequest(string email, string password)
    {
        Email = email;
        Password = password;;
    }

    public string Email { get; }

    public string Password { get; }
}