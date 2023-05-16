using ResearchesUFU.API.Models.DTO.Requests;
using ResearchesUFU.API.Models.DTO.Responses;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services.Interfaces
{
    public interface IResearchService
    {
        public Task<HttpResponseBase<ResearchResponseDTO>> GetAsync(int id);
        
        public Task<HttpResponseBase<IQueryable<ResearchResponseDTO>>> GetAsync();
        
        public Task<HttpResponseBase<ResearchResponseDTO>> PostAsync(ResearchRequestDTO research);
        
        public Task<HttpResponseBase<ResearchResponseDTO>> PutAsync(int id, ResearchRequestDTO research);
        
        public Task<HttpResponseBase<ResearchResponseDTO>> DeleteAsync(int id);
    }
}
