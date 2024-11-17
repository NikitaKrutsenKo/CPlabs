using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffTimeOffController : ControllerBase
    {
        private readonly HospitalManagementDbContext _context;

        public StaffTimeOffController(HospitalManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffTimeOff>>> GetStaffTimeOffs()
        {
            return await _context.StaffTimeOffs
                .Include(s => s.Staff)
                .Include(s => s.RefTimeOffReason)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffTimeOff>> GetStaffTimeOff(int id)
        {
            var staffTimeOff = await _context.StaffTimeOffs
                .Include(s => s.Staff)
                .Include(s => s.RefTimeOffReason)
                .FirstOrDefaultAsync(s => s.StaffTimeOff_ID == id);

            if (staffTimeOff == null)
            {
                return NotFound();
            }

            return staffTimeOff;
        }
    }
}
