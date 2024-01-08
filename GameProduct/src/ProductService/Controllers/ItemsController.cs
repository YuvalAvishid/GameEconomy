using GameCommon.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ProductContracts;
using ProductService.Dtos;
using ProductService.Entities;
using ProductService.Extensions;

namespace ProductService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IRepository<Item> _itemRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public ItemsController(IRepository<Item> itemRepository, IPublishEndpoint publishEndpoint)
    {
        _itemRepository = itemRepository;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetItemsAsync()
    {
        var items = await _itemRepository.GetAllAsync();
        return items.Select(item => item.AsDto()).ToList();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var item = await _itemRepository.GetItemAsync(id);
        if (item == null)
            return NoContent();

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(CreateItemDto createItemDto)
    {
        var item = new Item
        {
            Name = createItemDto.Name,
            Description = createItemDto.Description,
            Price = createItemDto.Price,
            CreatedDate = DateTimeOffset.UtcNow
        };

        await _itemRepository.CreateAsync(item);

        await _publishEndpoint.Publish(new ProductItemCreated(item.Id, item.Name, item.Description));

        return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
    {
        var existingItem = await _itemRepository.GetItemAsync(id);
        if (existingItem == null)
            return NoContent();

        existingItem = existingItem with
        {
            Name = updateItemDto.Name,
            Description = updateItemDto.Description,
            Price = updateItemDto.Price
        };

        await _itemRepository.UpdateAsync(existingItem);

        await _publishEndpoint.Publish(new ProductItemUpdated(existingItem.Id, existingItem.Name, existingItem.Description));

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var existingItem = await _itemRepository.GetItemAsync(id);
        if (existingItem == null)
            return NotFound();

        await _itemRepository.RemoveAsync(existingItem.Id);

        await _publishEndpoint.Publish(new ProductItemDeleted(existingItem.Id));

        return NoContent();
    }
}
