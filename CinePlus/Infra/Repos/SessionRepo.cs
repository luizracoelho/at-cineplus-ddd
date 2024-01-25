using CinePlus.Domain.Contracts.Context;
using CinePlus.Domain.Contracts.Repos;
using CinePlus.Domain.Models;
using CinePlus.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CinePlus.Infra.Repos;

public class SessionRepo : BaseRepo<Session>, ISessionRepo
{
    public SessionRepo(IDataContext context) : base(context)
    {
    }

    public async Task<IList<Session>> ListByMovieAndRoomAsync(long movieId, long roomId)
    {
        return await DbSet
            .Where(session =>
                session.MovieId == movieId &&
                session.RoomId == roomId
            )
            .Include(session => session.Seats)
            .ToListAsync();
    }

    public override Task<Session?> FindAsync(long id)
    {
        return DbSet
            .Include(session => session.Seats)
            .FirstOrDefaultAsync(session => session.Id == id);
    }
}