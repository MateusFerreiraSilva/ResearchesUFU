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
        private readonly DbSet<Research> _researchesRepository;
        private readonly DbSet<ResearchField> _researchFieldRepository;
        private readonly DbSet<ResearchTag> _researchTagRepository;
        private readonly DbSet<ResearchAuthor> _researchAuthorRepository;

        private readonly IFieldService _fieldService;
        private readonly ITagService _tagService;
        private readonly IAuthorService _authorService;

        public readonly IMapper _mapper;

        public ResearchService(
            ResearchesUFUContext dbContext,
            IFieldService fieldService,
            ITagService tagService,
            IAuthorService authorService,
            IMapper mapper
        )
        {
            _dbContext = dbContext;

            _researchesRepository = _dbContext.Researches;
            _researchFieldRepository = _dbContext.ResearchField;
            _researchTagRepository = _dbContext.ResearchTag;
            _researchAuthorRepository = _dbContext.ResearchAuthor;

            _fieldService = fieldService;
            _tagService = tagService;
            _authorService = authorService;

            _mapper = mapper;
        }

        public async Task<HttpResponseBase<ResearchResponseDTO>> GetAsync(int id)
        {
            try
            {
                var responseDTO = await BuildResearchResponseDTO(await FindOneAsync(id));

                var response = HttpUtils<ResearchResponseDTO>.GenerateHttpSuccessResponse(responseDTO);

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

                var response = HttpUtils<IQueryable<ResearchResponseDTO>>.GenerateHttpSuccessResponse(responseDTOList);

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
                
                var tagsIds = researchRequest.Tags.Select(t => t.Id);
                var tags = tagsIds.Select(id => _tagService.FindOneAsync(id).Result).ToList();

                var authorsIds = researchRequest.Authors.Select(a => a.Id);
                var authors = authorsIds.Select(id => _authorService.FindOneAsync(id).Result).ToList();

                _researchesRepository.Add(research);
                SaveAllFields(research, fields);
                SaveAllTags(research, tags);
                SaveAllAuthors(research, authors);

                await _dbContext.SaveChangesAsync();
                _dbContext.Entry(research).State = EntityState.Detached;

                var responseDTO = await BuildResearchResponseDTO(research);

                return HttpUtils<ResearchResponseDTO>.GenerateHttpSuccessResponse(responseDTO);
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
                var newFields = newFieldsIds.Select(id => _fieldService.FindOneAsync(id).Result).ToList();
                var oldFields = await GetFieldsAsync(id);

                var newTagsIds = researchRequest.Tags.Select(t => t.Id);
                var newTags = newTagsIds.Select(id => _tagService.FindOneAsync(id).Result).ToList();
                var oldTags = await GetTagsAsync(id);

                var newAuthorsIds = researchRequest.Authors.Select(a => a.Id);
                var newAuthors = newAuthorsIds.Select(id => _authorService.FindOneAsync(id).Result).ToList();
                var oldAuthors = await GetAuthorsAsync(id);

                _dbContext.Entry(research).State = EntityState.Detached;

                research = _mapper.Map<Research>(researchRequest);
                research.Id = id;

                _researchesRepository.Update(research);

                RemoveAllFields(research, oldFields);
                SaveAllFields(research, newFields);
                
                RemoveAllTags(research, oldTags);
                SaveAllTags(research, newTags);

                RemoveAllAuthors(research, oldAuthors);
                SaveAllAuthors(research, newAuthors);


                await _dbContext.SaveChangesAsync();

                var researchDTO = await BuildResearchResponseDTO(research);

                return HttpUtils<ResearchResponseDTO>.GenerateHttpSuccessResponse(researchDTO);
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

        private async Task<List<Tag>> GetTagsAsync(int id)
        {
            var researchTag= await _researchTagRepository.ToListAsync();
            var tagsIds = researchTag.Where(rt => rt.ResearchId.Equals(id)).Select(rt => rt.TagId);
            var tags = tagsIds.Select(id => _tagService.FindOneAsync(id).Result);

            return tags.ToList();
        }

        private void SaveAllTags(Research research, List<Tag> tags)
        {
            var researchTagList = BuildResearchTagList(research, tags);

            _researchTagRepository.AddRange(researchTagList);
        }

        private void RemoveAllTags(Research research, List<Tag> tags)
        {
            var researchTagList = BuildResearchTagList(research, tags);

            _researchTagRepository.RemoveRange(researchTagList);
        }

        private List<ResearchTag> BuildResearchTagList(Research research, List<Tag> tags)
        {
            return tags.Select(t =>
                new ResearchTag
                {
                    Research = research,
                    Tag = t
                }
            ).ToList();
        }
        private async Task<List<Author>> GetAuthorsAsync(int id)
        {
            var researchAuthor = await _researchAuthorRepository.ToListAsync();
            var auhtorsIds = researchAuthor.Where(ra => ra.ResearchId.Equals(id)).Select(ra => ra.AuthorId);
            var authors = auhtorsIds.Select(id => _authorService.FindOneAsync(id).Result);

            return authors.ToList();
        }

        private void SaveAllAuthors(Research research, List<Author> authors)
        {
            var researchAuhtorList = BuildResearchAuthorList(research, authors);

            _researchAuthorRepository.AddRange(researchAuhtorList);
        }

        private void RemoveAllAuthors(Research research, List<Author> authors)
        {
            var researchAuthorList = BuildResearchAuthorList(research, authors);

            _researchAuthorRepository.RemoveRange(researchAuthorList);
        }

        private List<ResearchAuthor> BuildResearchAuthorList(Research research, List<Author> authors)
        {
            return authors.Select(a =>
                new ResearchAuthor
                {
                    Research = research,
                    Author = a
                }
            ).ToList();
        }

        private async Task<ResearchResponseDTO> BuildResearchResponseDTO(Research research)
        {
            if (research != null)
            {
                var fields = await GetFieldsAsync(research.Id);
                var tags = await GetTagsAsync(research.Id);
                var authors = await GetAuthorsAsync(research.Id);

                var researchDTO = _mapper.Map<ResearchResponseDTO>(research);
                var fieldsDTO = _mapper.Map<List<FieldResponseDTO>>(fields);
                var tagsDTO = _mapper.Map<List<TagResponseDTO>>(tags);
                var authorsDTO = _mapper.Map<List<AuthorResponseDTO>>(authors);

                researchDTO.Fields = fieldsDTO;
                researchDTO.Tags = tagsDTO;
                researchDTO.Authors = authorsDTO;

                return researchDTO;
            }

            return null;
        }
    }
}
