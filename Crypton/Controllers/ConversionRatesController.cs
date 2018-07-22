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
    [Route("api/conversionrates")]
    [ApiController]
    public class ConversionRatesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConversionRatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ConversionRates
        [HttpGet]
        public IEnumerable<ConversionRate> GetConversionRate()
        {
            return _context.ConversionRate;
        }

        // GET: api/ConversionRates/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConversionRate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var conversionRate = await _context.ConversionRate.FindAsync(id);

            if (conversionRate == null)
            {
                return NotFound();
            }

            return Ok(conversionRate);
        }

        // PUT: api/ConversionRates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConversionRate([FromRoute] int id, [FromBody] ConversionRate conversionRate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != conversionRate.conversionID)
            {
                return BadRequest();
            }

            _context.Entry(conversionRate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConversionRateExists(id))
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

        // POST: api/ConversionRates
        [HttpPost]
        public async Task<IActionResult> PostConversionRate([FromBody] ConversionRate conversionRate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ConversionRate.Add(conversionRate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConversionRate", new { id = conversionRate.conversionID }, conversionRate);
        }

        // DELETE: api/ConversionRates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConversionRate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var conversionRate = await _context.ConversionRate.FindAsync(id);
            if (conversionRate == null)
            {
                return NotFound();
            }

            _context.ConversionRate.Remove(conversionRate);
            await _context.SaveChangesAsync();

            return Ok(conversionRate);
        }

        private bool ConversionRateExists(int id)
        {
            return _context.ConversionRate.Any(e => e.conversionID == id);
        }
    }
}