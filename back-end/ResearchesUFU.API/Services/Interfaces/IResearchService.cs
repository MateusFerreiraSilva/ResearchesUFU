using ResearchesUFU.API.Models;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services.Interfaces
{
    public interface IResearchService
    {
        public HttpResponseBase<Research> Get(int id);
    }
}
