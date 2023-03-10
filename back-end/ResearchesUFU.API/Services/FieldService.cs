using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Services.Interfaces;

namespace ResearchesUFU.API.Services
{
    public class FieldService : IFieldService
    {
        private readonly ResearchesUFUContext _dbContext;
        private readonly DbSet<Field> _fieldRepository;

        public FieldService(ResearchesUFUContext dbContext)
        {
            _dbContext = dbContext;

            _fieldRepository = _dbContext.Fields;
        }
        public async Task<Field> FindOneAsync(int id)
        {
            try
            {
                return await _fieldRepository.FindAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IQueryable<Field>> FindAllAsync()
        {
            try
            {
                var fieldsList = await _fieldRepository.ToListAsync();

                return fieldsList.AsQueryable();
            }
            catch
            {
                return null;
            }
        }
    }
}