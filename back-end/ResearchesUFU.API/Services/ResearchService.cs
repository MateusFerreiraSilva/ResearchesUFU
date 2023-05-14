using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Models.DTO.Requests;
using ResearchesUFU.API.Models.DTO.Responses;
using ResearchesUFU.API.Repositories.Interfaces;
using ResearchesUFU.API.Services.Interfaces;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services
{
    public class ResearchService : BaseService<Research>, IResearchService
    {
        private readonly IMapper _mapper;

        public ResearchService(
            IBaseRepository<Research> researchRepository,
            IMapper mapper
        ) : base(researchRepository)
        {
            _mapper = mapper;
        }
        
        public async Task<HttpResponseBase<ResearchResponseDTO>> GetAsync(int id)
        {
            var method = async delegate()
            {
                var research = await GetOneAsync(id);

                if (research == null)
                {
                    return HttpUtils<ResearchResponseDTO>.GenerateHttpResponse(StatusCodes.Status404NotFound);
                }

                return HttpUtils<ResearchResponseDTO>.GenerateHttpSuccessResponse(_mapper.Map<ResearchResponseDTO>(research));
            };
            
            var response = await ExecuteMethodAsync(method);

            return response;
        }

        public async Task<HttpResponseBase<IQueryable<ResearchResponseDTO>>> GetAsync()
        {
            var method = async delegate()
            {
                var researches = await GetAllAsync();
        
                if (researches == null || researches.Count().Equals(default(int)))
                {
                    return HttpUtils<IQueryable<ResearchResponseDTO>>.GenerateHttpResponse(StatusCodes.Status404NotFound);
                }
                
                return HttpUtils<IQueryable<ResearchResponseDTO>>.GenerateHttpSuccessResponse(
                    researches.Select(r => _mapper.Map<ResearchResponseDTO>(r))
                );
            };
            
            var response = await ExecuteMethodAsync(method);
        
            return response;
        }

        public async Task<HttpResponseBase<ResearchResponseDTO>> PostAsync(ResearchRequestDTO researchRequest)
        {
            var method = async delegate()
            {
                var research = _mapper.Map<Research>(researchRequest);

                Insert(research);
                await SaveAsync();

                var responseDTO = _mapper.Map<ResearchResponseDTO>(research);
        
                return HttpUtils<ResearchResponseDTO>.GenerateHttpSuccessResponse(responseDTO);
            };
            
            var response = await ExecuteMethodAsync(method);
        
            return response;
        }
        
        public async Task<HttpResponseBase<ResearchResponseDTO>> PutAsync(int id, ResearchRequestDTO researchRequest)
        {
            var method = async delegate()
            {
                var oldResearch = await GetOneAsync(id);
                
                if (oldResearch == null)
                {
                    return HttpUtils<ResearchResponseDTO>.GenerateHttpResponse(StatusCodes.Status404NotFound);
                }
                
                var newResearch = _mapper.Map<Research>(researchRequest);
                newResearch.Id = oldResearch.Id;

                Update(oldResearch, newResearch);
                await SaveAsync();

                return HttpUtils<ResearchResponseDTO>.GenerateHttpSuccessResponse(_mapper.Map<ResearchResponseDTO>(newResearch));
            };
            
            var response = await ExecuteMethodAsync(method);
        
            return response;
        }
        
        public async Task<HttpResponseBase<ResearchResponseDTO>> DeleteAsync(int id)
        {
            var method = async delegate()
            {
                var research = await GetOneAsync(id);

                if (research == null)
                {
                    return HttpUtils<ResearchResponseDTO>.GenerateHttpResponse(StatusCodes.Status404NotFound);
                }
                
                Delete(research);
                await SaveAsync();

                return HttpUtils<ResearchResponseDTO>.GenerateHttpSuccessResponse();
            };
            
            var response = await ExecuteMethodAsync(method);
        
            return response;
        }
    }
}
