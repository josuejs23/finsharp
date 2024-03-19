using api.Data;
using api.DTOs.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _context.Stocks.ToListAsync();
            var stocksDto =  stocks.Select( s => s.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequest createStockRequest)
        {
            var stockModel = createStockRequest.ToStockFromCreateDTO();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto )
        {
            var stockInDb = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if(stockInDb == null)
            {
                return NotFound();
            }

            stockInDb.Symbol = updateStockRequestDto.Symbol;
            stockInDb.CompanyName = updateStockRequestDto.CompanyName;
            stockInDb.Purchase = updateStockRequestDto.Purchase;
            stockInDb.LastDiv = updateStockRequestDto.LastDiv;
            stockInDb.Industry = updateStockRequestDto.Industry;
            stockInDb.MarketCap = updateStockRequestDto.MarketCap;

            await _context.SaveChangesAsync();
            return Ok(stockInDb.ToStockDto());

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockInDb = _context.Stocks.FirstOrDefault(s => s.Id == id);
            if(stockInDb == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stockInDb);
           await  _context.SaveChangesAsync();

            return NoContent();
        }
    }
}