namespace ProductService;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetAllAsync();
    Task<Item> GetItemAsync(Guid Id);
    Task CreateAsync(Item entity);
    Task UpdateAsync(Item entity);
    Task RemoveAsync(Guid id);
}
