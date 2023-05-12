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

        // public async Task<HttpResponseBase<ResearchResponseDTO>> PostAsync(ResearchRequestDTO researchRequest)
        // {
        //     var method = async delegate()
        //     {
        //         var research = _mapper.Map<Research>(researchRequest);
        //         
        //         var fields = await _fieldService.FindManyAsync(researchRequest.Fields.Select(f => f.Id));
        //         var tags = await _tagService.FindManyAsync(researchRequest.Tags.Select(t => t.Id));
        //         var authors = await _authorService.FindManyAsync(researchRequest.Authors.Select(a => a.Id));
        //
        //         Repository.Add(research);
        //         SaveAllFields(research, fields);
        //         SaveAllTags(research, tags);
        //         SaveAllAuthors(research, authors);
        //
        //         await DbContext.SaveChangesAsync();
        //         DbContext.Entry(research).State = EntityState.Detached;
        //
        //         var responseDTO = await BuildResearchResponseDTO(research);
        //
        //         return HttpUtils<ResearchResponseDTO>.GenerateHttpSuccessResponse(responseDTO);
        //     };
        //     
        //     var response = await ExecuteMethodAsync(method);
        //
        //     return response;
        // }
        //
        // public async Task<HttpResponseBase<ResearchResponseDTO>> PutAsync(int id, ResearchRequestDTO researchRequest)
        // {
        //     var method = async delegate()
        //     {
        //         var research = await FindOneAsync(id);
        //
        //         var newFieldsIds = researchRequest.Fields.Select(f => f.Id);
        //         var newFields = newFieldsIds.Select(id => _fieldService.FindOneAsync(id).Result).ToList();
        //         var oldFields = await GetFieldsAsync(id);
        //
        //         var newTagsIds = researchRequest.Tags.Select(t => t.Id);
        //         var newTags = newTagsIds.Select(id => _tagService.FindOneAsync(id).Result).ToList();
        //         var oldTags = await GetTagsAsync(id);
        //
        //         var newAuthorsIds = researchRequest.Authors.Select(a => a.Id);
        //         var newAuthors = newAuthorsIds.Select(id => _authorService.FindOneAsync(id).Result).ToList();
        //         var oldAuthors = await GetAuthorsAsync(id);
        //
        //         DbContext.Entry(research).State = EntityState.Detached;
        //
        //         research = _mapper.Map<Research>(researchRequest);
        //         research.Id = id;
        //
        //         Repository.Update(research);
        //
        //         RemoveAllFields(research, oldFields);
        //         SaveAllFields(research, newFields);
        //         
        //         RemoveAllTags(research, oldTags);
        //         SaveAllTags(research, newTags);
        //
        //         RemoveAllAuthors(research, oldAuthors);
        //         SaveAllAuthors(research, newAuthors);
        //
        //
        //         await DbContext.SaveChangesAsync();
        //
        //         var researchDTO = await BuildResearchResponseDTO(research);
        //
        //         return HttpUtils<ResearchResponseDTO>.GenerateHttpSuccessResponse(researchDTO);
        //     };
        //     
        //     var response = await ExecuteMethodAsync(method);
        //
        //     return response;
        // }
        //
        // public async Task<HttpResponseBase<ResearchResponseDTO>> DeleteAsync(int id)
        // {
        //     var method = async delegate()
        //     {
        //         var research = await FindOneAsync(id);
        //
        //         var fields = await GetFieldsAsync(research.Id);
        //
        //         Repository.Remove(research);
        //         RemoveAllFields(research, fields);
        //
        //         await DbContext.SaveChangesAsync();
        //
        //         return HttpUtils<ResearchResponseDTO>.GenerateHttpSuccessResponse();
        //     };
        //     
        //     var response = await ExecuteMethodAsync(method);
        //
        //     return response;
        // }
        //
        // private async Task<List<Field>> GetFieldsAsync(int id)
        // {
        //     var researchField = await _researchFieldRepository.ToListAsync();
        //     var fieldsIds = researchField.Where(rf => rf.ResearchId.Equals(id)).Select(rf => rf.FieldId);
        //     var fields = fieldsIds.Select(id => _fieldService.FindOneAsync(id).Result);
        //
        //     return fields.ToList();
        // }
        //
        // private void SaveAllFields(Research research, List<Field> fields)
        // {
        //     var researchFieldList = BuildResearchFieldList(research, fields);
        //
        //     _researchFieldRepository.AddRange(researchFieldList);
        // }
        //
        // private void RemoveAllFields(Research research, List<Field> fields)
        // {
        //     var researchFieldList = BuildResearchFieldList(research, fields);
        //
        //     _researchFieldRepository.RemoveRange(researchFieldList);
        // }
        //
        // private List<ResearchField> BuildResearchFieldList(Research research, List<Field> fields)
        // {
        //     return fields.Select(f =>
        //         new ResearchField
        //         {
        //             Research = research,
        //             Field = f
        //         }
        //     ).ToList();
        // }
        //
        // private async Task<List<Tag>> GetTagsAsync(int id)
        // {
        //     var researchTag= await _researchTagRepository.ToListAsync();
        //     var tagsIds = researchTag.Where(rt => rt.ResearchId.Equals(id)).Select(rt => rt.TagId);
        //     var tags = tagsIds.Select(id => _tagService.FindOneAsync(id).Result);
        //
        //     return tags.ToList();
        // }
        //
        // private void SaveAllTags(Research research, List<Tag> tags)
        // {
        //     var researchTagList = BuildResearchTagList(research, tags);
        //
        //     _researchTagRepository.AddRange(researchTagList);
        // }
        //
        // private void RemoveAllTags(Research research, List<Tag> tags)
        // {
        //     var researchTagList = BuildResearchTagList(research, tags);
        //
        //     _researchTagRepository.RemoveRange(researchTagList);
        // }
        //
        // private List<ResearchTag> BuildResearchTagList(Research research, List<Tag> tags)
        // {
        //     return tags.Select(t =>
        //         new ResearchTag
        //         {
        //             Research = research,
        //             Tag = t
        //         }
        //     ).ToList();
        // }
        // private async Task<List<Author>> GetAuthorsAsync(int id)
        // {
        //     var researchAuthor = await _researchAuthorRepository.ToListAsync();
        //     var authorsIds = researchAuthor.Where(ra => ra.ResearchId.Equals(id)).Select(ra => ra.AuthorId);
        //     var authors = authorsIds.Select(id => _authorService.FindOneAsync(id).Result);
        //
        //     return authors.ToList();
        // }
        //
        // private void SaveAllAuthors(Research research, List<Author> authors)
        // {
        //     var researchAuthorList = BuildResearchAuthorList(research, authors);
        //
        //     _researchAuthorRepository.AddRange(researchAuthorList);
        // }
        //
        // private void RemoveAllAuthors(Research research, List<Author> authors)
        // {
        //     var researchAuthorList = BuildResearchAuthorList(research, authors);
        //
        //     _researchAuthorRepository.RemoveRange(researchAuthorList);
        // }
        //
        // private List<ResearchAuthor> BuildResearchAuthorList(Research research, List<Author> authors)
        // {
        //     return authors.Select(a =>
        //         new ResearchAuthor
        //         {
        //             Research = research,
        //             Author = a
        //         }
        //     ).ToList();
        // }

        // private async Task<ResearchResponseDTO> BuildResearchResponseDTO(Research research)
        // {
        //     if (research != null)
        //     {
        //         var fields = await GetFieldsAsync(research.Id);
        //         var tags = await GetTagsAsync(research.Id);
        //         var authors = await GetAuthorsAsync(research.Id);
        //
        //         var researchDTO = _mapper.Map<ResearchResponseDTO>(research);
        //         var fieldsDTO = _mapper.Map<List<FieldResponseDTO>>(fields);
        //         var tagsDTO = _mapper.Map<List<TagResponseDTO>>(tags);
        //         var authorsDTO = _mapper.Map<List<AuthorResponseDTO>>(authors);
        //
        //         researchDTO.Fields = fieldsDTO;
        //         researchDTO.Tags = tagsDTO;
        //         researchDTO.Authors = authorsDTO;
        //
        //         return researchDTO;
        //     }
        //
        //     return null;
        // }
    }
}
