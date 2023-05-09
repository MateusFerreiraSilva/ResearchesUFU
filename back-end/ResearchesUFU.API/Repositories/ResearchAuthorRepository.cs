using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Repositories.Interfaces;

namespace ResearchesUFU.API.Repositories;

public class ResearchAuthorRepository : BaseRepository<ResearchAuthor>
{
    public ResearchAuthorRepository(ResearchesUFUContext dbContext, DbSet<ResearchAuthor> dbSet) : base(dbContext, dbSet)
    {
    }
}