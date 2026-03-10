using Endurist.Contracts;
using Endurist.Contracts.Exceptions;
using Endurist.Contracts.Profiles.Queries;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Filters;
using Endurist.Models.Profiles;
using SideEffect.Messaging.RPC;
using System.Linq.Expressions;

namespace Endurist.Reader.Handlers.Profiles;

internal sealed class GetProfileQueryHandler : RequestHandlerBase<GetProfileQuery, QueryReply<ProfileModel>>
{
    private readonly Storage _storage;

    public GetProfileQueryHandler(Storage storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public override async Task<QueryReply<ProfileModel>> HandleAsync(GetProfileQuery message, CancellationToken cancellationToken = default)
    {
        var document = await _storage.Profiles.GetByIdAsync(message.ProfileId, cancellationToken)
            ?? throw new EntityNotFoundException(typeof(ProfileDocument), message.ProfileId);

        var filter = new ActivityFilter { ProfileIdIn = [message.ProfileId] };
        var queryFilter = _storage.Activities.BuildFilter(filter);

        Expression<Func<ActivityDocument, ProfileModel>> mapper = x =>
            new ProfileModel
            {
                Id = x.ProfileId.ToString(),
                Distance = x.Distance,
                Duration = x.Duration
            };

        var profiles = await _storage.Activities.GetActivitySetTotalAsync(x => x.ProfileId, mapper, filter: queryFilter, cancellationToken: cancellationToken);

        var data = profiles.FirstOrDefault();
        if (data is not null)
        {
            data.Id = document.EntityId;
            data.Name = document.Name;
        }

        return new QueryReply<ProfileModel> { Data = data };
    }
}