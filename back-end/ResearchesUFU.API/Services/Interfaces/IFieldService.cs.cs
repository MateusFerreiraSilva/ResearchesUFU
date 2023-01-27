using ResearchesUFU.API.Models;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services.Interfaces
{
    public interface IFieldService
    {
        public Task<HttpResponseBase<Field>> GetAsync(int id);

        public Task<HttpResponseBase<IQueryable<Field>>> GetAllAsync();
    }
}
