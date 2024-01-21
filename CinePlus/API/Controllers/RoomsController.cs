using CinePlus.APP;
using CinePlus.Domain.ViewModels.Rooms;
using Microsoft.AspNetCore.Mvc;

namespace CinePlus.API.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class RoomsController : ControllerBase
{
    private readonly RoomApp _app;

    public RoomsController(RoomApp app) => _app = app;

    [HttpGet]
    public async Task<IActionResult> ListAsync()
        => Ok(await _app.ListAsync());

    [HttpGet("{id:long}")]
    public async Task<IActionResult> FindAsync(long id)
        => Ok(await _app.FindAsync(id));

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateRoomVm vm)
        => Ok(await _app.AddAsync(vm));

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateAsync(long id, [FromBody] CreateRoomVm vm)
        => Ok(await _app.UpdateAsync(id, vm));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> RemoveAsync(long id)
    {
        var isSuccess = await _app.RemoveAsync(id);
        return isSuccess ? Ok() : BadRequest();
    }

    [HttpPatch("{id:long}/activate")]
    public async Task<IActionResult> ActivateAsync(long id)
    {
        var isSuccess = await _app.ActivateAsync(id);
        return isSuccess ? Ok() : BadRequest();
    }

    [HttpPatch("{id:long}/deactivate")]
    public async Task<IActionResult> DeactivateAsync(long id)
    {
        var isSuccess = await _app.DeactivateAsync(id);
        return isSuccess ? Ok() : BadRequest();
    }
}