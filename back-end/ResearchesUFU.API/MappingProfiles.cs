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
            CreateMap<ResearchRequestDTO, Research>();
            CreateMap<FieldRequestDTO, Field>();
            CreateMap<Research, ResearchResponseDTO>();
            CreateMap<Field, FieldResponseDTO>();
            CreateMap<Tag, TagResponseDTO>();
            CreateMap<Author, AuthorResponseDTO>();
        }
    }
}
