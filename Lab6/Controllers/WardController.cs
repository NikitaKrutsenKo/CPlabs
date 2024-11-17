using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WardController : ControllerBase
    {
        private readonly HospitalManagementDbContext _context;

        public WardController(HospitalManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ward>>> GetWards()
        {
            return await _context.Wards.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ward>> GetWard(int id)
        {
            var ward = await _context.Wards.FindAsync(id);

            if (ward == null)
            {
                return NotFound();
            }

            return ward;
        }
    }
}
