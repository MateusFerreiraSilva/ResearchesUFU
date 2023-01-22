using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Models;

namespace ResearchesUFU.API.Context
{
    public class ResearchesUFUContext : DbContext
    {
        public ResearchesUFUContext(DbContextOptions<ResearchesUFUContext> options) : base(options)
        {
        }

        public DbSet<User>? Users { get; set; }

        public DbSet<Research>? Researches { get; set; }

        public DbSet<Author>? Authors { get; set; }
    }
}
