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
    public class IndexModel : PageModel
    {
        private readonly Crypton.Data.ApplicationDbContext _context;

        public IndexModel(Crypton.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Log> Log { get;set; }

        public async Task OnGetAsync()
        {
            Log = await _context.Log
                .Include(l => l.alert)
                .Include(l => l.alertType).ToListAsync();
        }
    }
}
