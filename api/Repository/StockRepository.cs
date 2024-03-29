using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateStockAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockInDb = await _context.Stocks.FirstOrDefaultAsync( s => s.Id == id);
            if(stockInDb == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockInDb);
            await _context.SaveChangesAsync();
            return stockInDb;
        }

        public async  Task<List<Stock>> GetAllSync(QueryObject? queryObject)
        {
            var stocks = _context.Stocks.Include(s => s.Comments).ThenInclude( c => c.AppUser).AsQueryable();
            
            if(queryObject != null && !string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(queryObject.Symbol));
            }

            if(queryObject != null && !string.IsNullOrWhiteSpace(queryObject.Company))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(queryObject.Company));
            }

            if(queryObject != null && !string.IsNullOrEmpty(queryObject.SortBy))
            {
                if(queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObject.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

            return await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync( i => i.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync( s => s.Symbol == symbol);
        }

        public async Task<bool> StockExist(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var stockInDb = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if(stockInDb == null)
            {
                return null;
            }

            stockInDb.Symbol = stockDto.Symbol;
            stockInDb.CompanyName = stockDto.CompanyName;
            stockInDb.Purchase = stockDto.Purchase;
            stockInDb.LastDiv = stockDto.LastDiv;
            stockInDb.Industry = stockDto.Industry;
            stockInDb.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();

            return stockInDb;
        }
    }
}