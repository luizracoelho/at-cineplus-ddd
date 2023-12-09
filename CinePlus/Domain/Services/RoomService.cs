using CinePlus.Domain.Models;
using CinePlus.Domain.Validators;
using CinePlus.Infra.Repos;
using FluentValidation;

namespace CinePlus.Domain.Services;

public class RoomService : BaseService<Room>
{
    private readonly RoomRepo _repo;
    private readonly RoomValidator _validator;
    
    public RoomService(RoomRepo repo, RoomValidator validator) : base(repo)
    {
        _repo = repo;
        _validator = validator;
    }
    
    public async Task<Room> AddAsync(Room room)
    {
        await _validator.ValidateAndThrowAsync(room);
        return await _repo.AddAsync(room);
    }

    public async Task<Room> UpdateAsync(Room room)
    {
        var roomDb = await FindAsync(room.Id);

        roomDb.Update(room.Name, room.RowsCount, room.SeatsByRow);

        await _validator.ValidateAndThrowAsync(roomDb);

        return await _repo.UpdateAsync(roomDb);
    }
    
    public async Task<bool> ActivateAsync(long id)
    {
        var roomDb = await FindAsync(id);
        var result = roomDb.Activate();

        if (!result) throw new Exception("Não foi possível ativar a sala, pois ela já se encontra ativa.");

        await _repo.UpdateAsync(roomDb);

        return true;
    }

    public async Task<bool> DeactivateAsync(long id)
    {
        var roomDb = await FindAsync(id);
        var result = roomDb.Deactivate();

        if (!result) throw new Exception("Não foi possível desativar a sala, pois ela já se encontra inativa.");

        await _repo.UpdateAsync(roomDb);

        return true;
    }
}