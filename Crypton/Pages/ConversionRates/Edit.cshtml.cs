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

namespace Crypton.Pages.ConversionRates
{
    public class EditModel : PageModel
    {
        private readonly Crypton.Data.ApplicationDbContext _context;

        public EditModel(Crypton.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ConversionRate ConversionRate { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ConversionRate = await _context.ConversionRate
                .Include(c => c.sourceCurrency)
                .Include(c => c.targetCurrency).FirstOrDefaultAsync(m => m.conversionID == id);

            if (ConversionRate == null)
            {
                return NotFound();
            }
           ViewData["sourceCurrencyID"] = new SelectList(_context.Currency, "currencyID", "currencyCode");
           ViewData["targetCurrencyID"] = new SelectList(_context.Currency, "currencyID", "currencyCode");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ConversionRate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConversionRateExists(ConversionRate.conversionID))
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

        private bool ConversionRateExists(int id)
        {
            return _context.ConversionRate.Any(e => e.conversionID == id);
        }
    }
}
