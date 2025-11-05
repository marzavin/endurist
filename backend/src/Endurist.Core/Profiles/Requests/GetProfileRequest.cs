using Endurist.Common.Models;
using Endurist.Core.Profiles.Models;
using MediatR;

namespace Endurist.Core.Profiles.Requests;

public class GetProfileRequest : IRequest<DataResponse<ProfileModel>>
{
    public GetProfileRequest(string id)
    {
        Id = id;
    }

    public string Id { get; }
}