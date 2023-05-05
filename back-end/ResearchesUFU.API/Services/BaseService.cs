using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Services.Interfaces;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services;

public class BaseService<T> : IBaseService<T> where T : class
{
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
    public async Task<T> FindOneAsync(DbSet<T> repository, int id)
    {
        try
        {
            return await repository.FindAsync(id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<IQueryable<T>> FindAllAsync(DbSet<T> repository)
    {
        try
        {
            var researchesList = await repository.ToListAsync();

            return researchesList.AsQueryable();
        }
        catch
        {
            return null;
        }
    }
}