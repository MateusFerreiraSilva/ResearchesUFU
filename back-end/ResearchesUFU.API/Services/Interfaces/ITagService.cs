using ResearchesUFU.API.Models;

namespace ResearchesUFU.API.Services.Interfaces
{
    public interface ITagService
    {
        public Task<Tag> FindOneAsync(int id);

        public Task<IQueryable<Tag>> FindAllAsync();
    }
}
