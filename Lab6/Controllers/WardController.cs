using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class WardController : ControllerBase
    {
        private readonly HospitalManagementDbContext _context;

        public WardController(HospitalManagementDbContext context)
        {
            _context = context;
        }

        // Версія 1: Базовий список палат
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<Ward>>> GetWardsV1()
        {
            return await _context.Wards.ToListAsync();
        }

        // Версія 2: Список палат із додатковими даними
        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<IEnumerable<object>>> GetWardsV2()
        {
            return await _context.Wards
                .Select(ward => new
                {
                    ward.Ward_ID,
                    ward.WardName,
                    ward.WardLocation,
                    ward.WardDescription,
                    AdditionalInfo = $"Ward {ward.WardName} details" 
                })
                .ToListAsync();
        }

        // Версія 1: Отримання палати за ID
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Ward>> GetWardV1(int id)
        {
            var ward = await _context.Wards.FindAsync(id);

            if (ward == null)
            {
                return NotFound();
            }

            return ward;
        }

        // Версія 2: Отримання палати за ID з додатковою інформацією
        [HttpGet("{id}")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<object>> GetWardV2(int id)
        {
            var ward = await _context.Wards.FindAsync(id);

            if (ward == null)
            {
                return NotFound();
            }

            return new
            {
                ward.Ward_ID,
                ward.WardName,
                ward.WardLocation,
                ward.WardDescription,
                AdditionalInfo = $"Ward {ward.WardName} details"
            };
        }
    }
}
