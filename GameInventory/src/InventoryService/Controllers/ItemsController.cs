using GameCommon.Repositories;
using InventoryService.Dtos;
using InventoryService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService;

[ApiController]
[Route("api/i/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IRepository<InventoryItem> _inventoryRepository;
    private readonly IRepository<ProductItem> _itemRepository;

    public ItemsController(IRepository<InventoryItem> inventoryRepository, IRepository<ProductItem> itemRepository)
    {
        _inventoryRepository = inventoryRepository;
        _itemRepository = itemRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAllAsync(Guid userId)
    {
        if(userId == Guid.Empty)
            return BadRequest();

        var inventoryItems = await _inventoryRepository.GetAllAsync(item => item.UserId == userId);
        var itemsId = inventoryItems.Select(item => item.ProductItemId);
        var productItems = await _itemRepository.GetAllAsync(item => itemsId.Contains(item.Id));
                
        var inventoryItemDtos = inventoryItems.Select(inventoryItem => 
        {
            var productItem = productItems.SingleOrDefault(productItem => productItem.Id == inventoryItem.ProductItemId);
            if(productItem == null)
            {
                Console.WriteLine($"{inventoryItem.ProductItemId} doesn't exists");
                return null;
            }

            return inventoryItem.AsDto(productItem.Name, productItem.Description);
        })
        .Where(item => item != null)
        .ToList();

        return Ok(inventoryItemDtos);
    }

    
    [HttpPost]
    public async Task<IActionResult> PostAsync(GrantItemsDto grantItemsDto)
    {
        var productItem = await _itemRepository.GetItemAsync(grantItemsDto.ProductItemId);
        if(productItem == null)
        {
            var msg = new {error = $"There is no such item with the following Id {grantItemsDto.ProductItemId}"};
            return BadRequest(msg);
        }

        var inventoryItem = (await _inventoryRepository.GetAllAsync(item => item.UserId == grantItemsDto.UserId && item.ProductItemId == grantItemsDto.ProductItemId))
                                  .SingleOrDefault();
        if(inventoryItem == null)
        {
            inventoryItem = new InventoryItem
            {
                UserId = grantItemsDto.UserId,
                ProductItemId = grantItemsDto.ProductItemId,
                Quantity = grantItemsDto.Quantity,
                AcquiredTime = DateTimeOffset.UtcNow
            };
            await _inventoryRepository.CreateAsync(inventoryItem);
        }
        else
        {
            inventoryItem = inventoryItem with {
                Quantity = inventoryItem.Quantity + grantItemsDto.Quantity,
            };
            await _inventoryRepository.UpdateAsync(inventoryItem);
        }

        return Ok();
    }
    
}
