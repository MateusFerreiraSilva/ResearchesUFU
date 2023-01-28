using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Models;

namespace ResearchesUFU.API.Context
{
    public class ResearchesUFUContext : DbContext
    {
        public DbSet<Research> Researches => Set<Research>();

        public DbSet<Field> Fields => Set<Field>();

        public DbSet<User> Users => Set<User>();

        public DbSet<Author> Authors => Set<Author>();

        public DbSet<ResearchField> ResearchField => Set<ResearchField>();

        public ResearchesUFUContext(DbContextOptions<ResearchesUFUContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            OneTooneRelationshipConfiguration(modelBuilder);
            ManyToManyRelationshipConfiguration(modelBuilder);

            DataSeed.Seed(modelBuilder);
        }

        private void OneTooneRelationshipConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Author)
                .WithOne(a => a.User)
                .HasForeignKey<Author>(a => a.UserId);
        }

        private void ManyToManyRelationshipConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResearchField>()
                .HasKey(rf => new { rf.ResearchId, rf.FieldId });
        }
    }
}
