using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crypton.Data;
using Crypton.Models.PriceViewModels;

namespace Crypton.Controllers
{
    [Route("api/prices")]
    [ApiController]
    public class CurrencyPricesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CurrencyPricesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CurrencyPrices
        [HttpGet]
        public IEnumerable<CurrencyPrice> GetCurrencyPrice()
        {
            return _context.CurrencyPrice;
        }

        // GET: api/CurrencyPrices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrencyPrice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currencyPrice = await _context.CurrencyPrice.FindAsync(id);

            if (currencyPrice == null)
            {
                return NotFound();
            }

            return Ok(currencyPrice);
        }

        // PUT: api/CurrencyPrices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrencyPrice([FromRoute] int id, [FromBody] CurrencyPrice currencyPrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != currencyPrice.currencyPriceID)
            {
                return BadRequest();
            }

            _context.Entry(currencyPrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyPriceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CurrencyPrices
        [HttpPost]
        public async Task<IActionResult> PostCurrencyPrice([FromBody] CurrencyPrice currencyPrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CurrencyPrice.Add(currencyPrice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurrencyPrice", new { id = currencyPrice.currencyPriceID }, currencyPrice);
        }

        // DELETE: api/CurrencyPrices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrencyPrice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currencyPrice = await _context.CurrencyPrice.FindAsync(id);
            if (currencyPrice == null)
            {
                return NotFound();
            }

            _context.CurrencyPrice.Remove(currencyPrice);
            await _context.SaveChangesAsync();

            return Ok(currencyPrice);
        }

        private bool CurrencyPriceExists(int id)
        {
            return _context.CurrencyPrice.Any(e => e.currencyPriceID == id);
        }
    }
}