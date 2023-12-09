using CinePlus.Domain.Models;
using CinePlus.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CinePlus.Infra.Repos;

public class RoomRepo : BaseRepo<Room>
{
    public RoomRepo(DataContext context) : base(context)
    {
    }

    public override async Task<IList<Room>> ListAsync()
        => await DbSet.OrderBy(movie => movie.Name).ToListAsync();

    public async Task<IList<Room>> ListActivesAsync()
        => await DbSet.Where(movie => movie.Active)
            .OrderBy(movie => movie.Name)
            .ToListAsync();
}