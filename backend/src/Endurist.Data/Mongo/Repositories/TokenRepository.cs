using Endurist.Data.Mongo.Documents;
using Endurist.Hosting.Settings;
using MongoDB.Driver;

namespace Endurist.Data.Mongo.Repositories;

public class TokenRepository : RepositoryBase<TokenDocument>
{
    protected override string CollectionName => "tokens";

    public TokenRepository(MongoStorageConfiguration settings)
        : base(settings) { }

    public virtual Task<TokenDocument> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        var queryFilter = Builders<TokenDocument>.Filter.Eq(x => x.Token, token);
        return Collection.Find(queryFilter).FirstOrDefaultAsync(cancellationToken);
    }
}