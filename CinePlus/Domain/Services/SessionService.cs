using CinePlus.Domain.Models;
using CinePlus.Domain.Validators;
using CinePlus.Infra.Repos;
using FluentValidation;

namespace CinePlus.Domain.Services;

public class SessionService : BaseService<Session>
{
    private readonly SessionRepo _repo;
    private readonly SessionValidator _validator;
    
    public SessionService(SessionRepo repo, SessionValidator validator) : base(repo)
    {
        _repo = repo;
        _validator = validator;
    }
    
    public async Task<Session> AddAsync(Session session)
    {
        await _validator.ValidateAndThrowAsync(session);
        return await _repo.AddAsync(session);
    }

    public async Task<Session> UpdateAsync(Session session)
    {
        var sessionDb = await FindAsync(session.Id);

        sessionDb.Update(session.DateTime,session.MovieId, session.RoomId, session.Price);

        await _validator.ValidateAndThrowAsync(sessionDb);

        return await _repo.UpdateAsync(sessionDb);
    }
}