using CinePlus.Infra.Repos;
using FluentValidation;

namespace CinePlus.Domain.Services;

public abstract class BaseService<T> where T : class
{
    private readonly BaseRepo<T> _repo;

    protected BaseService(BaseRepo<T> repo) => _repo = repo;

    public virtual async Task<IList<T>> ListAsync()
        => await _repo.ListAsync();
    
    public virtual async Task<T> FindAsync(long id)
    {
        if (id <= 0) throw new Exception("Não encontrado.");
        
        var entity = await _repo.FindAsync(id);
        if (entity == null) throw new Exception("Não encontrado.");

        return entity;
    }
    
    public virtual async Task<bool> RemoveAsync(long id)
    {
        var entity = await FindAsync(id);
        return await _repo.RemoveAsync(entity);
    }
}