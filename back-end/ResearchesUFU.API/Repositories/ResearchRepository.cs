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
    private readonly DbSet<ResearchField> _researchFieldRepository;
    private readonly DbSet<ResearchTag> _researchTagRepository;
    private readonly DbSet<ResearchAuthor> _researchAuthorRepository;

    public ResearchRepository(ResearchesUFUContext dbContext)
    {
        _dbContext = dbContext;
        _researchRepository = _dbContext.Researches;
        _fieldRepository = _dbContext.Fields;
        _tagRepository = _dbContext.Tags;
        _authorRepository = _dbContext.Authors;
        _researchFieldRepository = _dbContext.ResearchField;
        _researchTagRepository = _dbContext.ResearchTag;
        _researchAuthorRepository = _dbContext.ResearchAuthor;
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

    public void Insert(Research research)
    {
        _researchRepository.Add(AddRelationshipsAsync(research));
    }

    public void Update(Research oldResearch, Research newResearch)
    {
        newResearch = AddRelationshipsAsync(newResearch);

        _dbContext.Entry(oldResearch).CurrentValues.SetValues(newResearch);

        UpdateRelationships(oldResearch, newResearch);
        
        // _dbContext.Entry(oldResearch).State = EntityState.Detached;
    }

    public void Delete(Research research)
    {
        _researchRepository.Remove(research);
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
            rf.FieldId = field.Id;
            rf.Research = research;
            rf.ResearchId = research.Id;
        });
            
        research.ResearchTag.ForEach(rt =>
        {
            var tag = _tagRepository.Find(rt.TagId);
            
            rt.Tag = tag;
            rt.TagId = tag.Id;
            rt.Research = research;
            rt.ResearchId = research.Id;
        });
            
        research.ResearchAuthor.ForEach(ra =>
        {
            var author = _authorRepository.Find(ra.AuthorId);
            
            ra.Author = author;
            ra.AuthorId = author.Id;
            ra.Research = research;
            ra.ResearchId = research.Id;
        });

        return research;
    }

    private void UpdateRelationships(Research oldResearch, Research newResearch)
    {
        _researchFieldRepository.RemoveRange(oldResearch.ResearchField);
        _researchTagRepository.RemoveRange(oldResearch.ResearchTag);
        _researchAuthorRepository.RemoveRange(oldResearch.ResearchAuthor);

        var newResearchField = newResearch.ResearchField.Select(rf => new ResearchField
        {
            Research = oldResearch,
            ResearchId = oldResearch.Id,
            Field = rf.Field,
            FieldId = rf.FieldId
        });
        
        var newResearchTag = newResearch.ResearchTag.Select(rt => new ResearchTag
        {
            Research = oldResearch,
            ResearchId = oldResearch.Id,
            Tag = rt.Tag,
            TagId = rt.TagId
        });
        
        var newResearchAuthor = newResearch.ResearchAuthor.Select(ra => new ResearchAuthor
        {
            Research = oldResearch,
            ResearchId = oldResearch.Id,
            Author = ra.Author,
            AuthorId = ra.AuthorId
        });

        _researchFieldRepository.AddRange(newResearchField);
        _researchTagRepository.AddRange(newResearchTag);
        _researchAuthorRepository.AddRange(newResearchAuthor);
    }
}