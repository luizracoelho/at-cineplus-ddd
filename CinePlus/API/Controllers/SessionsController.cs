using CinePlus.Domain.Contracts.APP;
using CinePlus.Domain.ViewModels.Sessions;
using CinePlus.Domain.ViewModels.SessionSeats;
using Microsoft.AspNetCore.Mvc;

namespace CinePlus.API.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class SessionsController : ControllerBase
{
    private readonly ISessionApp _app;

    public SessionsController(ISessionApp app) => _app = app;

    [HttpGet]
    public async Task<IActionResult> ListAsync()
        => Ok(await _app.ListAsync());

    [HttpGet("movie/{movieId:long}/room/{roomId:long}")]
    public async Task<IActionResult> ListByMovieAndRoomAsync(long movieId, long roomId)
        => Ok(await _app.ListByMovieAndRoomAsync(movieId, roomId));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> FindAsync(long id)
        => Ok(await _app.FindAsync(id));

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateSessionVm vm)
        => Ok(await _app.AddAsync(vm));

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateAsync(long id, [FromBody] CreateSessionVm vm)
        => Ok(await _app.UpdateAsync(id, vm));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> RemoveAsync(long id)
    {
        var isSuccess = await _app.RemoveAsync(id);
        return isSuccess ? Ok() : BadRequest();
    }

    [HttpPost("{sessionId:long}/seats")]
    public async Task<IActionResult> AddSeatAsync(long sessionId, [FromBody] CreateSessionSeatVm vm)
        => Ok(await _app.AddSeatAsync(sessionId, vm));

    [HttpPut("{sessionId:long}/seats/{id:long}")]
    public async Task<IActionResult> UpdateSeatAsync(long sessionId, long id, [FromBody] CreateSessionSeatVm vm)
        => Ok(await _app.UpdateSeatAsync(sessionId, id, vm));

    [HttpDelete("{sessionId:long}/seats/{id:long}")]
    public async Task<IActionResult> RemoveSeatAsync(long sessionId, long id)
        => Ok(await _app.RemoveSeatAsync(sessionId, id));

    [HttpPatch("{sessionId:long}/seats/{id:long}/reserve")]
    public async Task<IActionResult> ReserveSeatAsync(long sessionId, long id, [FromBody] ReserveSessionSeatVm vm)
        => Ok(await _app.ReserveSeatAsync(sessionId, id, vm));

    [HttpPatch("{sessionId:long}/seats/{id:long}/cancel-reserve")]
    public async Task<IActionResult> CancelReserveSeatAsync(long sessionId, long id)
        => Ok(await _app.CancelReserveSeatAsync(sessionId, id));

    [HttpPatch("{sessionId:long}/seats/{id:long}/confirm-reserve")]
    public async Task<IActionResult> ConfirmReserveSeatAsync(long sessionId, long id)
        => Ok(await _app.ConfirmReserveSeatAsync(sessionId, id));

    [HttpPatch("{sessionId:long}/seats/{id:long}/cancel-confirmation")]
    public async Task<IActionResult> CancelConfirmationSeatAsync(long sessionId, long id)
        => Ok(await _app.CancelConfirmationSeatAsync(sessionId, id));
}