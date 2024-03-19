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
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList()
            .Select( s => s.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);

            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequest createStockRequest)
        {
            var stockModel = createStockRequest.ToStockFromCreateDTO();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto )
        {
            var stockInDb = _context.Stocks.FirstOrDefault(s => s.Id == id);
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

            _context.SaveChanges();
            return Ok(stockInDb.ToStockDto());

        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var stockInDb = _context.Stocks.FirstOrDefault(s => s.Id == id);
            if(stockInDb == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stockInDb);
            _context.SaveChanges();

            return NoContent();
        }
    }
}