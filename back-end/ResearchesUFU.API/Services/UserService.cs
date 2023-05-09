using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Models.DTO.Requests;
using ResearchesUFU.API.Models.DTO.Responses;
using ResearchesUFU.API.Services.Interfaces;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services
{
    // public class UserService : BaseService, IUserService
    // {
    //     public async Task<HttpResponseBase<UserAuthenticationResponseDTO>> AuthenticateUserAsync(UserAuthenticationRequestDTO userAuthenticationRequest)
    //     {
    //         try
    //         {
    //             var email = userAuthenticationRequest.Email;
    //             var passwordHash = userAuthenticationRequest.PasswordHash;
    //
    //             if (!ValidationsUtils.isValidEmail(email) ||
    //                 !ValidationsUtils.isValidPasswordHash(passwordHash)
    //             )
    //             {
    //                 return HttpUtils<UserAuthenticationResponseDTO>.GenerateHttpBadRequestResponse(null);
    //             }
    //
    //             var allUsers = await FindAllAsync();
    //             var user = allUsers.Where(u => u.Email.Equals(email)).FirstOrDefault();
    //
    //             if (user == null)
    //             {
    //                 return HttpUtils<UserAuthenticationResponseDTO>.GenerateHttpSuccessResponse();
    //             }
    //
    //             if (user.PasswordHash.Equals(passwordHash))
    //             {
    //                 var successResponse = new UserAuthenticationResponseDTO
    //                 {
    //                     UserId = user.Id,
    //                     Token = Guid.NewGuid().ToString() // random GUID :)
    //                 };
    //                 return HttpUtils<UserAuthenticationResponseDTO>.GenerateHttpSuccessResponse(successResponse);
    //             }
    //
    //             var failureResponse = new UserAuthenticationResponseDTO
    //             {
    //                 UserId = 0,
    //                 Token = string.Empty
    //             };
    //
    //             return HttpUtils<UserAuthenticationResponseDTO>.GenerateHttpBadRequestResponse(failureResponse);
    //         }
    //         catch
    //         {
    //             return HttpUtils<UserAuthenticationResponseDTO>.GenerateHttpErrorResponse();
    //         }
    //     }
    // }
}
