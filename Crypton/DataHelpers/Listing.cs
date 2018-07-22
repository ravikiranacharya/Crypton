using Crypton.Controllers;
using Crypton.Data;
using Crypton.Models.CurrencyViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Crypton.DataHelpers
{
    /* This is based on CoinMarketCap's public API */
    public class Listing
    {

        public class ParentObject
        {
            public List<ChildObject> data { get; set; }
        }

        public class ChildObject
        {
            public int id { get; set; }
            public string name { get; set; }
            public string symbol { get; set; }
            public string website_slug { get; set; }

        }

        /* Initializing a new Database context specific to this class */
        private readonly ApplicationDbContext _context;

        /* Initializing a provider to incorporate more data providers like CoinGecko */
        public Provider provider { get; set; }

        /* Constructor for Listing with a db context */
        public Listing(Crypton.Data.ApplicationDbContext context)
        {
            this._context = context;
        }
                
        public async Task<bool> UpdateCoinListAsync()
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
                                List<Currency> currencies = new List<Currency>();
                                var jsonObj = JsonConvert.DeserializeObject<ParentObject>(result);

                                foreach(ChildObject listing in jsonObj.data)
                                {
                                    Currency currency = new Currency();
                                    currency.providerCurrencyID = Convert.ToString(listing.id);
                                    currency.currencyName = listing.name;
                                    currency.currencyCode = listing.symbol;

                                    currencies.Add(currency);                                    
                                }

                                _context.AddRange(currencies);
                                _context.SaveChanges(); /* This is very important */
                                
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
