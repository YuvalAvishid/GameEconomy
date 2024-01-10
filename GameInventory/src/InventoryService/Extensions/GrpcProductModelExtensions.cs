using ProductService;

namespace InventoryService;

public static class GrpcProductModelExtensions
{
    public static ProductItem AsProductItem(this GrpcProductModel grpcProductModel)
    {
        return new ProductItem
        {
            Id = Guid.Parse(grpcProductModel.ProductId),
            Name = grpcProductModel.Name,
            Description = grpcProductModel.Description
        };
    }
}
