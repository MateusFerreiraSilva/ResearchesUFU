namespace ResearchesUFU.API.Repositories.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    public Task<TEntity> GetOneAsync(int id);
    
    public Task<IQueryable<TEntity>> GetAllAsync();

    public void Insert(TEntity entity);
    
    public void Update(TEntity entity);
    
    public void Delete(TEntity entity);

    public Task SaveAsync();
}