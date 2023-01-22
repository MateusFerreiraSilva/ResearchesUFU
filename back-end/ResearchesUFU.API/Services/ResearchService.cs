using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Services.Interfaces;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Services
{
    public class ResearchService : IResearchService
    {
        private readonly ResearchesUFUContext _context;
        private readonly DbSet<Research> _researches;

        public ResearchService(ResearchesUFUContext context)
        {
            _context = context;
            _researches = _context.Researches;
        }

        public HttpResponseBase<Research> Get(int id)
        {
            try
            {
                var queryResult = _researches.FirstOrDefault(r => r.Id.Equals(id));

                var response = HttpUtils<Research>.GenerateHttpResponse(queryResult);

                return response;
            }
            catch (Exception ex)
            {
                return HttpUtils<Research>.GenerateHttpErrorResponse();
            }
        }

        public HttpResponseBase<IQueryable<Research>> GetAll()
        {
            try
            {
                var queryResult = _researches.AsQueryable();
                var response = HttpUtils<IQueryable<Research>>.GenerateHttpResponse(queryResult);

                return response;
            }
            catch (Exception ex)
            {
                return HttpUtils<IQueryable<Research>>.GenerateHttpErrorResponse();
            }
        }

        // TO DO olhar lance do StoreGeneratedPattern para pegar o id
        public HttpResponseBase<IdResponse> Post(Research research)
        {
            var response = new IdResponse();
            try
            {
                _researches.Add(research);
                _context.SaveChanges();

                _researches.Entry(research).GetDatabaseValues(); // refresh

                response.Id = research.Id;

                return HttpUtils<IdResponse>.GenerateHttpResponse(response);
            }
            catch (Exception ex)
            {
                return HttpUtils<IdResponse>.GenerateHttpErrorResponse();
            }
        }
    }
}
