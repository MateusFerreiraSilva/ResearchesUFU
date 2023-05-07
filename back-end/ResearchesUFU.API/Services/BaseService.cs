using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Services.Interfaces;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services;

public abstract class BaseService<T> : IBaseService<T> where T : class
{
    protected ResearchesUFUContext DbContext;
    protected DbSet<T>? Repository;
    
    public async Task<HttpResponseBase<U>> ExecuteMethodAsync<U>(Func<Task<HttpResponseBase<U>>> method)
    {
        try
        {
            return await method();
        }
        catch
        {
            return HttpUtils<U>.GenerateHttpErrorResponse();
        }
    }
    public async Task<T> FindOneAsync(int id)
    {
        try
        {
            return await Repository.FindAsync(id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<T>> FindManyAsync(IEnumerable<int> ids)
    {
        try
        {
            var queries = ids.Select(id => FindOneAsync(id));
            var queryResults = (await Task.WhenAll(queries)).ToList();

            return queryResults;
        }
        catch
        {
            return null;
        }
    }
    
    public async Task<IQueryable<T>> FindAllAsync()
    {
        try
        {
            var queryResponse = await Repository.ToListAsync();

            return queryResponse.AsQueryable();
        }
        catch
        {
            return null;
        }
    }
}