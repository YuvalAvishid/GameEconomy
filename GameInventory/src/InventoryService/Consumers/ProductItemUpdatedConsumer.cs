using GameCommon.Repositories;
using MassTransit;
using ProductContracts;

namespace InventoryService;

public class ProductItemUpdatedConsumer : IConsumer<ProductItemUpdated>
{
    private readonly IRepository<ProductItem> _productRepository;

    public ProductItemUpdatedConsumer(IRepository<ProductItem> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<ProductItemUpdated> context)
    {
        var message = context.Message;

        var item = await _productRepository.GetItemAsync(message.ItemId);

        if(item == null)
        {
            item = new ProductItem 
            {
                Id = message.ItemId,
                Name = message.Name,
                Description = message.Description
            };

            await _productRepository.CreateAsync(item);
        }
        else
        {
            item = item with
            {
                Name = message.Name,
                Description = message.Description
            };

            await _productRepository.UpdateAsync(item);
        }        
    }
}
