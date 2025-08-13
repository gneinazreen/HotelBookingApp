using HotelBooking.Api.Contracts;
using HotelBooking.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly HotelContext _db;
    public RoomsController(HotelContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomDto>>> GetAll()
        => Ok(await _db.Rooms.AsNoTracking()
            .Select(r => new RoomDto(r.RoomId, r.RoomType, r.BasePrice))
            .ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RoomDto>> Get(int id)
    {
        var row = await _db.Rooms.AsNoTracking()
            .Where(r => r.RoomId == id)
            .Select(r => new RoomDto(r.RoomId, r.RoomType, r.BasePrice))
            .FirstOrDefaultAsync();
        return row is null ? NotFound() : Ok(row);
    }

    [HttpPost]
    public async Task<ActionResult<RoomDto>> Create([FromBody] RoomDto dto)
    {
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
        var e = await _db.Rooms.FindAsync(id);
        if (e is null) return NotFound();

        e.RoomType = dto.RoomType;
        e.BasePrice = dto.BasePrice;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.Rooms.FindAsync(id);
        if (e is null) return NotFound();
        _db.Rooms.Remove(e);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
