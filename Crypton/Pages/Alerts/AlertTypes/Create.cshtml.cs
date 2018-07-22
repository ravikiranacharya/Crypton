using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Crypton.Data;
using Crypton.Models.AlertViewModels;

namespace Crypton.Pages.Alerts.AlertTypes
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
            return Page();
        }

        [BindProperty]
        public AlertType AlertType { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AlertType.Add(AlertType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}