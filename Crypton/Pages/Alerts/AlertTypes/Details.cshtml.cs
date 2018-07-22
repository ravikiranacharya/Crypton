using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crypton.Data;
using Crypton.Models.AlertViewModels;

namespace Crypton.Pages.Alerts.AlertTypes
{
    public class DetailsModel : PageModel
    {
        private readonly Crypton.Data.ApplicationDbContext _context;

        public DetailsModel(Crypton.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public AlertType AlertType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AlertType = await _context.AlertType.FirstOrDefaultAsync(m => m.alertTypeID == id);

            if (AlertType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
