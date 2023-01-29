using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Services.Interfaces;

namespace ResearchesUFU.API.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ResearchesUFUContext _dbContext;
        private readonly DbSet<Author> _authorRepository;

        public AuthorService(ResearchesUFUContext dbContext)
        {
            _dbContext = dbContext;

            _authorRepository = _dbContext.Authors;
        }
        public async Task<Author> FindOneAsync(int id)
        {
            try
            {
                return await _authorRepository.FindAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IQueryable<Author>> FindAllAsync()
        {
            try
            {
                var fieldsList = await _authorRepository.ToListAsync();

                return fieldsList.AsQueryable();
            }
            catch
            {
                return null;
            }
        }
    }
}