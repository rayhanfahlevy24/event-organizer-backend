using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using event_organizer.Data;
using event_organizer.Models;

namespace event_organizer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILogger<TenantsController> _logger;

        public TenantsController(AppDbContext db, ILogger<TenantsController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? q)
        {
            var query = _db.Tenants.AsNoTracking().OrderByDescending(t => t.CreatedAt).AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                var qq = q.Trim().ToLower();
                query = query.Where(t =>
                    t.TenantName.ToLower().Contains(qq) ||
                    t.TenantType.ToString().ToLower().Contains(qq) ||
                    (t.BoothNum ?? "").ToLower().Contains(qq) ||
                    (t.TenantPhone ?? "").ToLower().Contains(qq) ||
                    (t.TenantAdress ?? "").ToLower().Contains(qq)
                );
            }

            var list = await query.ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var t = await _db.Tenants.FindAsync(id);
            if (t == null) return NotFound();
            return Ok(t);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tenant input)
        {
            if (string.IsNullOrWhiteSpace(input.TenantName))
                return BadRequest(new { error = "tenant_name is required" });

            if (!Enum.IsDefined(typeof(TenantType), input.TenantType))
                return BadRequest(new { error = "tenant_type is invalid" });

            if (input.TenantType == TenantType.booth && string.IsNullOrWhiteSpace(input.BoothNum))
                return BadRequest(new { error = "booth_num is required for booth type" });

            if (input.TenantType == TenantType.space_only && (input.AreaSm == null || input.AreaSm <= 0))
                return BadRequest(new { error = "area_sm is required and must be > 0 for space_only type" });

            input.CreatedAt = DateTime.UtcNow;
            _db.Tenants.Add(input);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
        }
    }
}
