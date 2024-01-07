
using MongoDB.Driver;

namespace ProductService;

public class ItemRepository : IItemRepository
{
    private const string collectionName = "items";
    private readonly IMongoCollection<Item> _dbCollection;
    private readonly FilterDefinitionBuilder<Item> _filterBuilder = Builders<Item>.Filter;

    public ItemRepository(IMongoDatabase database)
    {
        _dbCollection = database.GetCollection<Item>(collectionName);
    }
    
    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        return await _dbCollection.Find(_filterBuilder.Empty).ToListAsync();
    }
    
    public async Task<Item> GetItemAsync(Guid id)
    {
        FilterDefinition<Item> filter = _filterBuilder.Eq(entity => entity.Id, id);
        return await _dbCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task CreateAsync(Item entity)
    {
        if(entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        await _dbCollection.InsertOneAsync(entity);
    }
    
    public async Task UpdateAsync(Item entity)
    {
        if(entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        FilterDefinition<Item> filter = _filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
        await _dbCollection.ReplaceOneAsync(filter, entity);
    }

    public async Task RemoveAsync(Guid id)
    {
        FilterDefinition<Item> filter = _filterBuilder.Eq(existingEntity => existingEntity.Id, id);
        await _dbCollection.DeleteOneAsync(filter);
    }    
}
