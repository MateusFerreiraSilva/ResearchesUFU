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

        public async Task<HttpResponseBase<UserAuthenticationResponseDto>> AuthenticateUserAsync(UserAuthenticationRequestDto userAuthenticationRequest)
        {
            var method = async delegate()
            {
                var email = userAuthenticationRequest.Email;
                var passwordHash = userAuthenticationRequest.PasswordHash;

                if (!ValidationsUtils.isValidEmail(email) ||
                    !ValidationsUtils.isValidPasswordHash(passwordHash)
                   )
                {
                    return HttpUtils<UserAuthenticationResponseDto>.GenerateHttpBadRequestResponse();
                }

                var user = (await GetAllAsync()).FirstOrDefault(u => u.Email.Equals(email));

                if (user == null)
                {
                    return HttpUtils<UserAuthenticationResponseDto>.GenerateHttpResponse(StatusCodes.Status404NotFound);
                }

                if (user.PasswordHash.Equals(passwordHash))
                {
                    var successResponse = new UserAuthenticationResponseDto
                    {
                        UserId = user.Id,
                        Token = Guid.NewGuid().ToString() // random GUID :)
                    };
                    return HttpUtils<UserAuthenticationResponseDto>.GenerateHttpSuccessResponse(successResponse);
                }

                var failureResponse = new UserAuthenticationResponseDto
                {
                    UserId = 0,
                    Token = string.Empty
                };

                return HttpUtils<UserAuthenticationResponseDto>.GenerateHttpBadRequestResponse(failureResponse);
            };

            var response = await ExecuteMethodAsync(method);

            return response;
        }
    }
}
