using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Crypton.Data;
using Crypton.Models.PriceViewModels;

namespace Crypton.Pages.Prices
{
    public class EditModel : PageModel
    {
        private readonly Crypton.Data.ApplicationDbContext _context;

        public EditModel(Crypton.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CurrencyPrice CurrencyPrice { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CurrencyPrice = await _context.CurrencyPrice
                .Include(c => c.currency)
                .Include(c => c.provider).FirstOrDefaultAsync(m => m.currencyPriceID == id);

            if (CurrencyPrice == null)
            {
                return NotFound();
            }
           ViewData["currencyID"] = new SelectList(_context.Currency, "currencyID", "currencyCode");
           ViewData["providerID"] = new SelectList(_context.Provider, "providerID", "apiUrl");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CurrencyPrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyPriceExists(CurrencyPrice.currencyPriceID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CurrencyPriceExists(int id)
        {
            return _context.CurrencyPrice.Any(e => e.currencyPriceID == id);
        }
    }
}
