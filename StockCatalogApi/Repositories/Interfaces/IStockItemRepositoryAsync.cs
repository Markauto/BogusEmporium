using StockCatalogApi.Entities;

namespace StockCatalogApi.Repositories.Interfaces;

public interface IStockItemRepositoryAsync
{
    Task<IEnumerable<StockItem>> GetAllAsync();
    Task<StockItem> GetByIdAsync(Guid id);
    Task<StockItem> AddAsync(StockItem stockItem);
    Task<StockItem> UpdateAsync(StockItem stockItem);
    Task<bool> DeleteAsync(Guid id);
}
