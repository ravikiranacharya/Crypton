using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Crypton.Data;
using Crypton.Models.PriceViewModels;

namespace Crypton.Pages.ConversionRates
{
    public class CreateModel : PageModel
    {
        private readonly Crypton.Data.ApplicationDbContext _context;

        public CreateModel(Crypton.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["sourceCurrencyID"] = new SelectList(_context.Currency, "currencyID", "currencyCode");
        ViewData["targetCurrencyID"] = new SelectList(_context.Currency, "currencyID", "currencyCode");
            return Page();
        }

        [BindProperty]
        public ConversionRate ConversionRate { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ConversionRate.Add(ConversionRate);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}