using GameCommon.Repositories;
using Microsoft.AspNetCore.Mvc;
using ProductService.Dtos;
using ProductService.Entities;
using ProductService.Extensions;

namespace ProductService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IRepository<Item> _itemRepository;

    public ItemsController(IRepository<Item> itemRepository)
    {
        _itemRepository = itemRepository;
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
    public IActionResult PostAsync(CreateItemDto createItemDto)
    {
        var item = new Item
        {
            Name = createItemDto.Name,
            Description = createItemDto.Description,
            Price = createItemDto.Price,
            CreatedDate = DateTimeOffset.UtcNow
        };

        _itemRepository.CreateAsync(item);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
    {
        var existingItem = await _itemRepository.GetItemAsync(id);
        if (existingItem == null)
            return NoContent();

        var updatedItemDto = existingItem with
        {
            Name = updateItemDto.Name,
            Description = updateItemDto.Description,
            Price = updateItemDto.Price
        };

        await _itemRepository.UpdateAsync(updatedItemDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var existingItem = await _itemRepository.GetItemAsync(id);
        if (existingItem == null)
            return NotFound();

        await _itemRepository.RemoveAsync(existingItem.Id);
        return NoContent();
    }
}
