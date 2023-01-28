using ResearchesUFU.API.Models;

namespace ResearchesUFU.API.Services.Interfaces
{
    public interface IFieldService
    {
        public Task<Field> FindOneAsync(int id);

        public Task<IQueryable<Field>> FindAllAsync();
    }
}
