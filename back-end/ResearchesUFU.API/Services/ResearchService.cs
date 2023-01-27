using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Models.DTO.Requests;
using ResearchesUFU.API.Models.DTO.Responses;
using ResearchesUFU.API.Services.Interfaces;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services
{
    public class ResearchService : IResearchService
    {
        private readonly ResearchesUFUContext _dbContext;
        public readonly IMapper _mapper;
        private readonly DbSet<Research> _researchesRepository;
        private readonly DbSet<ResearchField> _researcheFieldRepository;
        private readonly IFieldService _fieldService;

        public ResearchService(ResearchesUFUContext dbContext, IMapper mapper, IFieldService fieldService)
        {
            _dbContext = dbContext;
            _mapper = mapper;

            _researchesRepository = _dbContext.Researches;
            _researcheFieldRepository = _dbContext.ResearchField;

            _fieldService = fieldService;
        }

        public async Task<HttpResponseBase<ResearchResponseDTO>> GetAsync(int id)
        {
            try
            {
                var queryResult = await _researchesRepository.FindAsync(id);

                var responseDTO = await BuildResearchResponseDTO(queryResult);

                var response = HttpUtils<ResearchResponseDTO>.GenerateHttpResponse(responseDTO);

                return response;
            }
            catch (Exception ex)
            {
                return HttpUtils<ResearchResponseDTO>.GenerateHttpErrorResponse();
            }
        }

        public async Task<HttpResponseBase<IQueryable<ResearchResponseDTO>>> GetAllAsync()
        {
            try
            {
                var researchesList = await _researchesRepository.ToListAsync();
                var result = researchesList.Select(r => BuildResearchResponseDTO(r).Result);
                var queryableResult = result.AsQueryable();
                var response = HttpUtils<IQueryable<ResearchResponseDTO>>.GenerateHttpResponse(queryableResult);

                return response;
            }
            catch (Exception ex)
            {
                return HttpUtils<IQueryable<ResearchResponseDTO>>.GenerateHttpErrorResponse();
            }
        }

        public async Task<HttpResponseBase<ResearchResponseDTO>> PostAsync(ResearchRequestDTO researchRequest)
        {
            try
            {
                var research = _mapper.Map<Research>(researchRequest);
                var researchFieldList = await BuildResearchFieldList(research, researchRequest.Fields);

                _researchesRepository.Add(research);

                foreach (var rf in researchFieldList)
                {
                    _researcheFieldRepository.Add(rf);
                }

                await _dbContext.SaveChangesAsync();

                var researchResponse = _mapper.Map<ResearchResponseDTO>(research);
                researchResponse.Fields = researchFieldList.Select(rf => _mapper.Map<FieldResponseDTO>(rf.Field)).ToList();

                return HttpUtils<ResearchResponseDTO>.GenerateHttpResponse(researchResponse);
            }
            catch (Exception ex)
            {
                return HttpUtils<ResearchResponseDTO>.GenerateHttpErrorResponse();
            }
        }

        //public async Task<HttpResponseBase<Research>> PutAsync(int id, Research research)
        //{
        //    try
        //    {
        //        var getResponse = await GetAsync(id);

        //        if (HttpUtils<Research>.CheckIfIsValidHttpResponse(getResponse) == false)
        //        {
        //                return HttpUtils<Research>.GenerateHttpResponse(null);
        //        }

        //        _dbContext.Entry(getResponse.Content).State = EntityState.Detached;
                
        //        research.Id = id;

        //        _researchesRepository.Update(research);
        //        await _dbContext.SaveChangesAsync();

        //        return HttpUtils<Research>.GenerateHttpSuccessResponse();
        //    }
        //    catch (Exception ex)
        //    {
        //        return HttpUtils<Research>.GenerateHttpErrorResponse();
        //    }
        //}

        //public async Task<HttpResponseBase<Research>> DeleteAsync(int id)
        //{
        //    try
        //    {
        //        var getResponse = await GetAsync(id);

        //        if (HttpUtils<Research>.CheckIfIsValidHttpResponse(getResponse) == false)
        //        {
        //            return HttpUtils<Research>.GenerateHttpResponse(null);
        //        }

        //        var research = getResponse.Content;
        //        _researchesRepository.Remove(research);
        //        await _dbContext.SaveChangesAsync();

        //        return HttpUtils<Research>.GenerateHttpSuccessResponse();
        //    }
        //    catch (Exception ex)
        //    {
        //        return HttpUtils<Research>.GenerateHttpErrorResponse();
        //    }
        //}

        private async Task<ResearchResponseDTO> BuildResearchResponseDTO(Research research)
        {
            var fieldsTasks = (await _researcheFieldRepository.ToListAsync())
                    .Where(rf => rf.ResearchId.Equals(research.Id))
                    .Select(rf => _fieldService.GetAsync(rf.FieldId));
            var fields = (await Task.WhenAll(fieldsTasks))
                .Select(t => t.Content);

            var response = _mapper.Map<ResearchResponseDTO>(research);
            response.Fields = fields.Select(f => _mapper.Map<FieldResponseDTO>(f)).ToList();

            return response;
        }

        private async Task<List<ResearchField>> BuildResearchFieldList(Research research, List<FieldRequestDTO> fieldsDTO)
        {
            var fieldsTask = fieldsDTO.Select(f => _fieldService.GetAsync(f.Id));
            var fields = (await Task.WhenAll(fieldsTask)).Select(f => f.Content).ToList();

            var reasearchFieldList = fields.Select(f =>
                {
                    var researcheField = new ResearchField
                    {
                        Research = research,
                        Field = f
                    };

                    return researcheField;
                }
            ).ToList();

            return reasearchFieldList;
        }
    }
}
