using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crypton.DataHelpers;
using Crypton.DataHelpers.CoinMarketCapHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crypton.Pages.Testing
{
    public class TestModel : PageModel
    {
        private readonly Crypton.Data.ApplicationDbContext _context;

        public TestModel(Crypton.Data.ApplicationDbContext context)
        {
            this._context = context;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            Schedule schedule = new Schedule(_context);
            await schedule.RefreshDataAsync();

            return RedirectToPage("./Test");
        }
    }
}