using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Services.Interfaces;

namespace ResearchesUFU.API.Services
{
    public class TagService : BaseService<Tag>, ITagService
    {
        public TagService(ResearchesUFUContext dbContext)
        {
            DbContext = dbContext;
            Repository = DbContext.Tags;
        }
    }
}
