using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Crypton.Data;
using Crypton.Models.CurrencyViewModels;

namespace Crypton.Pages.Global
{
    public class EditModel : PageModel
    {
        private readonly Crypton.Data.ApplicationDbContext _context;

        public EditModel(Crypton.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GlobalData GlobalData { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GlobalData = await _context.GlobalData
                .Include(g => g.provider).FirstOrDefaultAsync(m => m.id == id);

            if (GlobalData == null)
            {
                return NotFound();
            }
           ViewData["providerId"] = new SelectList(_context.Provider, "providerID", "apiUrl");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(GlobalData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GlobalDataExists(GlobalData.id))
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

        private bool GlobalDataExists(int id)
        {
            return _context.GlobalData.Any(e => e.id == id);
        }
    }
}
