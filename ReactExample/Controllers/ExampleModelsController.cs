using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactExample.Data;
using ReactExample.Models;

namespace ReactExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExampleModelsController(ApplicationDbContext context)
        {
            _context = context;
          
        }

        // GET: api/ExampleModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExampleModel>>> GetExampleModel()
        {
            return await _context.ExampleModel.ToListAsync();
        }

        // GET: api/ExampleModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExampleModel>> GetExampleModel(int id)
        {
            var exampleModel = await _context.ExampleModel.FindAsync(id);

            if (exampleModel == null)
            {
                return NotFound();
            }

            return exampleModel;
        }

        // PUT: api/ExampleModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExampleModel(int id, ExampleModel exampleModel)
        {
            if (id != exampleModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(exampleModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExampleModelExists(id))
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

        // POST: api/ExampleModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExampleModel>> PostExampleModel(ExampleModel exampleModel)
        {
            _context.ExampleModel.Add(exampleModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExampleModel", new { id = exampleModel.Id }, exampleModel);
        }

        // DELETE: api/ExampleModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExampleModel(int id)
        {
            var exampleModel = await _context.ExampleModel.FindAsync(id);
            if (exampleModel == null)
            {
                return NotFound();
            }

            _context.ExampleModel.Remove(exampleModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExampleModelExists(int id)
        {
            return _context.ExampleModel.Any(e => e.Id == id);
        }
    }
}
