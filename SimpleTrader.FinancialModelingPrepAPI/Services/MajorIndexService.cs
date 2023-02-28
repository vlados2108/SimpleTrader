using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            using(HttpClient client = new HttpClient())
            {
                var uri = "https://financialmodelingprep.com/api/v3/quote-short/" + GetUriSuffix(indexType) + "?apikey=c0ae03b2347766d3cdeb980d4abc282d";

                HttpResponseMessage response = await client.GetAsync(uri);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                MajorIndex majorIndex = JsonConvert.DeserializeObject<MajorIndex>(jsonResponse.Substring(1,jsonResponse.Length-2));
                majorIndex.Type = indexType;
                
                return majorIndex;
                   
            }
        }

        private string GetUriSuffix(MajorIndexType indexType)
        {
            return indexType switch
            {
                MajorIndexType.Apple => "AAPL",
                MajorIndexType.Google => "GOOG",
                MajorIndexType.Meta => "META",
                _ => "AAPL"
            };
        }
    }
}
