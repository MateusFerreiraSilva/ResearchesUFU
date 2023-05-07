using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Services.Interfaces;

namespace ResearchesUFU.API.Services
{
    public class AuthorService : BaseService<Author>, IAuthorService
    {
        public AuthorService(ResearchesUFUContext dbContext)
        {
            DbContext = dbContext;
            Repository = DbContext.Authors;
        }
    }
}