using HotelBooking.Api.Contracts;
using HotelBooking.Api.Storage;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers;

[ApiController]
[Route("api/xml/reports")]
public class XmlReportsController : ControllerBase
{
    private readonly IStorage _store;
    public XmlReportsController(IStorage store) => _store = store;

    // GET api/xml/reports/daily?start=2025-08-01&end=2025-08-31
    [HttpGet("daily")]
    public ActionResult<IEnumerable<WeeklyReportRow>> Daily([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        if (end.Date <= start.Date) return BadRequest("end must be after start");

        var bookings = _store.GetBookings();
        var rooms = _store.GetRooms().ToDictionary(r => r.RoomId);
        var reqs = _store.GetRequests().ToDictionary(r => r.RequestId);

        var rows =
            bookings
                .Where(b => b.CheckIn.Date >= start.Date && b.CheckIn.Date < end.Date)
                .OrderBy(b => b.CheckIn)
                .Select(b =>
                {
                    var roomType = rooms.TryGetValue(b.RoomId, out var r) ? r.RoomType : "Unknown";
                    var reqDesc = reqs.TryGetValue(b.RequestId, out var s) ? s.Description : "-";
                    return new WeeklyReportRow(b.CheckIn.Date, $"{b.FirstName} {b.LastName}", roomType, reqDesc);
                })
                .ToList();

        return Ok(rows);
    }

    // GET api/xml/reports/weekly?weekStart=2025-08-11
    [HttpGet("weekly")]
    public ActionResult<IEnumerable<WeeklyReportRow>> Weekly([FromQuery] DateTime weekStart)
    {
        var start = weekStart.Date;
        var end = start.AddDays(7);

        var bookings = _store.GetBookings();
        var rooms = _store.GetRooms().ToDictionary(r => r.RoomId);
        var reqs = _store.GetRequests().ToDictionary(r => r.RequestId);

        var rows =
            bookings
                .Where(b => b.CheckIn.Date >= start && b.CheckIn.Date < end)
                .OrderBy(b => b.CheckIn)
                .Select(b =>
                {
                    var roomType = rooms.TryGetValue(b.RoomId, out var r) ? r.RoomType : "Unknown";
                    var reqDesc = reqs.TryGetValue(b.RequestId, out var s) ? s.Description : "-";
                    return new WeeklyReportRow(b.CheckIn.Date, $"{b.FirstName} {b.LastName}", roomType, reqDesc);
                })
                .ToList();

        return Ok(rows);
    }
}
