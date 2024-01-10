namespace InventoryService.SyncDataClient.Grpc;

public interface IProductDataClient
{
    Task<List<ProductItem>> GetAllProducts();
}
