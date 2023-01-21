using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Services.Interfaces;

namespace ResearchesUFU.API.Services
{
    public class ResearchService : IResearchService
    {
        private readonly DbSet<Research> _researches;

        public ResearchService(ResearchesUFUContext context)
        {
            _researches = context.Researches;
        }

        public Research Get(int id)
        {
            var queryResult = _researches.FirstOrDefault(r => r.Id.Equals(id));

            return queryResult;
        } 
    }
}
