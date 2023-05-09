using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Repositories.Interfaces;

namespace ResearchesUFU.API.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private ResearchesUFUContext _dbContext;
    private DbSet<TEntity> _dbSet;

    public BaseRepository(ResearchesUFUContext dbContext, DbSet<TEntity> dbSet)
    {
        _dbContext = dbContext;
        _dbSet = dbSet;
    }
    
    public async Task<TEntity> GetOneAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IQueryable<TEntity>> GetAllAsync()
    {
        var queryResponse = await _dbSet.ToListAsync();

        return queryResponse.AsQueryable();
    }

    public void Insert(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}