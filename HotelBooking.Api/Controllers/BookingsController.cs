using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Api.Infrastructure;
using HotelBooking.Api.Contracts;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly HotelContext _db;
        public BookingsController(HotelContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAll()
        {
            var rows = await _db.Bookings
                .AsNoTracking()
                .Select(b => new BookingDto(
                    b.BookingId, b.FirstName, b.LastName,
                    b.RoomId, b.RequestId, b.CheckIn, b.CheckOut,
                    b.IsRecurring, b.RecurrencePattern))
                .ToListAsync();

            return Ok(rows);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookingDto>> Get(int id)
        {
            var dto = await _db.Bookings
                .AsNoTracking()
                .Where(b => b.BookingId == id)
                .Select(b => new BookingDto(
                    b.BookingId, b.FirstName, b.LastName,
                    b.RoomId, b.RequestId, b.CheckIn, b.CheckOut,
                    b.IsRecurring, b.RecurrencePattern))
                .FirstOrDefaultAsync();

            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Create([FromBody] BookingDto dto)
        {
            if (dto.CheckIn.Date >= dto.CheckOut.Date)
                return BadRequest("Check-out must be after check-in.");

            var roomExists = await _db.Rooms.AnyAsync(r => r.RoomId == dto.RoomId);
            var reqExists = await _db.Requests.AnyAsync(r => r.RequestId == dto.RequestId);
            if (!roomExists || !reqExists)
                return BadRequest("Invalid Room or Request.");

            // overlap: (A.start < B.end) && (B.start < A.end)
            var overlap = await _db.Bookings.AnyAsync(b =>
                b.RoomId == dto.RoomId &&
                dto.CheckIn < b.CheckOut &&
                b.CheckIn < dto.CheckOut);

            if (overlap) return Conflict("Room not available for those dates.");

            var e = new Domain.Booking
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RoomId = dto.RoomId,
                RequestId = dto.RequestId,
                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut,
                IsRecurring = dto.IsRecurring,
                RecurrencePattern = dto.RecurrencePattern ?? "None"
            };

            _db.Bookings.Add(e);
            await _db.SaveChangesAsync();

            var created = new BookingDto(
                e.BookingId, e.FirstName, e.LastName,
                e.RoomId, e.RequestId, e.CheckIn, e.CheckOut,
                e.IsRecurring, e.RecurrencePattern);

            return CreatedAtAction(nameof(Get), new { id = e.BookingId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookingDto dto)
        {
            if (id != dto.BookingId) return BadRequest("Id mismatch.");

            var b = await _db.Bookings.FindAsync(id);
            if (b is null) return NotFound();

            if (dto.CheckIn.Date >= dto.CheckOut.Date)
                return BadRequest("Check-out must be after check-in.");

            // If dates/room changed, re-check overlap
            if (b.RoomId != dto.RoomId || b.CheckIn != dto.CheckIn || b.CheckOut != dto.CheckOut)
            {
                var overlap = await _db.Bookings.AnyAsync(x =>
                    x.BookingId != id &&
                    x.RoomId == dto.RoomId &&
                    dto.CheckIn < x.CheckOut &&
                    x.CheckIn < dto.CheckOut);

                if (overlap) return Conflict("Room not available for those dates.");
            }

            b.FirstName = dto.FirstName;
            b.LastName = dto.LastName;
            b.RoomId = dto.RoomId;
            b.RequestId = dto.RequestId;
            b.CheckIn = dto.CheckIn;
            b.CheckOut = dto.CheckOut;
            b.IsRecurring = dto.IsRecurring;
            b.RecurrencePattern = dto.RecurrencePattern ?? "None";

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var b = await _db.Bookings.FindAsync(id);
            if (b is null) return NotFound();

            _db.Bookings.Remove(b);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
