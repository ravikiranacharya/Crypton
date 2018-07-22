using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crypton.Data;
using Crypton.Models.AlertViewModels;

namespace Crypton.Controllers
{
    [Route("api/alerts")]
    [ApiController]
    public class AlertsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlertsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Alerts
        [HttpGet]
        public IEnumerable<Alert> GetAlert()
        {
            return _context.Alert;
        }

        // GET: api/Alerts/5
        //[Route("{id: int}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlert([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alert = await _context.Alert.FindAsync(id);

            if (alert == null)
            {
                return NotFound();
            }

            return Ok(alert);
        }

        // PUT: api/Alerts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlert([FromRoute] int id, [FromBody] Alert alert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alert.alertID)
            {
                return BadRequest();
            }

            _context.Entry(alert).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertExists(id))
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

        // POST: api/Alerts
        [HttpPost]
        public async Task<IActionResult> PostAlert([FromBody] Alert alert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Alert.Add(alert);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlert", new { id = alert.alertID }, alert);
        }

        // DELETE: api/Alerts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alert = await _context.Alert.FindAsync(id);
            if (alert == null)
            {
                return NotFound();
            }

            _context.Alert.Remove(alert);
            await _context.SaveChangesAsync();

            return Ok(alert);
        }

        private bool AlertExists(int id)
        {
            return _context.Alert.Any(e => e.alertID == id);
        }
    }
}