using Crypton.Data;
using Crypton.Extensions;
using Crypton.Models.CurrencyViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Crypton.DataHelpers.CoinMarketCapHelpers
{
    public class GlobalDataModel
    {
        public partial class JsonResult
        {
            /* The parent object for the JSON response is "data" */
            [JsonProperty("data")]
            public Data data { get; set; }
        }

        public partial class Data
        {
            [JsonProperty("active_cryptocurrencies")]
            public int ActiveCryptocurrencies { get; set; }

            [JsonProperty("active_markets")]
            public int ActiveMarkets { get; set; }

            [JsonProperty("bitcoin_percentage_of_market_cap")]
            public double BitcoinPercentageOfMarketCap { get; set; }

            [JsonProperty("quotes")]
            public Dictionary<string, Quote> Quotes { get; set; }

            [JsonProperty("last_updated")]
            public long LastUpdated { get; set; }

        }

        public partial class Quote
        {
            [JsonProperty("total_market_cap")]
            public long TotalMarketCap { get; set; }

            [JsonProperty("total_volume_24h")]
            public long TotalVolume24H { get; set; }
        }

        /* Initializing a new Database context specific to this class */
        private readonly ApplicationDbContext _context;
        /* Initializing a provider to incorporate more data providers like CoinGecko */
        public Provider provider { get; set; }

        /* Constructor for Ticker with a db context */
        public GlobalDataModel(ApplicationDbContext context)
        {
            this._context = context;
        }


        public async Task<bool> UpdateGlobalDataAsync()
        {
            try
            {
                string endPoint = "global/"; /* This is the end point for the api to fetch global data */
                using (HttpClient httpClient = new HttpClient())
                {
                    using (HttpResponseMessage response = await httpClient.GetAsync(this.provider.apiUrl + endPoint))
                    {
                        using (HttpContent responseContent = response.Content)
                        {
                            string result = await responseContent.ReadAsStringAsync();
                            if (result != null)
                            {
                                JsonResult jsonResult = JsonConvert.DeserializeObject<JsonResult>(result);

                                GlobalData globalData = _context.GlobalData.FirstOrDefault();
                                if (globalData == null)
                                {
                                    globalData = new GlobalData();
                                }

                                globalData.activeCurrencies = jsonResult.data.ActiveCryptocurrencies;
                                globalData.activeMarkets = jsonResult.data.ActiveMarkets;
                                globalData.bitcoinDominance = jsonResult.data.BitcoinPercentageOfMarketCap;
                                globalData.lastUpdated = HelperFunctions.UnixTimestampToDateTime(jsonResult.data.LastUpdated);
                                globalData.provider = this.provider;
                                globalData.providerId = this.provider.providerID;
                                globalData.totalMarketCap = jsonResult.data.Quotes["USD"].TotalMarketCap;

                                _context.GlobalData.Update(globalData);
                                _context.SaveChanges();

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }
    }
}
