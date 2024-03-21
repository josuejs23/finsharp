using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        public StockController( IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _stockRepository.GetAllSync(query);
            var stocksDto =  stocks.Select( s => s.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepository.GetByIdAsync(id);

            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequest createStockRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = createStockRequest.ToStockFromCreateDTO();
            await _stockRepository.CreateStockAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto )
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockInDb = await _stockRepository.UpdateAsync(id,updateStockRequestDto);
            if(stockInDb == null)
            {
                return NotFound();
            }

            return Ok(stockInDb.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var stockInDb = await _stockRepository.DeleteAsync(id);
            if(stockInDb == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}