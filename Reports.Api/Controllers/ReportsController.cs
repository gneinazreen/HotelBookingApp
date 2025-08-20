using HotelBooking.Api.Contracts;
using HotelBooking.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly HotelContext _db;
    public ReportsController(HotelContext db) => _db = db;

    // GET api/reports/daily?start=2025-08-01&end=2025-08-31
    [HttpGet("daily")]
    public async Task<ActionResult<IEnumerable<WeeklyReportRow>>> Daily([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        if (end.Date <= start.Date) return BadRequest("end must be after start");

        var q =
            from b in _db.Bookings.AsNoTracking()
            join r in _db.Rooms.AsNoTracking() on b.RoomId equals r.RoomId
            join s in _db.Requests.AsNoTracking() on b.RequestId equals s.RequestId
            where b.CheckIn.Date >= start.Date && b.CheckIn.Date < end.Date
            orderby b.CheckIn
            select new WeeklyReportRow(
                b.CheckIn.Date,
                b.FirstName + " " + b.LastName,
                r.RoomType,
                s.Description
            );

        var rows = await q.ToListAsync();
        return Ok(rows);
    }

    // GET api/reports/weekly?weekStart=2025-08-11
    [HttpGet("weekly")]
    public async Task<ActionResult<IEnumerable<WeeklyReportRow>>> Weekly([FromQuery] DateTime weekStart)
    {
        var start = weekStart.Date;
        var end = start.AddDays(7);

        var q =
            from b in _db.Bookings.AsNoTracking()
            join r in _db.Rooms.AsNoTracking() on b.RoomId equals r.RoomId
            join s in _db.Requests.AsNoTracking() on b.RequestId equals s.RequestId
            where b.CheckIn.Date >= start && b.CheckIn.Date < end
            orderby b.CheckIn
            select new WeeklyReportRow(
                b.CheckIn.Date,
                b.FirstName + " " + b.LastName,
                r.RoomType,
                s.Description
            );

        var rows = await q.ToListAsync();
        return Ok(rows);
    }
}
