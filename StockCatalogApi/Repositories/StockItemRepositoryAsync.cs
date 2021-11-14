using ServiceStack.Redis;
using StockCatalogApi.Entities;
using StockCatalogApi.Repositories.Interfaces;

namespace StockCatalogApi.Repositories;

public class StockItemRepositoryAsync : IStockItemRepositoryAsync
{
    private readonly IRedisClientsManagerAsync _redisClientManager;
    private const string _redisKey = "stock-item";

    public StockItemRepositoryAsync(IRedisClientsManagerAsync redisClientManager)
    {
        this._redisClientManager = redisClientManager;
    }

    public async Task<StockItem> AddAsync(StockItem stockItem)
    {
        await using var redisClient = await this._redisClientManager.GetClientAsync();
        var storedStockItem = await redisClient.As<StockItem>().StoreAsync(stockItem);
        return storedStockItem;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await using var redisClient = await this._redisClientManager.GetClientAsync();
        var storedStockItem = await redisClient.As<StockItem>().GetByIdAsync(id);
        await redisClient.As<StockItem>().DeleteByIdAsync(id);
        return true;
    }

    public async Task<IEnumerable<StockItem>> GetAllAsync()
    {
        await using var redisClient = await _redisClientManager.GetClientAsync();
        var stockItems = await redisClient.As<StockItem>().GetAllAsync();
        return stockItems;
    }

    public async Task<StockItem> GetByIdAsync(Guid id)
    {
        await using var redisClient = await _redisClientManager.GetClientAsync();
        var stockItem = await redisClient.As<StockItem>().GetByIdAsync(id);
        return stockItem;
    }

    public async Task<StockItem> UpdateAsync(StockItem stockItem)
    {
        await using var redisClient = await _redisClientManager.GetClientAsync();
        var storedStockItem = await redisClient.As<StockItem>().StoreAsync(stockItem);
        return storedStockItem;
    }
}