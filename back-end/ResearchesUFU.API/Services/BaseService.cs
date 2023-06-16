using ResearchesUFU.API.Repositories.Interfaces;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services;

public abstract class BaseService<TEntity> where TEntity : class

{
    private IBaseRepository<TEntity> _repository;

    public BaseService(IBaseRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public async Task<HttpResponseBase<T>> ExecuteMethodAsync<T>(Func<Task<HttpResponseBase<T>>> method)
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
    public async Task<TEntity?> GetOneAsync(int id)
    {
        return await _repository.GetOneAsync(id);
    }

    public async Task<IQueryable<TEntity>?> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public void Insert(TEntity requestEntity)
    {
        _repository.Insert(requestEntity);
    }
    
    public void Update(TEntity newEntity, TEntity oldEntity)
    {
        _repository.Update(newEntity, oldEntity);
    }

    public void Delete(TEntity requestEntity)
    {   
        _repository.Delete(requestEntity);
    }

    public async Task SaveAsync()
    {
        await _repository.SaveAsync();
    }
}