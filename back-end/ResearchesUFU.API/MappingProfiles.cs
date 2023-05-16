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
            CreateMap<Research, ResearchResponseDTO>()
                .ForMember(
                    researchResponseDTO => researchResponseDTO.Fields,
                    research => research.MapFrom(r => r.ResearchField))
                .ForMember(
                    researchResponseDTO => researchResponseDTO.Tags,
                    research => research.MapFrom(r => r.ResearchTag))
                .ForMember(
                    researchResponseDTO => researchResponseDTO.Authors,
                    research => research.MapFrom(r => r.ResearchAuthor));
            
            CreateMap<Field, FieldResponseDTO>();
            CreateMap<Tag, TagResponseDTO>();
            CreateMap<Author, AuthorResponseDTO>();
            #endregion
            
            #region Many to Many response Dtos
            CreateMap<ResearchField, ResearchFieldResponseDTO>();
            CreateMap<ResearchTag, ResearchTagResponseDto>();
            CreateMap<ResearchAuthor, ResearchAuthorResponseDto>();
            #endregion

            #region request Dtos
            CreateMap<ResearchRequestDTO, Research>()
                .ForMember(
                    research => research.ResearchField,
                    researchRequestDTO => researchRequestDTO.MapFrom(r => r.Fields))
                .ForMember(
                    research => research.ResearchTag,
                    researchRequestDTO => researchRequestDTO.MapFrom(r => r.Tags))
                .ForMember(
                    research => research.ResearchAuthor,
                    researchRequestDTO => researchRequestDTO.MapFrom(r => r.Authors));
            
            CreateMap<FieldRequestDTO, Field>();
            CreateMap<TagRequestDTO, Tag>();
            CreateMap<AuthorRequestDTO, Author>();
            #endregion
            
            #region Many to Many request Dtos
            CreateMap<ResearchFieldRequestDto, ResearchField>();
            CreateMap<ResearchTagRequestDto, ResearchTag>();
            CreateMap<ResearchAuthorRequestDto, ResearchAuthor>();
            #endregion
        }
    }
}
