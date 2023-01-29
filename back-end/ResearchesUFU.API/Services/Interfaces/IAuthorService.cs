using ResearchesUFU.API.Models;

namespace ResearchesUFU.API.Services.Interfaces
{
    public interface IAuthorService
    {
        public Task<Author> FindOneAsync(int id);

        public Task<IQueryable<Author>> FindAllAsync();
    }
}
