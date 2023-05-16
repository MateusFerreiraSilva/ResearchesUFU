using ResearchesUFU.API.Models;
using ResearchesUFU.API.Models.DTO.Requests;
using ResearchesUFU.API.Models.DTO.Responses;
using ResearchesUFU.API.Repositories.Interfaces;
using ResearchesUFU.API.Services.Interfaces;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IBaseRepository<User> userRepository) : base(userRepository)
        {
        }

        public async Task<HttpResponseBase<UserAuthenticationResponseDTO>> AuthenticateUserAsync(UserAuthenticationRequestDTO userAuthenticationRequest)
        {
            var method = async delegate()
            {
                var email = userAuthenticationRequest.Email;
                var passwordHash = userAuthenticationRequest.PasswordHash;

                if (!ValidationsUtils.isValidEmail(email) ||
                    !ValidationsUtils.isValidPasswordHash(passwordHash)
                   )
                {
                    return HttpUtils<UserAuthenticationResponseDTO>.GenerateHttpBadRequestResponse();
                }

                var user = (await GetAllAsync()).FirstOrDefault(u => u.Email.Equals(email));

                if (user == null)
                {
                    return HttpUtils<UserAuthenticationResponseDTO>.GenerateHttpResponse(StatusCodes.Status404NotFound);
                }

                if (user.PasswordHash.Equals(passwordHash))
                {
                    var successResponse = new UserAuthenticationResponseDTO
                    {
                        UserId = user.Id,
                        Token = Guid.NewGuid().ToString() // random GUID :)
                    };
                    return HttpUtils<UserAuthenticationResponseDTO>.GenerateHttpSuccessResponse(successResponse);
                }

                var failureResponse = new UserAuthenticationResponseDTO
                {
                    UserId = 0,
                    Token = string.Empty
                };

                return HttpUtils<UserAuthenticationResponseDTO>.GenerateHttpBadRequestResponse(failureResponse);
            };

            var response = await ExecuteMethodAsync(method);

            return response;
        }
    }
}
