using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HospitalController : ControllerBase
    {
        private readonly HospitalManagementDbContext _context;

        public HospitalController(HospitalManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospitals()
        {
            return await _context.Hospitals
                .Include(h => h.Address)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hospital>> GetHospital(int id)
        {
            var hospital = await _context.Hospitals
                .Include(h => h.Address)
                .FirstOrDefaultAsync(h => h.Hospital_ID == id);

            if (hospital == null)
            {
                return NotFound();
            }

            return hospital;
        }
    }
    
}
