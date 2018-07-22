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

namespace Crypton.Pages.Variations
{
    public class EditModel : PageModel
    {
        private readonly Crypton.Data.ApplicationDbContext _context;

        public EditModel(Crypton.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Variation Variation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Variation = await _context.Variation.FirstOrDefaultAsync(m => m.variationID == id);

            if (Variation == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Variation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VariationExists(Variation.variationID))
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

        private bool VariationExists(int id)
        {
            return _context.Variation.Any(e => e.variationID == id);
        }
    }
}
