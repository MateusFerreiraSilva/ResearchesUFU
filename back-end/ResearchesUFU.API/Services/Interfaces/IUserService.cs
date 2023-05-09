using ResearchesUFU.API.Models;
using ResearchesUFU.API.Models.DTO.Requests;
using ResearchesUFU.API.Models.DTO.Responses;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services.Interfaces
{
    public interface IUserService
    {
        public Task<HttpResponseBase<UserAuthenticationResponseDTO>> AuthenticateUserAsync(UserAuthenticationRequestDTO userAuthenticationRequest);
    }
}
