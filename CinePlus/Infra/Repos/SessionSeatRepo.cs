using CinePlus.Domain.Models;
using CinePlus.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CinePlus.Infra.Repos;

public class SessionSeatRepo : BaseRepo<SessionSeat>
{
    public SessionSeatRepo(DataContext context) : base(context)
    {
    }

    public override async Task<IList<SessionSeat>> ListAsync()
        => await DbSet.OrderBy(seat => seat.Session).ToListAsync();
}