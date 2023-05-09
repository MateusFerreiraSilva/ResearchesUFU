using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Repositories.Interfaces;

namespace ResearchesUFU.API.Repositories;

public class ResearchRepository : IBaseRepository<Research>
{

    private readonly ResearchesUFUContext _dbContext;
    private readonly DbSet<Research> _repository;

    public ResearchRepository(ResearchesUFUContext dbContext)
    {
        _dbContext = dbContext;
        _repository = _dbContext.Researches;
    }

    public async Task<Research> GetOneAsync(int id)
    {
        var queryResponse = await _repository
            .Include(r => r.ResearchField)
                .ThenInclude(rf => rf.Field)
            .Include(r => r.ResearchTag)
                .ThenInclude(rt => rt.Tag)
            .Include(r => r.ResearchAuthor)
                .ThenInclude(ra => ra.Author)
            .FirstOrDefaultAsync(r => r.Id == id);

        return queryResponse;
    }

    public async Task<IQueryable<Research>> GetAllAsync()
    {
        var queryResponse = await _repository.ToListAsync();

        var queryResult = await _repository
            .Include(r => r.ResearchField)
                .ThenInclude(rf => rf.Field)
            .Include(r => r.ResearchTag)
                .ThenInclude(rt => rt.Tag)
            .Include(r => r.ResearchAuthor)
                .ThenInclude(ra => ra.Author)
            .ToListAsync();

        return queryResponse.AsQueryable();
    }

    public void Insert(Research entity)
    {
        _repository.Add(entity);
    }

    public void Update(Research entity)
    {
        _repository.Update(entity);
    }

    public void Delete(Research entity)
    {
        _repository.Remove(entity);
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}