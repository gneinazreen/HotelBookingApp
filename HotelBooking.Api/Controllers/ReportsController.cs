using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Api.Infrastructure;
using HotelBooking.Api.Contracts;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly HotelContext _db;
        public ReportsController(HotelContext db) => _db = db;

        [HttpGet("weekly")]
        public async Task<ActionResult<IEnumerable<WeeklyReportRow>>> Weekly([FromQuery] DateTime weekStart)
        {
            var start = weekStart.Date;
            var end = start.AddDays(7);

            var bookings = await _db.Bookings
                .AsNoTracking()
                .Where(b => b.CheckIn <= end && b.CheckOut > start)
                .ToListAsync();

            var rooms = await _db.Rooms
                .AsNoTracking()
                .ToDictionaryAsync(r => r.RoomId, r => r.RoomType);

            var reqs = await _db.Requests
                .AsNoTracking()
                .ToDictionaryAsync(r => r.RequestId, r => r.Description);

            var rows = new List<WeeklyReportRow>();
            for (int i = 0; i < 7; i++)
            {
                var day = start.AddDays(i);
                var onDay = bookings.Where(b => b.CheckIn <= day && b.CheckOut > day);

                if (!onDay.Any())
                {
                    rows.Add(new WeeklyReportRow(day, "No bookings", "-", "-"));
                }
                else
                {
                    foreach (var b in onDay)
                    {
                        rows.Add(new WeeklyReportRow(
                            day,
                            $"{b.FirstName} {b.LastName}",
                            rooms.TryGetValue(b.RoomId, out var rt) ? rt : "Unknown",
                            reqs.TryGetValue(b.RequestId, out var rq) ? rq : "-"
                        ));
                    }
                }
            }

            return Ok(rows);
        }
    }
}
