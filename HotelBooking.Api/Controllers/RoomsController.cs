using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Api.Infrastructure;
using HotelBooking.Api.Contracts;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly HotelContext _db;
        public RoomsController(HotelContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetAll()
        {
            var rows = await _db.Rooms
                .AsNoTracking()
                .Select(r => new RoomDto(r.RoomId, r.RoomType, r.BasePrice))
                .ToListAsync();

            return Ok(rows);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoomDto>> Get(int id)
        {
            var dto = await _db.Rooms
                .AsNoTracking()
                .Where(r => r.RoomId == id)
                .Select(r => new RoomDto(r.RoomId, r.RoomType, r.BasePrice))
                .FirstOrDefaultAsync();

            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<RoomDto>> Create([FromBody] RoomDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.RoomType) || dto.BasePrice <= 0)
                return BadRequest("Invalid room.");

            var e = new Domain.Room { RoomType = dto.RoomType, BasePrice = dto.BasePrice };
            _db.Rooms.Add(e);
            await _db.SaveChangesAsync();

            var created = new RoomDto(e.RoomId, e.RoomType, e.BasePrice);
            return CreatedAtAction(nameof(Get), new { id = e.RoomId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoomDto dto)
        {
            if (id != dto.RoomId) return BadRequest("Id mismatch.");

            var r = await _db.Rooms.FindAsync(id);
            if (r is null) return NotFound();

            r.RoomType = dto.RoomType;
            r.BasePrice = dto.BasePrice;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var inUse = await _db.Bookings.AnyAsync(b => b.RoomId == id);
            if (inUse) return Conflict("Room in use by bookings.");

            var r = await _db.Rooms.FindAsync(id);
            if (r is null) return NotFound();

            _db.Rooms.Remove(r);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
