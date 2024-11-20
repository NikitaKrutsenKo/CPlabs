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
    public class StaffController : ControllerBase
    {
        private readonly HospitalManagementDbContext _context;

        public StaffController(HospitalManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
        {
            return await _context.Staff
                .Include(s => s.Hospital)
                .Include(s => s.Address)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaff(int id)
        {
            var staff = await _context.Staff
                .Include(s => s.Hospital)
                .Include(s => s.Address)
                .FirstOrDefaultAsync(s => s.Staff_ID == id);

            if (staff == null)
            {
                return NotFound();
            }

            return staff;
        }
    }

    
}
