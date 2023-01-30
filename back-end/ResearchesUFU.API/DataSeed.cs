using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Models;

namespace ResearchesUFU.API
{
    public static class DataSeed
    {
        const string LOREM_IPSUM = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ultricies dictum varius. Donec ultrices nibh eget tellus aliquet tincidunt. Proin fringilla leo quis nibh sollicitudin hendrerit. Nullam et massa non felis vestibulum ornare at eget velit. Maecenas tincidunt commodo tellus, quis vehicula massa pharetra a. Suspendisse eu fermentum nulla. Nam tempor velit vitae commodo interdum. Mauris feugiat euismod semper. Nam porta vitae eros eget lobortis. Pellentesque sed neque sit amet sapien vestibulum consectetur. In at neque in lectus pulvinar ultrices. Mauris condimentum gravida metus nec pretium. Quisque a venenatis neque. Aenean quis tellus elementum, laoreet tortor in, vehicula lacus. Integer tellus massa, lacinia sit amet molestie vel, convallis a ante.\nNulla euismod justo ut rhoncus mattis. Sed quam quam, scelerisque sagittis nibh in, rutrum lacinia odio. Suspendisse gravida leo in elit elementum, at placerat arcu consequat. Integer justo dui, gravida sit amet ipsum non, vehicula fringilla nisl. Integer pretium vulputate purus, non efficitur tortor vestibulum nec. Etiam posuere nunc ipsum, vel venenatis ipsum vehicula eget. Mauris vulputate nibh at blandit euismod. Nulla laoreet feugiat efficitur. Suspendisse sit amet vulputate turpis, vitae tristique metus. Integer et condimentum arcu. Nulla efficitur dolor tortor, vitae sodales lectus rhoncus id. Etiam mollis porta urna sed tempus.";
        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedResearch(modelBuilder);
            SeedField(modelBuilder);
            SeedTag(modelBuilder);
            SeedUser(modelBuilder);
            SeedAuthor(modelBuilder);
            
            SeedResearchField(modelBuilder);
            SeedResearchTag(modelBuilder);
            SeedResearchAuthor(modelBuilder);
        }

        private static void SeedResearch(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Research>().HasData(
                new Research
                {
                    Id = 1,
                    Title = "Inteligência artificial para análise de investimentos.",
                    Summary = LOREM_IPSUM,
                    Status = Constants.RESEARCH_STATUS_ONGOING,
                    PublicationDate = "02/02/2022",
                    Thumbnail = "https://raw.githubusercontent.com/MateusFerreiraSilva/ResearchesUFU/master/back-end/ResearchesUFU.API/Images/image01.png"
                },
                new Research
                {
                    Id = 2,
                    Title = "Estudo de técnicas de Cloud Computing",
                    Summary = LOREM_IPSUM,
                    Status = Constants.RESEARCH_STATUS_FINISHED,
                    PublicationDate = "19/09/2019",
                    Thumbnail = "https://raw.githubusercontent.com/MateusFerreiraSilva/ResearchesUFU/master/back-end/ResearchesUFU.API/Images/image02.jpg"
                },
                new Research
                {
                    Id = 3,
                    Title = "Os beneficios do café",
                    Summary = LOREM_IPSUM,
                    Status = Constants.RESEARCH_STATUS_CANCELED,
                    PublicationDate = "01/01/2011",
                    Thumbnail = "https://raw.githubusercontent.com/MateusFerreiraSilva/ResearchesUFU/master/back-end/ResearchesUFU.API/Images/image03.jpg"
                },
                new Research
                {
                    Id = 4,
                    Title = "UFU...",
                    Summary = LOREM_IPSUM,
                    Status = Constants.RESEARCH_STATUS_FINISHED,
                    PublicationDate = "012/08/2018",
                    Thumbnail = "https://raw.githubusercontent.com/MateusFerreiraSilva/ResearchesUFU/master/back-end/ResearchesUFU.API/Images/image04.jpg"
                },
                new Research
                {
                    Id = 5,
                    Title = "DNA...",
                    Summary = LOREM_IPSUM,
                    Status = Constants.RESEARCH_STATUS_ONGOING,
                    PublicationDate = "20/03/2021",
                    Thumbnail = "https://raw.githubusercontent.com/MateusFerreiraSilva/ResearchesUFU/master/back-end/ResearchesUFU.API/Images/image05.png"
                },
                new Research
                {
                    Id = 6,
                    Title = "Artes...",
                    Summary = LOREM_IPSUM,
                    Status = Constants.RESEARCH_STATUS_ONGOING,
                    PublicationDate = "09/12/2016",
                    Thumbnail = "https://raw.githubusercontent.com/MateusFerreiraSilva/ResearchesUFU/master/back-end/ResearchesUFU.API/Images/image06.jpg"
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
                }, new Field
                {
                    Id = 4,
                    Name = "Sociologia",
                    Acronym = "S"
                },
                new Field
                {
                    Id = 5,
                    Name = "Artes",
                    Acronym = "Art"
                }
            );
        }

