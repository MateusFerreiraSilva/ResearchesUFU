using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services.Interfaces;

public interface IBaseService<T> where T : class
{
    public Task<HttpResponseBase<U>> ExecuteMethodAsync<U>(Func<Task<HttpResponseBase<U>>> method);
    
    public Task<T> FindOneAsync(DbSet<T> repository, int id);

    public Task<IQueryable<T>> FindAllAsync(DbSet<T> repository);
}