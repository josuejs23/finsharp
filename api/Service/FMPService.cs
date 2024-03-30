using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.Interfaces;
using api.Mappers;
using api.models;
using Newtonsoft.Json;

namespace api.Service
{
    public class FMPService : IFMPService
    {
        private HttpClient _httpClient;
        private IConfiguration _config;
    //   private string api =  $"https://financialmodelingprep.com/api/v3/cash-flow-statement/${query}?limit=40&apikey=c6e4c428cf5d1fd44910979ee40344ce";
        

        public FMPService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try
            {
                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/cash-flow-statement/${symbol}?limit=40&apikey={_config["FMPKey"]}");
                if(result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                    var stock = tasks[0];
                    if(stock != null)
                    {
                        stock.ToStockFromFMP();
                    }
                    return null;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            return null;
        }
    }
}