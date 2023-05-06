using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services.Interfaces;

public interface IBaseService
{
    public Task<HttpResponseBase<U>> ExecuteMethodAsync<U>(Func<Task<HttpResponseBase<U>>> method);
    
    public Task<T> FindOneAsync<T>(DbSet<T> repository, int id) where T : class;

    public Task<IQueryable<T>> FindAllAsync<T>(DbSet<T> repository) where T : class;
}