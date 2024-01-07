using GameCommon.Entities;

namespace ProductService.Entities;

public record Item : IEntity
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
}
