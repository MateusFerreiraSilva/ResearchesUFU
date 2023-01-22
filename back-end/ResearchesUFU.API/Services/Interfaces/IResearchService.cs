using ResearchesUFU.API.Models;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services.Interfaces
{
    public interface IResearchService
    {
        public HttpResponseBase<Research> Get(int id);
        
        public HttpResponseBase<IQueryable<Research>> GetAll();

        public HttpResponseBase<IdResponse> Post(Research research);

    }
}
