using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Models;

namespace ResearchesUFU.API
{
    public static class DataSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedResearch(modelBuilder);
            SeedField(modelBuilder);
            SeedResearchField(modelBuilder);
        }

        private static void SeedResearch(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Research>().HasData(
                new Research
                {
                    Id = 1,
                    Title = "Primeira Pesquisa",
                    Summary = "AAAAA",
                    Status = Constants.RESEARCH_STATUS_ONGOING,
                    PublicationDate = "02/02/2022",
                    Thumbnail = "https://cdn.vox-cdn.com/thumbor/WR9hE8wvdM4hfHysXitls9_bCZI=/0x0:1192x795/1400x1400/filters:focal(596x398:597x399)/cdn.vox-cdn.com/uploads/chorus_asset/file/22312759/rickroll_4k.jpg",
                },
                new Research
                {
                    Id = 2,
                    Title = "Segunda Pesquisa",
                    Summary = "BBBB",
                    Status = Constants.RESEARCH_STATUS_FINISHED,
                    PublicationDate = "19/09/2019",
                    Thumbnail = "https://cdn.vox-cdn.com/thumbor/WR9hE8wvdM4hfHysXitls9_bCZI=/0x0:1192x795/1400x1400/filters:focal(596x398:597x399)/cdn.vox-cdn.com/uploads/chorus_asset/file/22312759/rickroll_4k.jpg",
                },
                new Research
                {
                    Id = 3,
                    Title = "Batata",
                    Summary = "Potato",
                    Status = Constants.RESEARCH_STATUS_CANCELED,
                    PublicationDate = "01/01/2011",
                    Thumbnail = "https://static.mundoeducacao.uol.com.br/mundoeducacao/conteudo_legenda/01325ea5fd7fd4ecab7e209393bf6188.jpg",
                }
           );
        }

        private static void SeedField(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Field>().HasData(
                new Field
                {
                    Id = 1,
                    Name = "Sistemas de Informação",
                    Acronym = "SI"
                },
                new Field
                {
                    Id = 2,
                    Name = "Ciência da Computação",
                    Acronym = "CC"
                },
                new Field
                {
                    Id = 3,
                    Name = "Biologia",
                    Acronym = "BIO"
                }
            );
        }

        private static void SeedResearchField(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResearchField>().HasData(
                new ResearchField
                {
                    ResearchId = 1,
                    FieldId = 1,
                },
                new ResearchField
                {
                    ResearchId = 2,
                    FieldId = 1,
                },
                new ResearchField
                {
                    ResearchId = 2,
                    FieldId = 2,
                },
                new ResearchField
                {
                    ResearchId = 3,
                    FieldId = 3,
                }
            );
        }
    }
}
