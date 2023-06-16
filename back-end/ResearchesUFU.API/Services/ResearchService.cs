using AutoMapper;
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
            async Task<HttpResponseBase<ResearchResponseDTO>> Method()
            {
                var research = await GetOneAsync(id);

                return research != null ?
                    HttpUtils<ResearchResponseDTO>.GenerateHttpSuccessResponse(_mapper.Map<ResearchResponseDTO>(research)) :
                    HttpUtils<ResearchResponseDTO>.GenerateHttpResponse(StatusCodes.Status404NotFound);
            }

            var response = await ExecuteMethodAsync(Method);

            return response;
        }

        public async Task<HttpResponseBase<IQueryable<ResearchResponseDTO>>> GetAsync()
        {
            async Task<HttpResponseBase<IQueryable<ResearchResponseDTO>>> Method()
            {
                var researches = await GetAllAsync();

                if (researches == null || researches.Count().Equals(default))
                {
                    return HttpUtils<IQueryable<ResearchResponseDTO>>.GenerateHttpResponse(StatusCodes.Status404NotFound);
                }

                return HttpUtils<IQueryable<ResearchResponseDTO>>.GenerateHttpSuccessResponse(researches.Select(r => _mapper.Map<ResearchResponseDTO>(r)));
            }

            var response = await ExecuteMethodAsync(Method);
        
            return response;
        }

        public async Task<HttpResponseBase<ResearchResponseDTO>> PostAsync(ResearchRequestDTO researchRequest)
        {
            async Task<HttpResponseBase<ResearchResponseDTO>> Method()
            {
                var research = _mapper.Map<Research>(researchRequest);

                Insert(research);
                await SaveAsync();

                var responseDto = _mapper.Map<ResearchResponseDTO>(research);

                return HttpUtils<ResearchResponseDTO>.GenerateHttpSuccessResponse(responseDto);
            }

            var response = await ExecuteMethodAsync(Method);
        
            return response;
        }
        
        public async Task<HttpResponseBase<ResearchResponseDTO>> PutAsync(int id, ResearchRequestDTO researchRequest)
        {
            async Task<HttpResponseBase<ResearchResponseDTO>> Method()
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
            }

            var response = await ExecuteMethodAsync(Method);
        
            return response;
        }
        
        public async Task<HttpResponseBase<ResearchResponseDTO>> DeleteAsync(int id)
        {
            async Task<HttpResponseBase<ResearchResponseDTO>> Method()
            {
                var research = await GetOneAsync(id);

                if (research == null)
                {
                    return HttpUtils<ResearchResponseDTO>.GenerateHttpResponse(StatusCodes.Status404NotFound);
                }

                Delete(research);
                await SaveAsync();

                return HttpUtils<ResearchResponseDTO>.GenerateHttpSuccessResponse();
            }

            var response = await ExecuteMethodAsync(Method);
        
            return response;
        }
    }
}
