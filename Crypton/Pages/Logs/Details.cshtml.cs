using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crypton.Data;
using Crypton.Models.AlertViewModels;

namespace Crypton.Pages.Logs
{
    public class DetailsModel : PageModel
    {
        private readonly Crypton.Data.ApplicationDbContext _context;

        public DetailsModel(Crypton.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Log Log { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Log = await _context.Log
                .Include(l => l.alert)
                .Include(l => l.alertType).FirstOrDefaultAsync(m => m.logID == id);

            if (Log == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
