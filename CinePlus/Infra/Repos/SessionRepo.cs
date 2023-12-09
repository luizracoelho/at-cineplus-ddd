using CinePlus.Domain.Models;
using CinePlus.Infra.Context;

namespace CinePlus.Infra.Repos;

public class SessionRepo : BaseRepo<Session>
{
    public SessionRepo(DataContext context) : base(context)
    {
    }
}