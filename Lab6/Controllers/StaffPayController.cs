using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffPayController : ControllerBase
    {
        private readonly HospitalManagementDbContext _context;

        public StaffPayController(HospitalManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffPay>>> GetStaffPays()
        {
            return await _context.StaffPay
                .Include(s => s.Staff)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffPay>> GetStaffPay(int id)
        {
            var staffPay = await _context.StaffPay
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(s => s.Pay_ID == id);

            if (staffPay == null)
            {
                return NotFound();
            }

            return staffPay;
        }

        [HttpPost]
        public async Task<ActionResult<StaffPay>> CreateStaffPay(StaffPay staffPay)
        {
            _context.StaffPay.Add(staffPay);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStaffPay), new { id = staffPay.Pay_ID }, staffPay);
        }
    }
}
