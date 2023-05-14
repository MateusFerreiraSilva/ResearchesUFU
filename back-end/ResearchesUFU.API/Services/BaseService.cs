using ResearchesUFU.API.Repositories.Interfaces;
using ResearchesUFU.API.Utils;
using System.Reflection;

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
    public async Task<TEntity> GetOneAsync(int id)
    {
        try
        {
            return await _repository.GetOneAsync(id);
        }
        catch
        {
            return null;
        }
    }
    
    public async Task<List<TEntity>> GetManyAsync(IEnumerable<int> ids)
    {
        try
        {
            var queries = ids.Select(id => GetOneAsync(id));
            var queryResults = (await Task.WhenAll(queries)).ToList();
    
            return queryResults;
        }
        catch
        {
            return null;
        }
    }
    
    public async Task<IQueryable<TEntity>> GetAllAsync()
    {
        try
        {
            return await _repository.GetAllAsync();
        }
        catch
        {
            return null;
        }
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