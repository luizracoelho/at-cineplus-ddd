using AutoMapper;
using CinePlus.Domain.Models;
using CinePlus.Domain.Services;
using CinePlus.Domain.ViewModels.Sessions;
using CinePlus.Domain.ViewModels.SessionSeats;

namespace CinePlus.APP;

public class SessionApp
{
    private readonly SessionService _service;
    private readonly SessionSeatService _seatService;
    private readonly IMapper _mapper;

    public SessionApp(
        SessionService service,
        SessionSeatService seatService,
        IMapper mapper
    )
    {
        _service = service;
        _seatService = seatService;
        _mapper = mapper;
    }

    public async Task<IList<SessionVm>> ListAsync()
    {
        var sessions = await _service.ListAsync();
        return _mapper.Map<IList<SessionVm>>(sessions);
    }

    public async Task<IList<SessionVm>> ListByMovieAndRoomAsync(long movieId, long roomId)
    {
        var sessions = await _service.ListByMovieAndRoomAsync(movieId, roomId);
        return _mapper.Map<IList<SessionVm>>(sessions);
    }

    public async Task<SessionVm> FindAsync(long id)
    {
        var session = await _service.FindAsync(id);
        return _mapper.Map<SessionVm>(session);
    }

    public async Task<SessionVm> AddAsync(CreateSessionVm vm)
    {
        var session = new Session(vm.DateTime, vm.MovieId, vm.RoomId, vm.Price);
        await _service.AddAsync(session);

        return _mapper.Map<SessionVm>(session);
    }

    public async Task<SessionVm> UpdateAsync(long id, CreateSessionVm vm)
    {
        var session = await _service.FindAsync(id);

        session.Update(vm.DateTime, vm.MovieId, vm.RoomId, vm.Price);
        await _service.UpdateAsync(session);

        return _mapper.Map<SessionVm>(session);
    }

    public async Task<bool> RemoveAsync(long id)
        => await _service.RemoveAsync(id);

    public async Task<SessionSeatVm> AddSeatAsync(CreateSessionSeatVm vm)
    {
        var seat = new SessionSeat(vm.Seat, vm.SessionId);
        await _seatService.AddAsync(seat);

        return _mapper.Map<SessionSeatVm>(seat);
    }

    public async Task<SessionSeatVm> UpdateSeatAsync(long id, UpdateSessionSeatVm vm)
    {
        var seat = await _seatService.FindAsync(id);
        seat.Update(vm.Seat);

        await _seatService.UpdateAsync(seat);

        return _mapper.Map<SessionSeatVm>(seat);
    }

    public async Task<bool> RemoveSeatAsync(long id)
        => await _seatService.RemoveAsync(id);

    public async Task<bool> ReserveSeatAsync(long id, ReserveSessionSeatVm vm)
        => await _seatService.ReserveAsync(id, vm.Document);
    
    public async Task<bool> CancelReserveSeatAsync(long id)
        => await _seatService.CancelReserveAsync(id);
    
    public async Task<bool> ConfirmReserveSeatAsync(long id)
        => await _seatService.ConfirmAsync(id);
    
    public async Task<bool> CancelConfirmationSeatAsync(long id)
        => await _seatService.CancelConfirmationAsync(id);
}