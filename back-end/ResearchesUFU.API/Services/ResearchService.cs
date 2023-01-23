using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Services.Interfaces;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services
{
    public class ResearchService : IResearchService
    {
        private readonly ResearchesUFUContext _dbContext;
        private readonly DbSet<Research> _researchesRepository;

        public ResearchService(ResearchesUFUContext dbContext)
        {
            _dbContext = dbContext;
            _researchesRepository = _dbContext.Researches;
        }

        public async Task<HttpResponseBase<Research>> GetAsync(int id)
        {
            try
            {
                var queryResult = await _researchesRepository.FindAsync(id);

                var response = HttpUtils<Research>.GenerateHttpResponse(queryResult);

                return response;
            }
            catch (Exception ex)
            {
                return HttpUtils<Research>.GenerateHttpErrorResponse();
            }
        }

        public async Task<HttpResponseBase<IQueryable<Research>>> GetAllAsync()
        {
            try
            {
                var result = await _researchesRepository.ToListAsync();
                var queryableResult = result.AsQueryable();
                var response = HttpUtils<IQueryable<Research>>.GenerateHttpResponse(queryableResult);

                return response;
            }
            catch (Exception ex)
            {
                return HttpUtils<IQueryable<Research>>.GenerateHttpErrorResponse();
            }
        }

        public async Task<HttpResponseBase<IdResponse>> PostAsync(Research research)
        {
            try
            {
                var idReponse = new IdResponse();

                _researchesRepository.Add(research);
                await _dbContext.SaveChangesAsync();

                idReponse.Id = research.Id;

                return HttpUtils<IdResponse>.GenerateHttpResponse(idReponse);
            }
            catch (Exception ex)
            {
                return HttpUtils<IdResponse>.GenerateHttpErrorResponse();
            }
        }

        public async Task<HttpResponseBase<Research>> PutAsync(int id, Research research)
        {
            try
            {
                var getResponse = await GetAsync(id);

                if (HttpUtils<Research>.CheckIfIsValidHttpResponse(getResponse) == false)
                {
                        return HttpUtils<Research>.GenerateHttpResponse(null);
                }

                _dbContext.Entry(getResponse.Content).State = EntityState.Detached;
                
                research.Id = id;

                _researchesRepository.Update(research);
                await _dbContext.SaveChangesAsync();

                return HttpUtils<Research>.GenerateHttpSuccessResponse();
            }
            catch (Exception ex)
            {
                return HttpUtils<Research>.GenerateHttpErrorResponse();
            }
        }

        public async Task<HttpResponseBase<Research>> DeleteAsync(int id)
        {
            try
            {
                var getResponse = await GetAsync(id);

                if (HttpUtils<Research>.CheckIfIsValidHttpResponse(getResponse) == false)
                {
                    return HttpUtils<Research>.GenerateHttpResponse(null);
                }

                var research = getResponse.Content;
                _researchesRepository.Remove(research);
                await _dbContext.SaveChangesAsync();

                return HttpUtils<Research>.GenerateHttpSuccessResponse();
            }
            catch (Exception ex)
            {
                return HttpUtils<Research>.GenerateHttpErrorResponse();
            }
        }
    }
}
