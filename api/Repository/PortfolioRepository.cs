using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {

        public readonly ApplicationDbContext _dbContext;
        public PortfolioRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _dbContext.Portfolios.AddAsync(portfolio);
            await _dbContext.SaveChangesAsync();
            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _dbContext.Portfolios.Where( u => u.AppUserId == user.Id)
                .Select(stock => new Stock
                {
                    Id = stock.StockId,
                    Symbol = stock.Stock.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    Purchase = stock.Stock.Purchase,
                    LastDiv = stock.Stock.LastDiv,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap
                }).ToListAsync();
        }
       
    }
}