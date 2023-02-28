using Newtonsoft.Json;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        public async Task<double> GetPrice(string symbol)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = "https://financialmodelingprep.com/api/v3/quote-short/" + symbol+ "?apikey=c0ae03b2347766d3cdeb980d4abc282d";
                HttpResponseMessage response = await client.GetAsync(uri);
                string jsonResponse = await response.Content.ReadAsStringAsync();
   
                StockPriceResult stockPriceResult = JsonConvert.DeserializeObject<StockPriceResult>(jsonResponse.Substring(1, jsonResponse.Length - 2));

                if (stockPriceResult == null)
                {
                    throw new InvalidSymbolException(symbol);
                }
                return stockPriceResult.Price;

            }
        }
    }
}
