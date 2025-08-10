using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Api.Infrastructure;
using HotelBooking.Api.Contracts;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController : ControllerBase
    {
        private readonly HotelContext _db;
        public RequestsController(HotelContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialRequestDto>>> GetAll()
        {
            var rows = await _db.Requests
                .AsNoTracking()
                .Select(r => new SpecialRequestDto(r.RequestId, r.Description, r.Category))
                .ToListAsync();

            return Ok(rows);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SpecialRequestDto>> Get(int id)
        {
            var dto = await _db.Requests
                .AsNoTracking()
                .Where(r => r.RequestId == id)
                .Select(r => new SpecialRequestDto(r.RequestId, r.Description, r.Category))
                .FirstOrDefaultAsync();

            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<SpecialRequestDto>> Create([FromBody] SpecialRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Description))
                return BadRequest("Description required.");

            var e = new Domain.SpecialRequest { Description = dto.Description, Category = dto.Category };
            _db.Requests.Add(e);
            await _db.SaveChangesAsync();

            var created = new SpecialRequestDto(e.RequestId, e.Description, e.Category);
            return CreatedAtAction(nameof(Get), new { id = e.RequestId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] SpecialRequestDto dto)
        {
            if (id != dto.RequestId) return BadRequest("Id mismatch.");

            var r = await _db.Requests.FindAsync(id);
            if (r is null) return NotFound();

            r.Description = dto.Description;
            r.Category = dto.Category;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var inUse = await _db.Bookings.AnyAsync(b => b.RequestId == id);
            if (inUse) return Conflict("Request in use by bookings.");

            var r = await _db.Requests.FindAsync(id);
            if (r is null) return NotFound();

            _db.Requests.Remove(r);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
