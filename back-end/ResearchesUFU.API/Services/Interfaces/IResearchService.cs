using ResearchesUFU.API.Models.DTO.Requests;
using ResearchesUFU.API.Models.DTO.Responses;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services.Interfaces
{
    public interface IResearchService
    {
        public Task<HttpResponseBase<ResearchResponseDto>> GetAsync(int id);
        
        public Task<HttpResponseBase<IQueryable<ResearchResponseDto>>> GetAsync();
        
        public Task<HttpResponseBase<ResearchResponseDto>> PostAsync(ResearchRequestDto research);
        
        public Task<HttpResponseBase<ResearchResponseDto>> PutAsync(int id, ResearchRequestDto research);
        
        public Task<HttpResponseBase<ResearchResponseDto>> DeleteAsync(int id);
    }
}
