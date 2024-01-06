using Microsoft.AspNetCore.Mvc;

namespace ProductService;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private static readonly List<ItemDto> Items = new List<ItemDto>()
    {
        new ItemDto(Guid.NewGuid(), "Poition", "Some Poition", 3, DateTime.Now),
        new ItemDto(Guid.NewGuid(), "Sword", "Some Sword", 3, DateTime.UtcNow)
    };
    
    [HttpGet]
    public IEnumerable<ItemDto> GetItems()
    {
        return Items;
    }

    [HttpGet("/id")]
    public IActionResult GetItems(Guid id)
    {
        var item = Items.SingleOrDefault(item => item.Id == id);
        if (item == null)
            return NoContent();

        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateItem(CreateItemDto createItemDto)
    {
        var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTime.Now);
        Items.Add(item);

        return Ok(createItemDto);
    }

    [HttpPut("/id")]
    public IActionResult UpdateItem(Guid id, UpdateItemDto updateItemDto)
    {
        var itemIndex = Items.FindIndex(item => item.Id == id);
        if (itemIndex == -1)
            return NoContent();

        var item = Items[itemIndex];    
        var updatedItemDto = item with
        {
            Name = updateItemDto.Name,
            Description = updateItemDto.Description,
            Price = updateItemDto.Price
        };

        Items[itemIndex] = updatedItemDto;

        return Ok(updatedItemDto);
    }

    [HttpDelete("/id")]
    public IActionResult DeleteItem(Guid id)
    {
        var item = Items.SingleOrDefault(item => item.Id == id);
        if (item == null)
            return NotFound();

        Items.Remove(item);
        return NoContent();
    }
}
