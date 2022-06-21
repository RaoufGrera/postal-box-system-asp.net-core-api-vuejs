using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemData;
using SystemData.Models;

namespace BoxSystem10.Controllers.v1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly SystemContext _context;

        public OfficesController(SystemContext context)
        {
            _context = context;
        }

        // GET: api/Offices
        [HttpGet]
        public IEnumerable<Office> GetOffice()
        {
            return _context.Office;
        }

        // GET: api/Offices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOffice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var office = await _context.Office.FindAsync(id);

            if (office == null)
            {
                return NotFound();
            }

            return Ok(office);
        }

        // PUT: api/Offices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOffice([FromRoute] int id, [FromBody] Office office)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != office.Id)
            {
                return BadRequest();
            }

            _context.Entry(office).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficeExists(id))
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

        // POST: api/Offices
        [HttpPost]
        public async Task<IActionResult> PostOffice([FromBody] Office office)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Office.Add(office);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOffice", new { id = office.Id }, office);
        }

        // DELETE: api/Offices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var office = await _context.Office.FindAsync(id);
            if (office == null)
            {
                return NotFound();
            }

            _context.Office.Remove(office);
            await _context.SaveChangesAsync();

            return Ok(office);
        }

        private bool OfficeExists(int id)
        {
            return _context.Office.Any(e => e.Id == id);
        }
    }
}