using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

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

        public async  Task<List<Stock>> GetAllSync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
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