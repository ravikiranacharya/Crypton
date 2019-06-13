using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Crypton.Data;
using Crypton.Models.CurrencyViewModels;

namespace Crypton.Pages.Currencies
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
        ViewData["providerID"] = new SelectList(_context.Provider, "providerID", "apiUrl");
            return Page();
        }

        [BindProperty]
        public Currency Currency { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Currency.Add(Currency);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}