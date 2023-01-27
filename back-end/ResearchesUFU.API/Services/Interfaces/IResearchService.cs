using ResearchesUFU.API.Models;
using ResearchesUFU.API.Models.DTO.Requests;
using ResearchesUFU.API.Models.DTO.Responses;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services.Interfaces
{
    public interface IResearchService
    {
        public Task<HttpResponseBase<ResearchResponseDTO>> GetAsync(int id);
        
        public Task<HttpResponseBase<IQueryable<ResearchResponseDTO>>> GetAllAsync();

        public Task<HttpResponseBase<ResearchResponseDTO>> PostAsync(ResearchRequestDTO research);

        //public Task<HttpResponseBase<Research>> PutAsync(int id, Research research);

        //public Task<HttpResponseBase<Research>> DeleteAsync(int id);
    }
}
