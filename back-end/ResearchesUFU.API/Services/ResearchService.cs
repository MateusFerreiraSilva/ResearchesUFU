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
        
        public async Task<HttpResponseBase<ResearchResponseDto>> GetAsync(int id)
        {
            async Task<HttpResponseBase<ResearchResponseDto>> Method()
            {
                var research = await GetOneAsync(id);

                return research != null ?
                    HttpUtils<ResearchResponseDto>.GenerateHttpSuccessResponse(_mapper.Map<ResearchResponseDto>(research)) :
                    HttpUtils<ResearchResponseDto>.GenerateHttpResponse(StatusCodes.Status404NotFound);
            }

            var response = await ExecuteMethodAsync(Method);

            return response;
        }

        public async Task<HttpResponseBase<IQueryable<ResearchResponseDto>>> GetAsync()
        {
            async Task<HttpResponseBase<IQueryable<ResearchResponseDto>>> Method()
            {
                var researches = await GetAllAsync();

                if (researches == null || researches.Count().Equals(default))
                {
                    return HttpUtils<IQueryable<ResearchResponseDto>>.GenerateHttpResponse(StatusCodes.Status404NotFound);
                }

                return HttpUtils<IQueryable<ResearchResponseDto>>.GenerateHttpSuccessResponse(researches.Select(r => _mapper.Map<ResearchResponseDto>(r)));
            }

            var response = await ExecuteMethodAsync(Method);
        
            return response;
        }

        public async Task<HttpResponseBase<ResearchResponseDto>> PostAsync(ResearchRequestDto researchRequest)
        {
            async Task<HttpResponseBase<ResearchResponseDto>> Method()
            {
                var research = _mapper.Map<Research>(researchRequest);

                Insert(research);
                await SaveAsync();

                var responseDto = _mapper.Map<ResearchResponseDto>(research);

                return HttpUtils<ResearchResponseDto>.GenerateHttpSuccessResponse(responseDto);
            }

            var response = await ExecuteMethodAsync(Method);
        
            return response;
        }
        
        public async Task<HttpResponseBase<ResearchResponseDto>> PutAsync(int id, ResearchRequestDto researchRequest)
        {
            async Task<HttpResponseBase<ResearchResponseDto>> Method()
            {
                var oldResearch = await GetOneAsync(id);

                if (oldResearch == null)
                {
                    return HttpUtils<ResearchResponseDto>.GenerateHttpResponse(StatusCodes.Status404NotFound);
                }

                var newResearch = _mapper.Map<Research>(researchRequest);
                newResearch.Id = oldResearch.Id;

                Update(oldResearch, newResearch);
                await SaveAsync();

                return HttpUtils<ResearchResponseDto>.GenerateHttpSuccessResponse(_mapper.Map<ResearchResponseDto>(newResearch));
            }

            var response = await ExecuteMethodAsync(Method);
        
            return response;
        }
        
        public async Task<HttpResponseBase<ResearchResponseDto>> DeleteAsync(int id)
        {
            async Task<HttpResponseBase<ResearchResponseDto>> Method()
            {
                var research = await GetOneAsync(id);

                if (research == null)
                {
                    return HttpUtils<ResearchResponseDto>.GenerateHttpResponse(StatusCodes.Status404NotFound);
                }

                Delete(research);
                await SaveAsync();

                return HttpUtils<ResearchResponseDto>.GenerateHttpSuccessResponse();
            }

            var response = await ExecuteMethodAsync(Method);
        
            return response;
        }
    }
}
