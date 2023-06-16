using ResearchesUFU.API.Repositories.Interfaces;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services;

public abstract class BaseService<TEntity> where TEntity : class

{
    private readonly IBaseRepository<TEntity> _repository;

    protected BaseService(IBaseRepository<TEntity> repository)
    {
        _repository = repository;
    }

    protected async Task<HttpResponseBase<T>> ExecuteMethodAsync<T>(Func<Task<HttpResponseBase<T>>> method)
    {
        try
        {
            return await method();
        }
        catch
        {
            return HttpUtils<T>.GenerateHttpErrorResponse();
        }
    }

    protected async Task<TEntity?> GetOneAsync(int id)
    {
        return await _repository.GetOneAsync(id);
    }

    protected async Task<IQueryable<TEntity>?> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    protected void Insert(TEntity requestEntity)
    {
        _repository.Insert(requestEntity);
    }

    protected void Update(TEntity newEntity, TEntity oldEntity)
    {
        _repository.Update(newEntity, oldEntity);
    }

    protected void Delete(TEntity requestEntity)
    {   
        _repository.Delete(requestEntity);
    }

    protected async Task SaveAsync()
    {
        await _repository.SaveAsync();
    }
}