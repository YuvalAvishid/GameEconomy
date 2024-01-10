
using Grpc.Net.Client;
using ProductService;

namespace InventoryService.SyncDataClient.Grpc;

public class ProductDataClient : IProductDataClient
{
    private readonly IConfiguration _configuration;

    public ProductDataClient(IConfiguration configuration)
    {
        _configuration  = configuration;
    }

    public async Task<List<ProductItem>> GetAllProducts()
    {
        List<ProductItem> productItems = new List<ProductItem>();

        var grpcService = _configuration["GrpcProduct"] ?? string.Empty;
        System.Diagnostics.Debug.WriteLine($"Calling Grpc Service {grpcService}");

        var channel = GrpcChannel.ForAddress(grpcService);
        var client =  new GrpcProduct.GrpcProductClient(channel);
        var request = new GetAllRequest();

        try
        {
            var reply = await client.GetAllProductsAsync(request);
            productItems = reply.Product.Select(item => item.AsProductItem())
                                .ToList();
        }
        catch( Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Calling Grpc Service {grpcService} FAILED! {ex.Message}");
        }

        return productItems;
    }
}
