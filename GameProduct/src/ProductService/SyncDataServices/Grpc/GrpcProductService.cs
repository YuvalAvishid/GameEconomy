using GameCommon.Repositories;
using Grpc.Core;
using ProductService.Entities;
using ProductService.Extensions;

namespace ProductService.SyncDataServices.Grpc;

public class GrpcProductService : GrpcProduct.GrpcProductBase
{
    private readonly IRepository<Item> _itemRepository;
    
    public GrpcProductService(IRepository<Item> itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public override async Task<ProductResponse> GetAllProducts(GetAllRequest request, ServerCallContext context)
    {
        var response = new ProductResponse();
        var products = await _itemRepository.GetAllAsync();

        foreach(var item in products)
        {
            response.Product.Add(item.AsGrpcModel());
        }

        return response;
    }
}
