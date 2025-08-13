using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Api.Contracts;
using HotelBooking.Api.Mapping;
using HotelBooking.Api.Storage;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers;

[ApiController]
[Route("api/xml/bookings")]
public class XmlBookingsController : ControllerBase
{
    private readonly IStorage _store;
    public XmlBookingsController(IStorage store) => _store = store;

    [HttpGet]
    public ActionResult<IEnumerable<BookingDto>> GetAll()
        => Ok(_store.GetBookings().Select(Map.ToDto));

    [HttpGet("{id:int}")]
    public ActionResult<BookingDto> Get(int id)
    {
        var b = _store.GetBooking(id);
        return b is null ? NotFound() : Ok(Map.ToDto(b));
    }

    [HttpPost]
    public ActionResult<BookingDto> Create([FromBody] BookingDto dto)
    {
        // basic validation
        if (string.IsNullOrWhiteSpace(dto.FirstName)) return BadRequest("FirstName is required.");
        if (string.IsNullOrWhiteSpace(dto.LastName)) return BadRequest("LastName is required.");
        if (dto.CheckIn.Date >= dto.CheckOut.Date) return BadRequest("Check-out must be after check-in.");

        // existence checks
        var roomExists = _store.GetRooms().Any(r => r.RoomId == dto.RoomId);
        var reqExists = _store.GetRequests().Any(r => r.RequestId == dto.RequestId);
        if (!roomExists || !reqExists) return BadRequest("Invalid Room or Request.");

        // overlap: (A.start < B.end) && (B.start < A.end)
        var overlap = _store.GetBookings().Any(b =>
            b.RoomId == dto.RoomId &&
            dto.CheckIn < b.CheckOut &&
            b.CheckIn < dto.CheckOut);

        if (overlap) return Conflict("Room not available for those dates.");

        var entity = Map.ToEntity(dto);
        entity.BookingId = 0; // let storage assign
        entity.RecurrencePattern ??= "None";

        var created = _store.AddBooking(entity);
        var result = Map.ToDto(created);
        return CreatedAtAction(nameof(Get), new { id = result.BookingId }, result);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] BookingDto dto)
    {
        if (id != dto.BookingId) return BadRequest("Id mismatch.");
        if (dto.CheckIn.Date >= dto.CheckOut.Date) return BadRequest("Check-out must be after check-in.");

        var existing = _store.GetBooking(id);
        if (existing is null) return NotFound();

        // existence checks (always verify)
        var roomExists = _store.GetRooms().Any(r => r.RoomId == dto.RoomId);
        var reqExists = _store.GetRequests().Any(r => r.RequestId == dto.RequestId);
        if (!roomExists || !reqExists) return BadRequest("Invalid Room or Request.");

        // if dates/room changed, re-check overlap
        var datesChanged = existing.CheckIn != dto.CheckIn || existing.CheckOut != dto.CheckOut;
        var roomChanged = existing.RoomId != dto.RoomId;
        if (datesChanged || roomChanged)
        {
            var overlap = _store.GetBookings().Any(b =>
                b.BookingId != id &&
                b.RoomId == dto.RoomId &&
                dto.CheckIn < b.CheckOut &&
                b.CheckIn < dto.CheckOut);

            if (overlap) return Conflict("Room not available for those dates.");
        }

        var updated = _store.UpdateBooking(Map.ToEntity(dto));
        return updated is null ? NotFound() : NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
        => _store.DeleteBooking(id) ? NoContent() : NotFound();
}
