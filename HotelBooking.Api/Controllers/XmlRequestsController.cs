using HotelBooking.Api.Contracts;
using HotelBooking.Api.Mapping;
using HotelBooking.Api.Storage;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers;

[ApiController]
[Route("api/xml/requests")]
public class XmlRequestsController : ControllerBase
{
    private readonly IStorage _store;
    public XmlRequestsController(IStorage store) => _store = store;

    [HttpGet]
    public ActionResult<IEnumerable<SpecialRequestDto>> GetAll()
        => Ok(_store.GetRequests().Select(Map.ToDto));

    [HttpGet("{id:int}")]
    public ActionResult<SpecialRequestDto> Get(int id)
    {
        var r = _store.GetRequest(id);
        return r is null ? NotFound() : Ok(Map.ToDto(r));
    }

    [HttpPost]
    public ActionResult<SpecialRequestDto> Create([FromBody] SpecialRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Description))
            return BadRequest("Description is required.");
        if (string.IsNullOrWhiteSpace(dto.Category))
            return BadRequest("Category is required.");

        var entity = Map.ToEntity(dto);
        entity.RequestId = 0; // let storage assign

        var created = _store.AddRequest(entity);
        var result = Map.ToDto(created);
        return CreatedAtAction(nameof(Get), new { id = result.RequestId }, result);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] SpecialRequestDto dto)
    {
        if (id != dto.RequestId) return BadRequest("Id mismatch.");
        if (string.IsNullOrWhiteSpace(dto.Description))
            return BadRequest("Description is required.");
        if (string.IsNullOrWhiteSpace(dto.Category))
            return BadRequest("Category is required.");

        var existing = _store.GetRequest(id);
        if (existing is null) return NotFound();

        var updated = _store.UpdateRequest(Map.ToEntity(dto));
        return updated is null ? NotFound() : NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
        => _store.DeleteRequest(id) ? NoContent() : NotFound();
}
