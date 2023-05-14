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

    public async Task SaveAsync()
    {
        await _repository.SaveAsync();
    }

    // public async Task<List<U>> GetSubEntitiesAsync<T, U>(
    //     int id, IBaseRepository<T> manyToManyRepository, IBaseRepository<U> subEntitiesRepository
    //     )
    //     where T : class 
    //     where U : class
    // {
    //     var manyToManyEntity= (await manyToManyRepository.GetAllAsync()).Where();
    //     var subEntity = await subEntitiesRepository.GetOneAsync(manyToManyEntity.GetCustomAttribute);
    //
    //     return tags.ToList();
    // }
    //
    // public async Task<List<T>> GetSubEntityAsync<T, R>(int id, IBaseRepository<R> manyToManyRepository)
    // {
    //     var researchTag= await _researchTagRepository.ToListAsync();
    //     var tagsIds = researchTag.Where(rt => rt.ResearchId.Equals(id)).Select(rt => rt.TagId);
    //     var tags = tagsIds.Select(id => _tagService.FindOneAsync(id).Result);
    //     
    //     return tags.ToList();
    // }
}