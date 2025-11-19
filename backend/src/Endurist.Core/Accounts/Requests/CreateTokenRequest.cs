using Endurist.Common.Models;
using Endurist.Core.Accounts.Models;
using MediatR;

namespace Endurist.Core.Accounts.Requests;

public class CreateTokenRequest : IRequest<DataResponse<TokenModel>>
{
    public CreateTokenRequest(string name, string password)
    {
        Name = name;
        Password = password;;
    }

    public string Name { get; }

    public string Password { get; }
}