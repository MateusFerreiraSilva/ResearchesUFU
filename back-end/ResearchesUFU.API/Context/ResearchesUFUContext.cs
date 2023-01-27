using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Models;

namespace ResearchesUFU.API.Context
{
    public class ResearchesUFUContext : DbContext
    {
        //public DbSet<User> Users => Set<User>();

        public DbSet<Research> Researches => Set<Research>();

        public DbSet<Field> Fields => Set<Field>();

        public DbSet<ResearchField> ResearchField => Set<ResearchField>();

        public ResearchesUFUContext(DbContextOptions<ResearchesUFUContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            ManyToManyRelationshipConfiguration(modelBuilder);
        }

        private void ManyToManyRelationshipConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResearchField>()
                .HasKey(rf => new { rf.ResearchId, rf.FieldId });
        }
    }
}
