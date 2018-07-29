using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crypton.Data;
using Crypton.Models.CurrencyViewModels;

namespace Crypton.Pages.Global
{
    public class DetailsModel : PageModel
    {
        private readonly Crypton.Data.ApplicationDbContext _context;

        public DetailsModel(Crypton.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
