using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Services.Interfaces;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services
{
    public class ResearchService : IResearchService
    {
        private readonly DbSet<Research>? _researches;

        public ResearchService(ResearchesUFUContext context)
        {
            _researches = context.Researches;
        }

        public HttpResponseBase<Research> Get(int id)
        {
            try
            {
                var queryResult = _researches?.FirstOrDefault(r => r.Id.Equals(id));
                var response = HttpUtils<Research>.GenerateHttpResponse(queryResult);

                return response;
            }
            catch (Exception ex)
            {
                return HttpUtils<Research>.GenerateHttpErrorResponse();
            }
        } 
    }
}
