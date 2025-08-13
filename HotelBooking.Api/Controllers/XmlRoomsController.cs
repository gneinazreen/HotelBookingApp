using HotelBooking.Api.Contracts;
using HotelBooking.Api.Mapping;
using HotelBooking.Api.Storage;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers;

[ApiController]
[Route("api/xml/rooms")]
public class XmlRoomsController : ControllerBase
{
    private readonly IStorage _store;
    public XmlRoomsController(IStorage store) => _store = store;

    [HttpGet]
    public ActionResult<IEnumerable<RoomDto>> GetAll()
        => Ok(_store.GetRooms().Select(Map.ToDto));

    [HttpGet("{id:int}")]
    public ActionResult<RoomDto> Get(int id)
    {
        var r = _store.GetRoom(id);
        return r is null ? NotFound() : Ok(Map.ToDto(r));
    }

    [HttpPost]
    public ActionResult<RoomDto> Create([FromBody] RoomDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.RoomType))
            return BadRequest("RoomType is required.");
        if (dto.BasePrice <= 0)
            return BadRequest("BasePrice must be positive.");

        // Create a new entity and let storage assign the ID
        var entity = Map.ToEntity(dto);
        entity.RoomId = 0;

        var created = _store.AddRoom(entity);
        var result = Map.ToDto(created);
        return CreatedAtAction(nameof(Get), new { id = result.RoomId }, result);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] RoomDto dto)
    {
        if (id != dto.RoomId) return BadRequest("Id mismatch.");
        if (string.IsNullOrWhiteSpace(dto.RoomType))
            return BadRequest("RoomType is required.");
        if (dto.BasePrice <= 0)
            return BadRequest("BasePrice must be positive.");

        var existing = _store.GetRoom(id);
        if (existing is null) return NotFound();

        var updated = _store.UpdateRoom(Map.ToEntity(dto));
        return updated is null ? NotFound() : NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
        => _store.DeleteRoom(id) ? NoContent() : NotFound();
}
