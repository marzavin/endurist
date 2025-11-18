using Endurist.Data.Mongo.Documents;
using Endurist.Hosting.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using SideEffect.DataTransfer.Paging;
using SideEffect.DataTransfer.Sorting;
using SideEffect.Extensions;
using System.Linq.Expressions;

namespace Endurist.Data.Mongo.Repositories;

public abstract class RepositoryBase
{
    protected IMongoDatabase Database { get; }

    protected abstract string CollectionName { get; }

    protected RepositoryBase(MongoStorageConfiguration settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        var url = new MongoUrl(settings.ConnectionString);
        var client = new MongoClient(url);

        Database = client.GetDatabase(url.DatabaseName);
    }
}

public abstract class RepositoryBase<TDocument> : RepositoryBase
    where TDocument : DocumentBase
{
    protected IMongoCollection<TDocument> Collection => Database.GetCollection<TDocument>(CollectionName);

    protected RepositoryBase(MongoStorageConfiguration settings)
        : base(settings)
    { }

    protected virtual Dictionary<string, Expression<Func<TDocument, object>>> SortingFields { get; }
        = new Dictionary<string, Expression<Func<TDocument, object>>>(StringComparer.OrdinalIgnoreCase);

    protected virtual Expression<Func<TDocument, object>> DefaultSorting { get; } = x => x.Id;

    public virtual Task<List<TDocument>> SearchAsync(CancellationToken cancellationToken = default)
    {
        var queryFilter = Builders<TDocument>.Filter.Empty;
        return Collection.Find(queryFilter).ToListAsync(cancellationToken);
    }

    public virtual Task<List<TModel>> SearchAsync<TModel>(
        Expression<Func<TDocument, TModel>> mapper,
        FilterDefinition<TDocument> filter = null,
        PagingInfo paging = null,
        SortingInfo sorting = null,
        CancellationToken cancellationToken = default)
    {
        var queryFilter = filter ?? Builders<TDocument>.Filter.Empty;

        var builder = Builders<TDocument>.Projection;
        var projection = builder.Expression(mapper);

        var search = Collection.Find(queryFilter);

        if (sorting is not null)
        {
            search = ApplySorting(search, sorting);
        }

        if (paging is not null)
        {
            search = ApplyPaging(search, paging);
        }

        return search.Project(projection).ToListAsync(cancellationToken);
    }

    public virtual async Task<TDocument> GetFirstOrDefaultAsync(
        FilterDefinition<TDocument> filter = null,
        CancellationToken cancellationToken = default)
    {
        var queryFilter = filter ?? Builders<TDocument>.Filter.Empty;
        return await Collection.Find(queryFilter).FirstOrDefaultAsync(cancellationToken);
    }

    public virtual Task<TDocument> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var queryFilter = Builders<TDocument>.Filter.Eq(x => x.Id, ObjectId.Parse(id));
        return Collection.Find(queryFilter).FirstOrDefaultAsync(cancellationToken);
    }

    public virtual Task InsertAsync(TDocument document, CancellationToken cancellationToken = default)
    {
        return Collection.InsertOneAsync(document, cancellationToken: cancellationToken);
    }

    public virtual Task UpdateAsync(TDocument document, CancellationToken cancellationToken = default)
    {
        var queryFilter = Builders<TDocument>.Filter.Eq(x => x.Id, document.Id);
        return Collection.ReplaceOneAsync(queryFilter, document, new ReplaceOptions { IsUpsert = false }, cancellationToken);
    }

    public virtual Task DeleteAsync(TDocument document, CancellationToken cancellationToken = default)
    {
        var queryFilter = Builders<TDocument>.Filter.Eq(x => x.Id, document.Id);
        return DeleteAsync(queryFilter, cancellationToken);
    }

    public virtual Task DeleteAsync(FilterDefinition<TDocument> filter, CancellationToken cancellationToken = default)
    {
        return Collection.DeleteManyAsync(filter, cancellationToken);
    }

    protected IFindFluent<TDocument, TDocument> ApplySorting(IFindFluent<TDocument, TDocument> search, SortingInfo sorting)
    {
        var descending = sorting?.Descending ?? false;

        if (sorting is not null
            && !sorting.Key.IsEmpty()
            && !sorting.Key.IsSimilar(nameof(DocumentBase.Id))
            && SortingFields.TryGetValue(sorting.Key, out var sortExpression))
        {
            return descending
                ? search.SortByDescending(sortExpression).ThenByDescending(DefaultSorting)
                : search.SortBy(sortExpression).ThenBy(DefaultSorting);
        }

        return descending
            ? search.SortByDescending(DefaultSorting)
            : search.SortBy(DefaultSorting);
    }

    protected IFindFluent<TDocument, TDocument> ApplyPaging(IFindFluent<TDocument, TDocument> search, PagingInfo paging)
    {
        return search.Skip(paging.Skip).Limit(paging.Take);
    }
}
