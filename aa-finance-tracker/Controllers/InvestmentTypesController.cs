using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AAExpenseTracker.Domain.Data;
using AAExpenseTracker.Domain.Entities;

namespace AAFinanceTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentTypesController : ControllerBase
    {
        private readonly FinanceTrackerDbContext _context;

        public InvestmentTypesController(FinanceTrackerDbContext context)
        {
            _context = context;
        }

        // GET: api/InvestmentTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvestmentType>>> GetInvestmentsTypes()
        {
            return await _context.InvestmentsTypes.ToListAsync();
        }

        // GET: api/InvestmentTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvestmentType>> GetInvestmentType(int id)
        {
            var investmentType = await _context.InvestmentsTypes.FindAsync(id);

            if (investmentType == null)
            {
                return NotFound();
            }

            return investmentType;
        }

        // PUT: api/InvestmentTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvestmentType(int id, InvestmentType investmentType)
        {
            if (id != investmentType.Type)
            {
                return BadRequest();
            }

            _context.Entry(investmentType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestmentTypeExists(id))
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

        // POST: api/InvestmentTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InvestmentType>> PostInvestmentType(InvestmentType investmentType)
        {
            _context.InvestmentsTypes.Add(investmentType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvestmentType", new { id = investmentType.Type }, investmentType);
        }

        // DELETE: api/InvestmentTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestmentType(int id)
        {
            var investmentType = await _context.InvestmentsTypes.FindAsync(id);
            if (investmentType == null)
            {
                return NotFound();
            }

            _context.InvestmentsTypes.Remove(investmentType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvestmentTypeExists(int id)
        {
            return _context.InvestmentsTypes.Any(e => e.Type == id);
        }
    }
}
