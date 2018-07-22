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
    public class IndexModel : PageModel
    {
        private readonly Crypton.Data.ApplicationDbContext _context;

        public IndexModel(Crypton.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<AlertType> AlertType { get;set; }

        public async Task OnGetAsync()
        {
            AlertType = await _context.AlertType.ToListAsync();
        }
    }
}
