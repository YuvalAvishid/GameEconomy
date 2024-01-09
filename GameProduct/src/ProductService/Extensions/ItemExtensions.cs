using ProductService.Dtos;
using ProductService.Entities;

namespace ProductService.Extensions;

public static class ItemExtensions
{
    public static ItemDto AsDto(this Item item)
    {
        return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
    }

    public static GrpcProductModel AsGrpcModel(this Item item)
    {
        return new GrpcProductModel
        {
            ProductId = item.Id.ToString(),
            Name = item.Name,
            Description = item.Description
        };
    }

}
