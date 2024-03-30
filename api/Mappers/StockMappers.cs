using api.DTOs.Stock;
using api.models;

namespace api.Mappers
{
    public static class StockMappers
    {

        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select( c => c.ToCommentDto()).ToList()
            };
            
        }  

        public static Stock ToStockFromCreateDTO(this CreateStockRequest stockDtoModel)
        {
            return new Stock
            {
                Symbol = stockDtoModel.Symbol,
                CompanyName = stockDtoModel.CompanyName,
                Purchase = stockDtoModel.Purchase,
                LastDiv = stockDtoModel.LastDiv,
                Industry = stockDtoModel.Industry,
                MarketCap = stockDtoModel.MarketCap
            };
        } 
        public static Stock ToStockFromFMP(this FMPStock fmpStock)
        {
            return new Stock
            {
                Symbol = fmpStock.symbol,
                CompanyName = fmpStock.companyName,
                Purchase = (decimal)fmpStock.price,
                LastDiv = (decimal)fmpStock.lastDiv,
                Industry = fmpStock.industry,
                MarketCap = fmpStock.mktCap
            };
        } 


    }
}