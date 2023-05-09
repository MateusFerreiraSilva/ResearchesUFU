using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Repositories.Interfaces;

namespace ResearchesUFU.API.Repositories;

public class ResearchTagRepository : BaseRepository<ResearchTag>
{
    public ResearchTagRepository(ResearchesUFUContext dbContext, DbSet<ResearchTag> dbSet) : base(dbContext, dbSet)
    {
    }
}