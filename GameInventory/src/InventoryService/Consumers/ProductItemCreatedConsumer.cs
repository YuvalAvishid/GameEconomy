using GameCommon.Repositories;
using MassTransit;
using ProductContracts;

namespace InventoryService;

public class ProductItemCreatedConsumer : IConsumer<ProductItemCreated>
{
    private readonly IRepository<ProductItem> _productRepository;

    public ProductItemCreatedConsumer(IRepository<ProductItem> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<ProductItemCreated> context)
    {
        var message = context.Message;

        var item = await _productRepository.GetItemAsync(message.ItemId);

        if(item != null)
        {
            return;
        }

        item = new ProductItem 
        {
            Id = message.ItemId,
            Name = message.Name,
            Description = message.Description
        };

        await _productRepository.CreateAsync(item);
    }
}
