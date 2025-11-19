using Endurist.Data.Mongo.Documents;
using Endurist.Hosting.Settings;
using MongoDB.Driver;

namespace Endurist.Data.Mongo.Repositories;

public class AccountRepository : RepositoryBase<AccountDocument>
{
    protected override string CollectionName => "accounts";

    public AccountRepository(MongoStorageConfiguration settings)
        : base(settings) { }

    public virtual Task<AccountDocument> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var queryFilter = Builders<AccountDocument>.Filter.Eq(x => x.Name, name);
        return Collection.Find(queryFilter).FirstOrDefaultAsync(cancellationToken);
    }
}