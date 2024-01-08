using GameCommon.Repositories;
using InventoryService.Dtos;
using InventoryService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IRepository<InventoryItem> _itemsRepository;

    public ItemsController(IRepository<InventoryItem> mongoRepository)
    {
        _itemsRepository = mongoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAllAsync(Guid userId)
    {
        if(userId == Guid.Empty)
            return BadRequest();

        var items = (await _itemsRepository.GetAllAsync(item => item.UserId == userId))
                            .Select(item => item.AsDto());

        return Ok(items);
    }

    
    [HttpPost]
    public async Task<IActionResult> PostAsync(GrantItemsDto grantItemsDto)
    {
        var inventoryItem = (await _itemsRepository.GetAllAsync(item => item.UserId == grantItemsDto.UserId && item.ProductItemId == grantItemsDto.ProductItemId))
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
            await _itemsRepository.CreateAsync(inventoryItem);
        }
        else
        {
            inventoryItem = inventoryItem with {
                Quantity = inventoryItem.Quantity + grantItemsDto.Quantity,
            };
            await _itemsRepository.UpdateAsync(inventoryItem);
        }

        return Ok();
    }
    
}
