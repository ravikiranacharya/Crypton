﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crypton.Data;
using Crypton.Models.PriceViewModels;

namespace Crypton.Pages.Prices
{
    public class DeleteModel : PageModel
    {
        private readonly Crypton.Data.ApplicationDbContext _context;

        public DeleteModel(Crypton.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CurrencyPrice CurrencyPrice { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CurrencyPrice = await _context.CurrencyPrice
                .Include(c => c.currency)
                .Include(c => c.provider).FirstOrDefaultAsync(m => m.currencyPriceID == id);

            if (CurrencyPrice == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CurrencyPrice = await _context.CurrencyPrice.FindAsync(id);

            if (CurrencyPrice != null)
            {
                _context.CurrencyPrice.Remove(CurrencyPrice);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
