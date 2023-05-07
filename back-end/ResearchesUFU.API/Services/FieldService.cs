using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Services.Interfaces;

namespace ResearchesUFU.API.Services
{
    public class FieldService : BaseService<Field>, IFieldService
    {
        public FieldService(ResearchesUFUContext dbContext)
        {
            DbContext = dbContext;
            Repository = DbContext.Fields;
        }
    }
}