using GameCommon.Entities;

namespace InventoryService;

public record ProductItem : IEntity
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
