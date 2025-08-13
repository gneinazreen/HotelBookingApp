using HotelBooking.Api.Contracts;
using HotelBooking.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RequestsController : ControllerBase
{
    private readonly HotelContext _db;
    public RequestsController(HotelContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SpecialRequestDto>>> GetAll()
        => Ok(await _db.Requests.AsNoTracking()
            .Select(r => new SpecialRequestDto(r.RequestId, r.Description, r.Category))
            .ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SpecialRequestDto>> Get(int id)
    {
        var row = await _db.Requests.AsNoTracking()
            .Where(r => r.RequestId == id)
            .Select(r => new SpecialRequestDto(r.RequestId, r.Description, r.Category))
            .FirstOrDefaultAsync();
        return row is null ? NotFound() : Ok(row);
    }

    [HttpPost]
    public async Task<ActionResult<SpecialRequestDto>> Create([FromBody] SpecialRequestDto dto)
    {
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
        var e = await _db.Requests.FindAsync(id);
        if (e is null) return NotFound();

        e.Description = dto.Description;
        e.Category = dto.Category;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _db.Requests.FindAsync(id);
        if (e is null) return NotFound();
        _db.Requests.Remove(e);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
