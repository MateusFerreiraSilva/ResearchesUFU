using ResearchesUFU.API.Models;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services.Interfaces
{
    public interface IResearchService
    {
        public Task<HttpResponseBase<Research>> GetAsync(int id);
        
        public Task<HttpResponseBase<IQueryable<Research>>> GetAllAsync();

        public Task<HttpResponseBase<IdResponse>> PostAsync(Research research);

        public Task<HttpResponseBase<Research>> PutAsync(int id, Research research);

        public Task<HttpResponseBase<Research>> DeleteAsync(int id);
    }
}
