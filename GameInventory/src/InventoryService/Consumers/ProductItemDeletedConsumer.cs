using GameCommon.Repositories;
using MassTransit;
using ProductContracts;

namespace InventoryService;

public class ProductItemDeletedConsumer : IConsumer<ProductItemDeleted>
{
    private readonly IRepository<ProductItem> _productRepository;

    public ProductItemDeletedConsumer(IRepository<ProductItem> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<ProductItemDeleted> context)
    {
        var message = context.Message;

        var item = await _productRepository.GetItemAsync(message.ItemId);

        if(item == null)
        {
            return;
        }

        await _productRepository.RemoveAsync(message.ItemId);
    }
}
