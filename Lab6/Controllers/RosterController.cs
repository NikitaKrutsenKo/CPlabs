using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RosterController : ControllerBase
    {
        private readonly HospitalManagementDbContext _context;

        public RosterController(HospitalManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RosterOfStaffOnShift>>> GetRosters()
        {
            return await _context.RosterOfStaffOnShifts
                .Include(r => r.Staff)
                .Include(r => r.Shift)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RosterOfStaffOnShift>> GetRoster(int id)
        {
            var roster = await _context.RosterOfStaffOnShifts
                .Include(r => r.Staff)
                .Include(r => r.Shift)
                .FirstOrDefaultAsync(r => r.Roster_ID == id);

            if (roster == null)
            {
                return NotFound();
            }

            return roster;
        }

        [HttpPost]
        public async Task<ActionResult<RosterOfStaffOnShift>> CreateRoster(RosterOfStaffOnShift roster)
        {
            _context.RosterOfStaffOnShifts.Add(roster);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoster), new { id = roster.Roster_ID }, roster);
        }
    }
}
