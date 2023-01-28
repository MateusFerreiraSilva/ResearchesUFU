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
            catch
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
            catch
            {
                return HttpUtils<IQueryable<ResearchResponseDTO>>.GenerateHttpErrorResponse();
            }
        }

        public async Task<HttpResponseBase<ResearchResponseDTO>> PostAsync(ResearchRequestDTO researchRequest)
        {
            try
            {
                var research = _mapper.Map<Research>(researchRequest);

                var fieldsIds = researchRequest.Fields.Select(f => f.Id);
                var fields = fieldsIds.Select(id => _fieldService.FindOneAsync(id).Result).ToList();

                _researchesRepository.Add(research);
                SaveAllFields(research, fields);

                await _dbContext.SaveChangesAsync();
                _dbContext.Entry(research).State = EntityState.Detached;

                var responseDTO = await BuildResearchResponseDTO(research);

                return HttpUtils<ResearchResponseDTO>.GenerateHttpResponse(responseDTO);
            }
            catch
            {
                return HttpUtils<ResearchResponseDTO>.GenerateHttpErrorResponse();
            }
        }

        public async Task<HttpResponseBase<ResearchResponseDTO>> PutAsync(int id, ResearchRequestDTO researchRequest)
        {
            try
            {
                var research = await FindOneAsync(id);

                var newFieldsIds = researchRequest.Fields.Select(f => f.Id);
                var newfields = newFieldsIds.Select(id => _fieldService.FindOneAsync(id).Result).ToList();
                var oldFields = await GetFieldsAsync(id);

                _dbContext.Entry(research).State = EntityState.Detached;

                research = _mapper.Map<Research>(researchRequest);
                research.Id = id;

                _researchesRepository.Update(research);

                RemoveAllFields(research, oldFields);
                SaveAllFields(research, newfields);

                await _dbContext.SaveChangesAsync();

                var researchDTO = await BuildResearchResponseDTO(research);

                return HttpUtils<ResearchResponseDTO>.GenerateHttpResponse(researchDTO);
            }
            catch
            {
                return HttpUtils<ResearchResponseDTO>.GenerateHttpErrorResponse();
            }
        }

        public async Task<HttpResponseBase<ResearchResponseDTO>> DeleteAsync(int id)
        {
            try
            {
                var research = await FindOneAsync(id);

                var fields = await GetFieldsAsync(research.Id);

                _researchesRepository.Remove(research);
                RemoveAllFields(research, fields);

                await _dbContext.SaveChangesAsync();

                return HttpUtils<ResearchResponseDTO>.GenerateHttpSuccessResponse();
            }
            catch
            {
                return HttpUtils<ResearchResponseDTO>.GenerateHttpErrorResponse();
            }
        }

        public async Task<Research> FindOneAsync(int id)
        {
            try
            {
                return await _researchesRepository.FindAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IQueryable<Research>> FindAllAsync()
        {
            try
            {
                var researchesList = await _researchesRepository.ToListAsync();

                return researchesList.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        private async Task<List<Field>> GetFieldsAsync(int id)
        {
            var researchField = await _researchFieldRepository.ToListAsync();
            var fieldsIds = researchField.Where(rf => rf.ResearchId.Equals(id)).Select(rf => rf.FieldId);
            var fields = fieldsIds.Select(id => _fieldService.FindOneAsync(id).Result);

            return fields.ToList();
        }

        private void SaveAllFields(Research research, List<Field> fields)
        {
            var researchFieldList = BuildResearchFieldList(research, fields);

            _researchFieldRepository.AddRange(researchFieldList);
        }

        private void RemoveAllFields(Research research, List<Field> fields)
        {
            var researchFieldList = BuildResearchFieldList(research, fields);

            _researchFieldRepository.RemoveRange(researchFieldList);
        }

        private List<ResearchField> BuildResearchFieldList(Research research, List<Field> fields)
        {
            return fields.Select(f =>
                new ResearchField
                {
                    Research = research,
                    Field = f
                }
            ).ToList();
        }

        private async Task<ResearchResponseDTO> BuildResearchResponseDTO(Research research)
        {
            if (research != null)
            {
                var fields = await GetFieldsAsync(research.Id);
                //var tags = await GetTagsAsync(research.Id);

                var researchDTO = _mapper.Map<ResearchResponseDTO>(research);
                var fieldsDTO = _mapper.Map<List<FieldResponseDTO>>(fields);

                researchDTO.Fields = fieldsDTO;

                return researchDTO;
            }

            return null;
        }
    }
}