        private static void SeedTag(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = 1,
                    Name = "artificial intelligence",
                },
                new Tag
                {
                    Id = 2,
                    Name = "ai",
                },
                new Tag
                {
                    Id = 3,
                    Name = "cloud computing",
                },
                new Tag
                {
                    Id = 4,
                    Name = "computer science",
                },
                new Tag
                {
                    Id = 5,
                    Name = "biochemistry",
                },
                new Tag
                {
                    Id = 6,
                    Name = "coffe",
                },
                new Tag
                {
                    Id = 8,
                    Name = "ufu",
                },
                new Tag
                {
                    Id = 9,
                    Name = "dna",
                },
                new Tag
                {
                    Id = 10,
                    Name = "artes",
                },
                new Tag
                {
                    Id = 11,
                    Name = "pintura a óleo",
                },
                new Tag
                {
                    Id = 12,
                    Name = "estudos sociais",
                },
                new Tag
                {
                    Id = 13,
                    Name = "sociologia",
                }
            );
        }

        private static void SeedUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "teste@ufu.br",
                    // sha256 for "1234"
                    PasswordHash = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4",
                    AuthorId = 1,
                },
                new User
                {
                    Id = 2,
                    Email = "batata@ufu.br",
                    // sha256 for "batata"
                    PasswordHash = "f4610aa514477222afac2b77f971d069780ca2846f375849f3dfa3c0047ebbd1",
                    AuthorId = 2
                },
                new User
                {
                    Id = 3,
                    Email = "teste2@ufu.br",
                    // sha256 for "1234"
                    PasswordHash = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4",
                    AuthorId = 3,
                },
                new User
                {
                    Id = 4,
                    Email = "batata2@ufu.br",
                    // sha256 for "batata"
                    PasswordHash = "f4610aa514477222afac2b77f971d069780ca2846f375849f3dfa3c0047ebbd1",
                    AuthorId = 4
                },
                new User
                {
                    Id = 5,
                    Email = "teste3@ufu.br",
                    // sha256 for "1234"
                    PasswordHash = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4",
                    AuthorId = 5,
                }
            );
        }

        private static void SeedAuthor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    UserType = Constants.USER_TYPE_PUBLICATOR,
                    Name = "Elisabete Sintra Barateiro",
                    Birthdate = "20/07/1990",
                    PhoneNumber = "553498153388",
                    ProfilePictureUrl = "",
                    Bio = LOREM_IPSUM,
                    UserId = 1,
                },
                new Author
                {
                    Id = 2,
                    UserType = Constants.USER_TYPE_EDITOR,
                    Name = "Dominic Frota Godoi",
                    Birthdate = "05/05/1999",
                    PhoneNumber = "553498851389",
                    ProfilePictureUrl = "",
                    Bio = LOREM_IPSUM,
                    UserId = 2
                },
                new Author
                {
                    Id = 3,
                    UserType = Constants.USER_TYPE_PUBLICATOR,
                    Name = "Ricardo Guedes Neres",
                    Birthdate = "21/07/1993",
                    PhoneNumber = "553498153388",
                    ProfilePictureUrl = "",
                    Bio = LOREM_IPSUM,
                    UserId = 3,
                },
                new Author
                {
                    Id = 4,
                    UserType = Constants.USER_TYPE_EDITOR,
                    Name = "Cristiano Araújo Távora",
                    Birthdate = "04/12/2001",
                    PhoneNumber = "553498851389",
                    ProfilePictureUrl = "",
                    Bio = LOREM_IPSUM,
                    UserId = 4
                },
                new Author
                {
                    Id = 5,
                    UserType = Constants.USER_TYPE_EDITOR,
                    Name = "Eric Cadaval Ornelas",
                    Birthdate = "01/06/2000",
                    PhoneNumber = "553498851389",
                    ProfilePictureUrl = "",
                    Bio = LOREM_IPSUM,
                    UserId = 5
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
                    ResearchId = 1,
                    FieldId = 2,
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
                },
                new ResearchField
                {
                    ResearchId = 4,
                    FieldId = 4,
                },
                new ResearchField
                {
                    ResearchId = 5,
                    FieldId = 3,
                },
                new ResearchField
                {
                    ResearchId = 6,
                    FieldId = 5,
                }
            );
        }

        private static void SeedResearchTag(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResearchTag>().HasData(
                new ResearchTag
                {
                    ResearchId = 1,
                    TagId = 1
                },
                new ResearchTag
                {
                    ResearchId = 1,
                    TagId = 2
                },
                new ResearchTag
                {
                    ResearchId = 1,
                    TagId = 4
                },
                new ResearchTag
                {
                    ResearchId = 2,
                    TagId = 3
                },
                new ResearchTag
                {
                    ResearchId = 3,
                    TagId = 5
                },
                new ResearchTag
                {
                    ResearchId = 3,
                    TagId = 6
                },
                new ResearchTag
                {
                    ResearchId = 4,
                    TagId = 12
                },
                new ResearchTag
                {
                    ResearchId = 4,
                    TagId = 13
                },
                new ResearchTag
                {
                    ResearchId = 5,
                    TagId = 5
                },
                new ResearchTag
                {
                    ResearchId = 5,
                    TagId = 9
                },
                new ResearchTag
                {
                    ResearchId = 6,
                    TagId = 10
                },
                new ResearchTag
                {
                    ResearchId = 6,
                    TagId = 11
                }
            );
        }

        private static void SeedResearchAuthor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResearchAuthor>().HasData(
                new ResearchAuthor
                {
                    ResearchId = 1,
                    AuthorId = 1,
                },
                new ResearchAuthor
                {
                    ResearchId = 1,
                    AuthorId = 2,
                },
                new ResearchAuthor
                {
                    ResearchId = 2,
                    AuthorId = 1,
                },
                new ResearchAuthor
                {
                    ResearchId = 3,
                    AuthorId = 3,
                },
                new ResearchAuthor
                {
                    ResearchId = 4,
                    AuthorId = 4,
                },
                new ResearchAuthor
                {
                    ResearchId = 5,
                    AuthorId = 3,
                },
                new ResearchAuthor
                {
                    ResearchId = 6,
                    AuthorId = 5,
                }
            );
        }
    }
}
