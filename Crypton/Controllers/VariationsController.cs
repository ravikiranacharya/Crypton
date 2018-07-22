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
    [Route("api/variations")]
    [ApiController]
    public class VariationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VariationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Variations
        [HttpGet]
        public IEnumerable<Variation> GetVariation()
        {
            return _context.Variation;
        }

        // GET: api/Variations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVariation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var variation = await _context.Variation.FindAsync(id);

            if (variation == null)
            {
                return NotFound();
            }

            return Ok(variation);
        }

        // PUT: api/Variations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVariation([FromRoute] int id, [FromBody] Variation variation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != variation.variationID)
            {
                return BadRequest();
            }

            _context.Entry(variation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VariationExists(id))
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

        // POST: api/Variations
        [HttpPost]
        public async Task<IActionResult> PostVariation([FromBody] Variation variation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Variation.Add(variation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVariation", new { id = variation.variationID }, variation);
        }

        // DELETE: api/Variations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVariation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var variation = await _context.Variation.FindAsync(id);
            if (variation == null)
            {
                return NotFound();
            }

            _context.Variation.Remove(variation);
            await _context.SaveChangesAsync();

            return Ok(variation);
        }

        private bool VariationExists(int id)
        {
            return _context.Variation.Any(e => e.variationID == id);
        }
    }
}