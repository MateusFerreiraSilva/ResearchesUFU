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

        public DbSet<Tag> Tags => Set<Tag>();

        public DbSet<ResearchField> ResearchField => Set<ResearchField>();

        public DbSet<ResearchTag> ResearchTag => Set<ResearchTag>();

        public DbSet<ResearchAuthor> ResearchAuthor => Set<ResearchAuthor>();


        public ResearchesUFUContext(DbContextOptions<ResearchesUFUContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            OneToOneRelationshipConfiguration(modelBuilder);
            ManyToManyRelationshipConfiguration(modelBuilder);

            DataSeed.Seed(modelBuilder);
        }

        private void OneToOneRelationshipConfiguration(ModelBuilder modelBuilder)
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

            modelBuilder.Entity<ResearchAuthor>()
                .HasKey(ra => new { ra.ResearchId, ra.AuthorId });

            modelBuilder.Entity<ResearchTag>()
                .HasKey(rt => new { rt.ResearchId, rt.TagId });
        }
    }
}
