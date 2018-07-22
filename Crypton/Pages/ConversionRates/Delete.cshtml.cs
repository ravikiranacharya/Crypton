using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crypton.Data;
using Crypton.Models.PriceViewModels;

namespace Crypton.Pages.ConversionRates
{
    public class DeleteModel : PageModel
    {
        private readonly Crypton.Data.ApplicationDbContext _context;

        public DeleteModel(Crypton.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ConversionRate = await _context.ConversionRate.FindAsync(id);

            if (ConversionRate != null)
            {
                _context.ConversionRate.Remove(ConversionRate);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
