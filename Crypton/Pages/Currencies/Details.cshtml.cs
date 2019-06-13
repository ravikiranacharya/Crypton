using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crypton.Data;
using Crypton.Models.CurrencyViewModels;

namespace Crypton.Pages.Currencies
{
    public class DetailsModel : PageModel
    {
        private readonly Crypton.Data.ApplicationDbContext _context;

        public DetailsModel(Crypton.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Currency Currency { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Currency = await _context.Currency
                .Include(c => c.provider).FirstOrDefaultAsync(m => m.currencyID == id);

            if (Currency == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
