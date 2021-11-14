namespace StockCatalogApi.Entities;

public record StockItem(Guid Id, string Name, string Description, decimal Cost, 
                        int Amount, string ImageUrl);