using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllSync();
        public Task<Stock?> GetByIdAsync(int id);
        public Task<Stock> CreateStockAsync(Stock stockModel);
        public Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        public Task<Stock?> DeleteAsync(int id);
        public Task<bool> StockExist(int id);
    }
}