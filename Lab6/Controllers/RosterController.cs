using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
    [Authorize]
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

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<RosterOfStaffOnShift>>> SearchRoster(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] List<int>? staffIds,
            [FromQuery] string? shiftNameStart)
        {
            
            DateTime? utcStartDate = startDate?.ToUniversalTime();
            DateTime? utcEndDate = endDate?.ToUniversalTime();

            
            var query = _context.RosterOfStaffOnShifts
                .Include(r => r.Staff)
                .Include(r => r.Shift)
                .AsQueryable();

            
            if (utcStartDate.HasValue)
                query = query.Where(r => r.StartDate >= utcStartDate);

            if (utcEndDate.HasValue)
                query = query.Where(r => r.EndDate <= utcEndDate);

            if (staffIds != null && staffIds.Any())
                query = query.Where(r => staffIds.Contains(r.Staff_ID));

            if (!string.IsNullOrEmpty(shiftNameStart))
                query = query.Where(r => r.Shift.ShiftName.StartsWith(shiftNameStart));

            var results = await query.ToListAsync();

            TimeZoneInfo ukraineTimeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
            results.ForEach(r =>
            {
                r.StartDate = TimeZoneInfo.ConvertTimeFromUtc(r.StartDate, ukraineTimeZone);
                r.EndDate = TimeZoneInfo.ConvertTimeFromUtc(r.EndDate, ukraineTimeZone);
            });

            return results;
        }
    }
}
