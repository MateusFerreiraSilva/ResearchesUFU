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
            // CreateMap<ResearchRequestDTO, Research>();
            // CreateMap<FieldRequestDTO, Field>();
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

            CreateMap<ResearchField, ResearchFieldResponseDTO>();
            CreateMap<ResearchTag, ResearchTagResponseDto>();
            CreateMap<ResearchAuthor, ResearchAuthorResponseDto>();

        }
    }
}
