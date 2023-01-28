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
        private readonly DbSet<ResearchField> _researchFieldRepository;
        private readonly IFieldService _fieldService;

        public ResearchService(ResearchesUFUContext dbContext, IMapper mapper, IFieldService fieldService)
        {
            _dbContext = dbContext;
            _mapper = mapper;

            _researchesRepository = _dbContext.Researches;
            _researchFieldRepository = _dbContext.ResearchField;

            _fieldService = fieldService;
        }

        public async Task<HttpResponseBase<ResearchResponseDTO>> GetAsync(int id)
        {
            try
            {
                var responseDTO = await BuildResearchResponseDTO(await FindOneAsync(id));

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
                var responseDTOList = (await FindAllAsync())
                    .Select(r => BuildResearchResponseDTO(r).Result)
                    .AsQueryable();

                var response = HttpUtils<IQueryable<ResearchResponseDTO>>.GenerateHttpResponse(responseDTOList);

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
                var fields = await GetFieldsAsync(research.Id);

                _researchesRepository.Add(research);
                SaveAllFields(research, fields);

                await _dbContext.SaveChangesAsync();

                var responseDTO = await BuildResearchResponseDTO(research);

                return HttpUtils<ResearchResponseDTO>.GenerateHttpResponse(responseDTO);
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

        public async Task<Research> FindOneAsync(int id)
        {
            return await _researchesRepository.FindAsync(id);
        }

        public async Task<IQueryable<Research>> FindAllAsync()
        {
            var researchesList = await _researchesRepository.ToListAsync();
            
            return researchesList.AsQueryable();
        }

        private async Task<List<Field>> GetFieldsAsync(int id)
        {
            var researchField = await _researchFieldRepository.ToListAsync();
            var fieldsIds = researchField.Where(rf => rf.ResearchId.Equals(id)).Select(rf => rf.FieldId);
            var fields = fieldsIds.Select(id => _fieldService.FindOneAsync(id).Result);

            return fields.ToList();
        }

        private void SaveOneField(Research research, Field field)
        {
            var researchField = new ResearchField
            {
                Research = research,
                Field = field
            };

            _researchFieldRepository.Add(researchField);
        }

        private void SaveAllFields(Research research, List<Field> fields)
        {
            foreach (var f in fields)
            {
                SaveOneField(research, f);
            }
        }

        private async Task<ResearchResponseDTO> BuildResearchResponseDTO(Research research)
        {
            var fields = await GetFieldsAsync(research.Id);
            //var tags = await GetTagsAsync(research.Id);

            var researchDTO = _mapper.Map<ResearchResponseDTO>(research);
            var fieldsDTO = _mapper.Map<List<FieldResponseDTO>>(fields);

            researchDTO.Fields = fieldsDTO;

            return researchDTO;
        }


        //private async Task<ResearchResponseDTO> BuildResearchResponseDTO(Research research)
        //{
        //    var fieldsTasks = (await _researchFieldRepository.ToListAsync())
        //            .Where(rf => rf.ResearchId.Equals(research.Id))
        //            .Select(rf => _fieldService.GetAsync(rf.FieldId));
        //    var fields = (await Task.WhenAll(fieldsTasks))
        //        .Select(t => t.Content);

        //    var response = _mapper.Map<ResearchResponseDTO>(research);
        //    response.Fields = fields.Select(f => _mapper.Map<FieldResponseDTO>(f)).ToList();

        //    return response;
        //}

        //private async Task<List<ResearchField>> BuildResearchFieldList(Research research, List<FieldRequestDTO> fieldsDTO)
        //{
        //    var fieldsTask = fieldsDTO.Select(f => _fieldService.GetAsync(f.Id));
        //    var fields = (await Task.WhenAll(fieldsTask)).Select(f => f.Content).ToList();

        //    var reasearchFieldList = fields.Select(f =>
        //        {
        //            var researcheField = new ResearchField
        //            {
        //                Research = research,
        //                Field = f
        //            };

        //            return researcheField;
        //        }
        //    ).ToList();

        //    return reasearchFieldList;
        //}
    }
}
