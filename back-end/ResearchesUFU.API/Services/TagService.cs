using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Services.Interfaces;

namespace ResearchesUFU.API.Services
{
    public class TagService : ITagService
    {
        private readonly ResearchesUFUContext _dbContext;
        private readonly DbSet<Tag> _tagRepository;

        public TagService(ResearchesUFUContext dbContext)
        {
            _dbContext = dbContext;

            _tagRepository = _dbContext.Tags;
        }
        public async Task<Tag> FindOneAsync(int id)
        {
            try
            {
                return await _tagRepository.FindAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IQueryable<Tag>> FindAllAsync()
        {
            try
            {
                var fieldsList = await _tagRepository.ToListAsync();

                return fieldsList.AsQueryable();
            }
            catch
            {
                return null;
            }
        }
    }
}
