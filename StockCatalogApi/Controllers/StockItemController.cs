using StockCatalogApi.Repositories.Interfaces;
using StockCatalogApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace StockCatalogApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StockItemController : ControllerBase
{
    private readonly IStockItemRepositoryAsync _stockItemRepository;
    public StockItemController(IStockItemRepositoryAsync stockItemRepository)
    {
        _stockItemRepository = stockItemRepository;
    }

    [HttpGet(Name = "GetALLStockItems")]
    public async Task<IEnumerable<StockItem>> GetAll()
    {
        return await _stockItemRepository.GetAllAsync();
    }

    [HttpGet("{id}", Name = "GetStockItem")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var stockItem = await _stockItemRepository.GetByIdAsync(id);
        if (stockItem == null)
        {
            return NotFound();
        }
        return Ok(stockItem);
    }

    [HttpPost(Name = "AddStockItem")]
    public async Task<IActionResult> Add([FromBody] StockItem stockItem)
    {
        if (stockItem == null)
        {
            return BadRequest();
        }

        if (await _stockItemRepository.GetByIdAsync(stockItem.Id) != null)
        {
            return BadRequest();
        }
        
        return Ok(await _stockItemRepository.AddAsync(stockItem));
    }

    [HttpPut("{id}", Name = "UpdateStockItem")]
    public async Task<IActionResult> Update(Guid id, [FromBody] StockItem stockItem)
    {
        if (stockItem == null)
        {
            return BadRequest();
        }

        var stockItemToUpdate = await _stockItemRepository.GetByIdAsync(id);
        if (stockItemToUpdate == null)
        {
            return NotFound();
        }

        return Ok(await _stockItemRepository.UpdateAsync(stockItem));
    }

    [HttpDelete("{id}", Name = "DeleteStockItem")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var stockItemToDelete = await _stockItemRepository.GetByIdAsync(id);
        if (stockItemToDelete == null)
        {
            return NotFound();
        }

        return Ok(await _stockItemRepository.DeleteAsync(id));
    }
}