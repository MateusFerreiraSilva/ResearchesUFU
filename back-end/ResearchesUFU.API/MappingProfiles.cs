using AutoMapper;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Models.DTO.Requests;
using ResearchesUFU.API.Models.DTO.Responses;

namespace ResearchesUFU.API
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region response Dtos
            CreateMap<Research, ResearchResponseDto>()
                .ForMember(
                    researchResponseDto => researchResponseDto.Fields,
                    research => research.MapFrom(r => r.ResearchField))
                .ForMember(
                    researchResponseDto => researchResponseDto.Tags,
                    research => research.MapFrom(r => r.ResearchTag))
                .ForMember(
                    researchResponseDto => researchResponseDto.Authors,
                    research => research.MapFrom(r => r.ResearchAuthor));
            
            CreateMap<Field, FieldResponseDto>();
            CreateMap<Tag, TagResponseDto>();
            CreateMap<Author, AuthorResponseDto>();
            #endregion
            
            #region Many to Many response Dtos
            CreateMap<ResearchField, ResearchFieldResponseDto>();
            CreateMap<ResearchTag, ResearchTagResponseDto>();
            CreateMap<ResearchAuthor, ResearchAuthorResponseDto>();
            #endregion

            #region request Dtos
            CreateMap<ResearchRequestDto, Research>()
                .ForMember(
                    research => research.ResearchField,
                    researchRequestDto => researchRequestDto.MapFrom(r => r.Fields))
                .ForMember(
                    research => research.ResearchTag,
                    researchRequestDto => researchRequestDto.MapFrom(r => r.Tags))
                .ForMember(
                    research => research.ResearchAuthor,
                    researchRequestDto => researchRequestDto.MapFrom(r => r.Authors));
            
            CreateMap<FieldRequestDto, Field>();
            CreateMap<TagRequestDto, Tag>();
            CreateMap<AuthorRequestDto, Author>();
            #endregion
            
            #region Many to Many request Dtos
            CreateMap<ResearchFieldRequestDto, ResearchField>();
            CreateMap<ResearchTagRequestDto, ResearchTag>();
            CreateMap<ResearchAuthorRequestDto, ResearchAuthor>();
            #endregion
        }
    }
}
