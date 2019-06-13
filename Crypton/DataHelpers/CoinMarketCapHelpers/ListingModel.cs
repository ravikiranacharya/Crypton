using Crypton.Data;
using Crypton.Models.CurrencyViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Crypton.DataHelpers.CoinMarketCapHelpers
{
    public class ListingModel
    {
        public partial class JsonResult
        {
            [JsonProperty("data")]
            public List<Datum> Data { get; set; }

            [JsonProperty("metadata")]
            public Metadata Metadata { get; set; }
        }

        public partial class Datum
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("symbol")]
            public string Symbol { get; set; }

            [JsonProperty("website_slug")]
            public string WebsiteSlug { get; set; }
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
        public ListingModel(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> UpdateListingsAsync()
        {
            try
            {
                string endPoint = "listings/"; /* This is the end point for the api to fetch all the listings */
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
                                
                                if(jsonResult.Data.Any())
                                {
                                    //List<Currency> newListings = new List<Currency>();
                                    var newListings = from listing in jsonResult.Data
                                                join currency in _context.Currency.ToList()
                                                    on listing.Id.ToString() equals currency.providerCurrencyID
                                                into Updates
                                                from u in Updates.DefaultIfEmpty()
                                                where u == null
                                                select new Currency
                                                {
                                                    providerCurrencyID = listing.Id.ToString(),
                                                    currencyName = listing.Name,
                                                    currencyCode = listing.Symbol,
                                                    description = listing.WebsiteSlug                   
                                                };

                                    if (newListings.Any())
                                    {
                                        List<Currency> currencies = new List<Currency>();
                                        foreach(Currency c in newListings)
                                        {
                                            currencies.Add(new Currency
                                            {
                                                providerCurrencyID = c.providerCurrencyID,
                                                currencyName = c.currencyName,
                                                currencyCode = c.currencyCode,
                                                description = c.description,
                                                isEnabled = true,
                                                providerID = this.provider.providerID
                                            });
                                        }
                                        _context.Currency.UpdateRange(currencies);
                                        _context.SaveChanges();
                                    }
                                }
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
