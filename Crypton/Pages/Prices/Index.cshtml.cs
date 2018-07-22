using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crypton.Data;
using Crypton.Models.PriceViewModels;

namespace Crypton.Pages.Prices
{
    public class IndexModel : PageModel
    {
        private readonly Crypton.Data.ApplicationDbContext _context;

        public IndexModel(Crypton.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CurrencyPrice> CurrencyPrice { get;set; }

        public async Task OnGetAsync()
        {
            CurrencyPrice = await _context.CurrencyPrice
                .Include(c => c.currency)
                .Include(c => c.provider).ToListAsync();
        }
    }
}
