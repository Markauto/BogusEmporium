namespace StockCatalogApi.Entities;

public record StockItem(Guid Id, string Name, decimal Cost, int Amount);