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
    [Route("api/alerttypes")]
    [ApiController]
    public class AlertTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlertTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AlertTypes
        [HttpGet]
        public IEnumerable<AlertType> GetAlertType()
        {
            return _context.AlertType;
        }

        // GET: api/AlertTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlertType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alertType = await _context.AlertType.FindAsync(id);

            if (alertType == null)
            {
                return NotFound();
            }

            return Ok(alertType);
        }

        // PUT: api/AlertTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlertType([FromRoute] int id, [FromBody] AlertType alertType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alertType.alertTypeID)
            {
                return BadRequest();
            }

            _context.Entry(alertType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertTypeExists(id))
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

        // POST: api/AlertTypes
        [HttpPost]
        public async Task<IActionResult> PostAlertType([FromBody] AlertType alertType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AlertType.Add(alertType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlertType", new { id = alertType.alertTypeID }, alertType);
        }

        // DELETE: api/AlertTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlertType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alertType = await _context.AlertType.FindAsync(id);
            if (alertType == null)
            {
                return NotFound();
            }

            _context.AlertType.Remove(alertType);
            await _context.SaveChangesAsync();

            return Ok(alertType);
        }

        private bool AlertTypeExists(int id)
        {
            return _context.AlertType.Any(e => e.alertTypeID == id);
        }
    }
}