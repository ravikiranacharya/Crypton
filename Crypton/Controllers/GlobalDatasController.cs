using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crypton.Data;
using Crypton.Models.CurrencyViewModels;

namespace Crypton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalDatasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GlobalDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GlobalDatas
        [HttpGet]
        public IEnumerable<GlobalData> GetGlobalData()
        {
            return _context.GlobalData;
        }

        // GET: api/GlobalDatas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGlobalData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var globalData = await _context.GlobalData.FindAsync(id);

            if (globalData == null)
            {
                return NotFound();
            }

            return Ok(globalData);
        }

        // PUT: api/GlobalDatas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGlobalData([FromRoute] int id, [FromBody] GlobalData globalData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != globalData.id)
            {
                return BadRequest();
            }

            _context.Entry(globalData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GlobalDataExists(id))
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

        // POST: api/GlobalDatas
        [HttpPost]
        public async Task<IActionResult> PostGlobalData([FromBody] GlobalData globalData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GlobalData.Add(globalData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGlobalData", new { id = globalData.id }, globalData);
        }

        // DELETE: api/GlobalDatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGlobalData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var globalData = await _context.GlobalData.FindAsync(id);
            if (globalData == null)
            {
                return NotFound();
            }

            _context.GlobalData.Remove(globalData);
            await _context.SaveChangesAsync();

            return Ok(globalData);
        }

        private bool GlobalDataExists(int id)
        {
            return _context.GlobalData.Any(e => e.id == id);
        }
    }
}