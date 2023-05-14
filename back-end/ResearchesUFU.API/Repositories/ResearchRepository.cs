using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Repositories.Interfaces;

namespace ResearchesUFU.API.Repositories;

public class ResearchRepository : IBaseRepository<Research>
{

    private readonly ResearchesUFUContext _dbContext;
    private readonly DbSet<Research> _researchRepository;
    private readonly DbSet<Field> _fieldRepository;
    private readonly DbSet<Tag> _tagRepository;
    private readonly DbSet<Author> _authorRepository;

    
    public ResearchRepository(ResearchesUFUContext dbContext)
    {
        _dbContext = dbContext;
        _researchRepository = _dbContext.Researches;
        _fieldRepository = _dbContext.Fields;
        _tagRepository = _dbContext.Tags;
        _authorRepository = _dbContext.Authors;
    }

    public async Task<Research> GetOneAsync(int id)
    {
        var queryResponse = await _researchRepository
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
        var queryResponse = await _researchRepository.ToListAsync();

        var queryResult = await _researchRepository
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
        _researchRepository.Add(AddRelationshipsAsync(entity));
    }

    public void Update(Research entity)
    {
        _researchRepository.Update(entity);
    }

    public void Delete(Research entity)
    {
        _researchRepository.Remove(entity);
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
    
    private Research AddRelationshipsAsync(Research research)
    {
        research.ResearchField.ForEach(rf =>
        {
            var field = _fieldRepository.Find(rf.FieldId);
            
            rf.Field = field;
            rf.Research = research;
        });
            
        research.ResearchTag.ForEach(rt =>
        {
            var tag = _tagRepository.Find(rt.TagId);
            
            rt.Tag = tag;
            rt.Research = research;
        });
            
        research.ResearchAuthor.ForEach(ra =>
        {
            var author = _authorRepository.Find(ra.AuthorId);
            
            ra.Author = author;
            ra.Research = research;
        });

        return research;
    }
}