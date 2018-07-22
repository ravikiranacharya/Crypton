using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Crypton.Data;
using Crypton.Models.AlertViewModels;

namespace Crypton.Pages.Logs
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
        ViewData["alertID"] = new SelectList(_context.Set<Alert>(), "alertID", "alertID");
        ViewData["alertTypeID"] = new SelectList(_context.AlertType, "alertTypeID", "alertType");
            return Page();
        }

        [BindProperty]
        public Log Log { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Log.Add(Log);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}