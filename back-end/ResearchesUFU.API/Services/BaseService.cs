using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Services.Interfaces;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services;

public class BaseService: IBaseService
{
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
    public async Task<T> FindOneAsync<T>(DbSet<T> repository, int id) where T : class
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

    public async Task<IQueryable<T>> FindAllAsync<T>(DbSet<T> repository) where T : class
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