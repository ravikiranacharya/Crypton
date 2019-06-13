using Crypton.Data;
using Crypton.Extensions;
using Crypton.Models.CurrencyViewModels;
using Crypton.Models.PriceViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

/// <summary>
/// Namespace containing the helper classes to deserialize the JSON output from CoinMarketCap's Ticker API and insert into our database
/// </summary>
namespace Crypton.DataHelpers.CoinMarketCapHelpers
{
    /// <summary>
    /// Model for Ticker
    /// </summary>
    /// 
    /* This is a custom class I'm writing to de-serialize the JSON response. I am comfortable using Dictionaries in .Net and hence I am using them to shorten my models.
        * If you want to define a simple model, you can use this useful converter which does the job for you: https://app.quicktype.io/#l=cs&r=json2csharp */

    public class Ticker
    {
        public class JsonResult
        {
            /* The parent object for the JSON response is "data" */
            [JsonProperty("data")]
            public Dictionary<string, Datum> Data { get; set; }

            [JsonProperty("metadata")]
            public Metadata Metadata { get; set; }
        }

        public class Datum
        {
            [JsonProperty("id")]
            public int id { get; set; }

            [JsonProperty("name")]
            public string name { get; set; }

            [JsonProperty("symbol")]
            public string symbol { get; set; }

            [JsonProperty("website_slug")]
            public string website_slug { get; set; }

            [JsonProperty("rank")]
            public int rank { get; set; }

            [JsonProperty("circulating_supply")]
            public double circulating_supply { get; set; }

            [JsonProperty("total_supply")]
            public long total_supply { get; set; }

            [JsonProperty("max_supply")]
            public long? max_supply { get; set; }
            /* Maximum Supply can be null and hence the question mark ? */

            [JsonProperty("last_updated")]
            public long last_updated { get; set; }

            [JsonProperty("quotes")]
            public Dictionary<string, Quote> quotes { get; set; }
        }

        public class Quote
        {
            [JsonProperty("price")]
            public double price { get; set; }

            [JsonProperty("volume_24h")]
            public double volume_24h { get; set; }

            [JsonProperty("market_cap")]
            public double market_cap { get; set; }

            [JsonProperty("percent_change_1h")]
            public double percent_change_1h { get; set; }

            [JsonProperty("percent_change_24h")]
            public double percent_change_24h { get; set; }

            [JsonProperty("percent_change_7d")]
            public double percent_change_7d { get; set; }
        }

        public partial class Metadata
        {
            [JsonProperty("timestamp")]
            public long Timestamp { get; set; }

            [JsonProperty("num_cryptocurrencies")]
            public long NumCryptocurrencies { get; set; }

            [JsonProperty("error")]
            public object Error { get; set; }
        }
        /* Initializing a new Database context specific to this class */
        private readonly ApplicationDbContext _context;
        /* Initializing a provider to incorporate more data providers like CoinGecko */
        public Provider provider { get; set; }

        /* Constructor for Ticker with a db context */
        public Ticker(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> UpdateTickerAsync()
        {
            try
            {
                string endPoint = "ticker/"; /* This is the end point for the api to fetch all the listings */

                int startAt = 1;
                int endLimit = 100;
                int currencyCount = _context.Currency.Count();

                List<Task> tasks = new List<Task>();

                while(startAt < currencyCount)
                {
                    // Fetch data
                    string parameters = "?start=" + startAt + "&limit=" + endLimit;
                    string url = this.provider.apiUrl + endPoint + parameters;

                    var task = Task.Run(() => RunFetcher(url));
                    tasks.Add(task);

                    startAt += endLimit;
                }

                await Task.WhenAll(tasks);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }

        public async Task<bool> RunFetcher(string endPoint)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    using (HttpResponseMessage response = await httpClient.GetAsync(endPoint))
                    {
                        using (HttpContent responseContent = response.Content)
                        {
                            string result = await responseContent.ReadAsStringAsync();
                            if (result != null)
                            {

                                JsonResult jsonResult = JsonConvert.DeserializeObject<JsonResult>(result);

                                if (jsonResult.Data.Any())
                                {
                                    //List<Currency> newListings = new List<Currency>();
                                    var latestTicker = from ticker in jsonResult.Data
                                                       join currency in _context.Currency.ToList()
                                                           on ticker.Value.id.ToString() equals currency.providerCurrencyID
                                                       where HelperFunctions.UnixTimestampToDateTime(ticker.Value.last_updated) > currency.lastUpdated
                                                       select new
                                                       {
                                                           Currency = currency,
                                                           totalVolume = ticker.Value.total_supply,
                                                           currentVolume = ticker.Value.circulating_supply,
                                                           currentMarketCap = ticker.Value.quotes["USD"].market_cap,
                                                           marketRank = ticker.Value.rank,
                                                           lastUpdated = DateTime.Now,

                                                           CurrencyPrice = new CurrencyPrice
                                                           {
                                                               currencyID = currency.currencyID,
                                                               providerID = currency.providerID,
                                                               priceDate = HelperFunctions.UnixTimestampToDateTime(ticker.Value.last_updated),
                                                               priceUSD = ticker.Value.quotes["USD"].price
                                                               //Add percentage changes
                                                           }
                                                       };

                                    var tickerResult = latestTicker.ToList();
                                    tickerResult.ForEach(i =>
                                    {
                                        i.Currency.totalVolume = i.totalVolume;
                                        i.Currency.currentVolume = i.currentVolume;
                                        i.Currency.currentMarketCap = i.currentMarketCap;
                                        i.Currency.marketRank = i.marketRank;
                                        i.Currency.lastUpdated = i.lastUpdated;
                                    });




                                    List<Currency> currencies = tickerResult.Select(i => i.Currency).ToList();
                                    List<CurrencyPrice> currencyPrices = tickerResult.Select(i => i.CurrencyPrice).ToList();

                                    if (currencies.Any())
                                    {
                                        /* Update currencies */
                                        _context.Currency.UpdateRange(currencies);
                                    }

                                    if (currencyPrices.Any())
                                    {
                                        /* Update Prices */
                                        _context.CurrencyPrice.AddRange(currencyPrices);
                                    }

                                    _context.SaveChanges();
                                }
                            }
                        }
                    }
                }
                return true;
            }catch(Exception e)
            {
                return false;
            }
           
        }

    }
}
