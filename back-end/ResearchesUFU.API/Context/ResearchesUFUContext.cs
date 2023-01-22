using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Models;

namespace ResearchesUFU.API.Context
{
    public class ResearchesUFUContext : DbContext
    {
        public ResearchesUFUContext(DbContextOptions<ResearchesUFUContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Research> Researches => Set<Research>();

        public DbSet<Author> Authors => Set<Author>();
    }
}
