using Crypton.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypton.DataHelpers.CoinMarketCapHelpers
{
    public class Schedule
    {
        private readonly ApplicationDbContext _context;
        public Schedule(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> RefreshDataAsync()
        {
            try
            {
                var provider = _context.Provider.First();

                /* Global Data */
                GlobalDataModel globalData = new GlobalDataModel(_context);
                globalData.provider = provider;

                /* Listings */
                ListingModel listings = new ListingModel(_context);
                listings.provider = provider;

                /* Ticker */
                Ticker ticker = new Ticker(_context);
                ticker.provider = provider;

                await globalData.UpdateGlobalDataAsync();
                await listings.UpdateListingsAsync();
                await ticker.UpdateTickerAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }            
        }
    }
}
