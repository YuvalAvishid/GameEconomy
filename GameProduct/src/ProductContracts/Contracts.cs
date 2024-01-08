namespace ProductContracts;

public record ProductItemCreated(Guid ItemId, string Name, string Description);
public record ProductItemUpdated(Guid ItemId, string Name, string Description);
public record ProductItemDeleted(Guid ItemId);