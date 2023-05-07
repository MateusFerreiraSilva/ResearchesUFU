using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Services.Interfaces;

namespace ResearchesUFU.API.Services
{
    public class FieldService : BaseService<Field>, IFieldService
    {
        public FieldService(ResearchesUFUContext dbContext, IResearchService researchService)
        {
            DbContext = dbContext;
            Repository = DbContext.Fields;
        }
    }
}