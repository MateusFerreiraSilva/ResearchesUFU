using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services.Interfaces;

public interface IBaseService<T> where T : class
{
    public Task<HttpResponseBase<U>> ExecuteMethodAsync<U>(Func<Task<HttpResponseBase<U>>> method);
    
    public Task<T> FindOneAsync(int id);
    
    public Task<List<T>> FindManyAsync(IEnumerable<int> ids);

    public Task<IQueryable<T>> FindAllAsync();
}