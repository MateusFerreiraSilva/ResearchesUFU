using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Repositories.Interfaces;

namespace ResearchesUFU.API.Repositories;

public class ResearchFieldRepository : BaseRepository<ResearchField>
{
    public ResearchFieldRepository(ResearchesUFUContext dbContext, DbSet<ResearchField> dbSet) : base(dbContext, dbSet)
    {
    }
}