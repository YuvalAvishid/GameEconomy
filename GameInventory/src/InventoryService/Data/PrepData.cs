using GameCommon.Repositories;
using InventoryService.SyncDataClient.Grpc;

namespace InventoryService.Data;

public static class PrepData
{
    public static async Task PrepPoulation(IApplicationBuilder applicationBuilder)
    {
        using( var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var grpcClient = serviceScope.ServiceProvider.GetService<IProductDataClient>();
            var products = await grpcClient.GetAllProducts();

            var itemsRepository = serviceScope.ServiceProvider.GetService<IRepository<ProductItem>>();
            await SeedData(itemsRepository, products);
        }
    }

    private static async Task SeedData(IRepository<ProductItem> itemsRepository, List<ProductItem> productItems)
    {
        Console.WriteLine($"Seeding new products productItems.Count = {productItems.Count}");

        foreach (var item in productItems)
        {
            var isExists = await itemsRepository.IsEntityExists(item.Id);
            if(!isExists)
            {
                await itemsRepository.CreateAsync(item);
            }
        }
    }
}
