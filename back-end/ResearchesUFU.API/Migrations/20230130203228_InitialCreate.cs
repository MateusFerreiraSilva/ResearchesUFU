using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ResearchesUFU.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Acronym = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Researches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Summary = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    PublicationDate = table.Column<string>(type: "text", nullable: false),
                    Thumbnail = table.Column<string>(type: "text", nullable: false),
                    ResearchStructure = table.Column<string>(type: "text", nullable: false),
                    LastUpdated = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Researches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LastUpdated = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    LastUpdated = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResearchField",
                columns: table => new
                {
                    ResearchId = table.Column<int>(type: "integer", nullable: false),
                    FieldId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchField", x => new { x.ResearchId, x.FieldId });
                    table.ForeignKey(
                        name: "FK_ResearchField_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResearchField_Researches_ResearchId",
                        column: x => x.ResearchId,
                        principalTable: "Researches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearchTag",
                columns: table => new
                {
                    ResearchId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchTag", x => new { x.ResearchId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ResearchTag_Researches_ResearchId",
                        column: x => x.ResearchId,
                        principalTable: "Researches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResearchTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserType = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Birthdate = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "text", nullable: false),
                    CoverPhotoUrl = table.Column<string>(type: "text", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LastUpdated = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearchAuthor",
                columns: table => new
                {
                    ResearchId = table.Column<int>(type: "integer", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchAuthor", x => new { x.ResearchId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_ResearchAuthor_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResearchAuthor_Researches_ResearchId",
                        column: x => x.ResearchId,
                        principalTable: "Researches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Fields",
                columns: new[] { "Id", "Acronym", "Name" },
                values: new object[,]
                {
                    { 1, "SI", "Sistemas de Informação" },
                    { 2, "CC", "Ciência da Computação" },
                    { 3, "BIO", "Biologia" },
                    { 4, "S", "Sociologia" },
                    { 5, "Art", "Artes" }
                });

            migrationBuilder.InsertData(
                table: "Researches",
                columns: new[] { "Id", "LastUpdated", "PublicationDate", "ResearchStructure", "Status", "Summary", "Thumbnail", "Title" },
                values: new object[,]
                {
                    { 1, "30/01/2023 20:32:27", "02/02/2022", "", "Em andamento", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ultricies dictum varius. Donec ultrices nibh eget tellus aliquet tincidunt. Proin fringilla leo quis nibh sollicitudin hendrerit. Nullam et massa non felis vestibulum ornare at eget velit. Maecenas tincidunt commodo tellus, quis vehicula massa pharetra a. Suspendisse eu fermentum nulla. Nam tempor velit vitae commodo interdum. Mauris feugiat euismod semper. Nam porta vitae eros eget lobortis. Pellentesque sed neque sit amet sapien vestibulum consectetur. In at neque in lectus pulvinar ultrices. Mauris condimentum gravida metus nec pretium. Quisque a venenatis neque. Aenean quis tellus elementum, laoreet tortor in, vehicula lacus. Integer tellus massa, lacinia sit amet molestie vel, convallis a ante.\nNulla euismod justo ut rhoncus mattis. Sed quam quam, scelerisque sagittis nibh in, rutrum lacinia odio. Suspendisse gravida leo in elit elementum, at placerat arcu consequat. Integer justo dui, gravida sit amet ipsum non, vehicula fringilla nisl. Integer pretium vulputate purus, non efficitur tortor vestibulum nec. Etiam posuere nunc ipsum, vel venenatis ipsum vehicula eget. Mauris vulputate nibh at blandit euismod. Nulla laoreet feugiat efficitur. Suspendisse sit amet vulputate turpis, vitae tristique metus. Integer et condimentum arcu. Nulla efficitur dolor tortor, vitae sodales lectus rhoncus id. Etiam mollis porta urna sed tempus.", "https://raw.githubusercontent.com/MateusFerreiraSilva/ResearchesUFU/master/back-end/ResearchesUFU.API/Images/image01.png", "Inteligência artificial para análise de investimentos." },
                    { 2, "30/01/2023 20:32:27", "19/09/2019", "", "Finalizada", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ultricies dictum varius. Donec ultrices nibh eget tellus aliquet tincidunt. Proin fringilla leo quis nibh sollicitudin hendrerit. Nullam et massa non felis vestibulum ornare at eget velit. Maecenas tincidunt commodo tellus, quis vehicula massa pharetra a. Suspendisse eu fermentum nulla. Nam tempor velit vitae commodo interdum. Mauris feugiat euismod semper. Nam porta vitae eros eget lobortis. Pellentesque sed neque sit amet sapien vestibulum consectetur. In at neque in lectus pulvinar ultrices. Mauris condimentum gravida metus nec pretium. Quisque a venenatis neque. Aenean quis tellus elementum, laoreet tortor in, vehicula lacus. Integer tellus massa, lacinia sit amet molestie vel, convallis a ante.\nNulla euismod justo ut rhoncus mattis. Sed quam quam, scelerisque sagittis nibh in, rutrum lacinia odio. Suspendisse gravida leo in elit elementum, at placerat arcu consequat. Integer justo dui, gravida sit amet ipsum non, vehicula fringilla nisl. Integer pretium vulputate purus, non efficitur tortor vestibulum nec. Etiam posuere nunc ipsum, vel venenatis ipsum vehicula eget. Mauris vulputate nibh at blandit euismod. Nulla laoreet feugiat efficitur. Suspendisse sit amet vulputate turpis, vitae tristique metus. Integer et condimentum arcu. Nulla efficitur dolor tortor, vitae sodales lectus rhoncus id. Etiam mollis porta urna sed tempus.", "https://raw.githubusercontent.com/MateusFerreiraSilva/ResearchesUFU/master/back-end/ResearchesUFU.API/Images/image02.jpg", "Estudo de técnicas de Cloud Computing" },
                    { 3, "30/01/2023 20:32:27", "01/01/2011", "", "Cancelada", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ultricies dictum varius. Donec ultrices nibh eget tellus aliquet tincidunt. Proin fringilla leo quis nibh sollicitudin hendrerit. Nullam et massa non felis vestibulum ornare at eget velit. Maecenas tincidunt commodo tellus, quis vehicula massa pharetra a. Suspendisse eu fermentum nulla. Nam tempor velit vitae commodo interdum. Mauris feugiat euismod semper. Nam porta vitae eros eget lobortis. Pellentesque sed neque sit amet sapien vestibulum consectetur. In at neque in lectus pulvinar ultrices. Mauris condimentum gravida metus nec pretium. Quisque a venenatis neque. Aenean quis tellus elementum, laoreet tortor in, vehicula lacus. Integer tellus massa, lacinia sit amet molestie vel, convallis a ante.\nNulla euismod justo ut rhoncus mattis. Sed quam quam, scelerisque sagittis nibh in, rutrum lacinia odio. Suspendisse gravida leo in elit elementum, at placerat arcu consequat. Integer justo dui, gravida sit amet ipsum non, vehicula fringilla nisl. Integer pretium vulputate purus, non efficitur tortor vestibulum nec. Etiam posuere nunc ipsum, vel venenatis ipsum vehicula eget. Mauris vulputate nibh at blandit euismod. Nulla laoreet feugiat efficitur. Suspendisse sit amet vulputate turpis, vitae tristique metus. Integer et condimentum arcu. Nulla efficitur dolor tortor, vitae sodales lectus rhoncus id. Etiam mollis porta urna sed tempus.", "https://raw.githubusercontent.com/MateusFerreiraSilva/ResearchesUFU/master/back-end/ResearchesUFU.API/Images/image03.jpg", "Os beneficios do café" },
                    { 4, "30/01/2023 20:32:27", "012/08/2018", "", "Finalizada", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ultricies dictum varius. Donec ultrices nibh eget tellus aliquet tincidunt. Proin fringilla leo quis nibh sollicitudin hendrerit. Nullam et massa non felis vestibulum ornare at eget velit. Maecenas tincidunt commodo tellus, quis vehicula massa pharetra a. Suspendisse eu fermentum nulla. Nam tempor velit vitae commodo interdum. Mauris feugiat euismod semper. Nam porta vitae eros eget lobortis. Pellentesque sed neque sit amet sapien vestibulum consectetur. In at neque in lectus pulvinar ultrices. Mauris condimentum gravida metus nec pretium. Quisque a venenatis neque. Aenean quis tellus elementum, laoreet tortor in, vehicula lacus. Integer tellus massa, lacinia sit amet molestie vel, convallis a ante.\nNulla euismod justo ut rhoncus mattis. Sed quam quam, scelerisque sagittis nibh in, rutrum lacinia odio. Suspendisse gravida leo in elit elementum, at placerat arcu consequat. Integer justo dui, gravida sit amet ipsum non, vehicula fringilla nisl. Integer pretium vulputate purus, non efficitur tortor vestibulum nec. Etiam posuere nunc ipsum, vel venenatis ipsum vehicula eget. Mauris vulputate nibh at blandit euismod. Nulla laoreet feugiat efficitur. Suspendisse sit amet vulputate turpis, vitae tristique metus. Integer et condimentum arcu. Nulla efficitur dolor tortor, vitae sodales lectus rhoncus id. Etiam mollis porta urna sed tempus.", "https://raw.githubusercontent.com/MateusFerreiraSilva/ResearchesUFU/master/back-end/ResearchesUFU.API/Images/image04.jpg", "UFU..." },
                    { 5, "30/01/2023 20:32:27", "20/03/2021", "", "Em andamento", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ultricies dictum varius. Donec ultrices nibh eget tellus aliquet tincidunt. Proin fringilla leo quis nibh sollicitudin hendrerit. Nullam et massa non felis vestibulum ornare at eget velit. Maecenas tincidunt commodo tellus, quis vehicula massa pharetra a. Suspendisse eu fermentum nulla. Nam tempor velit vitae commodo interdum. Mauris feugiat euismod semper. Nam porta vitae eros eget lobortis. Pellentesque sed neque sit amet sapien vestibulum consectetur. In at neque in lectus pulvinar ultrices. Mauris condimentum gravida metus nec pretium. Quisque a venenatis neque. Aenean quis tellus elementum, laoreet tortor in, vehicula lacus. Integer tellus massa, lacinia sit amet molestie vel, convallis a ante.\nNulla euismod justo ut rhoncus mattis. Sed quam quam, scelerisque sagittis nibh in, rutrum lacinia odio. Suspendisse gravida leo in elit elementum, at placerat arcu consequat. Integer justo dui, gravida sit amet ipsum non, vehicula fringilla nisl. Integer pretium vulputate purus, non efficitur tortor vestibulum nec. Etiam posuere nunc ipsum, vel venenatis ipsum vehicula eget. Mauris vulputate nibh at blandit euismod. Nulla laoreet feugiat efficitur. Suspendisse sit amet vulputate turpis, vitae tristique metus. Integer et condimentum arcu. Nulla efficitur dolor tortor, vitae sodales lectus rhoncus id. Etiam mollis porta urna sed tempus.", "https://raw.githubusercontent.com/MateusFerreiraSilva/ResearchesUFU/master/back-end/ResearchesUFU.API/Images/image05.png", "DNA..." },
                    { 6, "30/01/2023 20:32:27", "09/12/2016", "", "Em andamento", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ultricies dictum varius. Donec ultrices nibh eget tellus aliquet tincidunt. Proin fringilla leo quis nibh sollicitudin hendrerit. Nullam et massa non felis vestibulum ornare at eget velit. Maecenas tincidunt commodo tellus, quis vehicula massa pharetra a. Suspendisse eu fermentum nulla. Nam tempor velit vitae commodo interdum. Mauris feugiat euismod semper. Nam porta vitae eros eget lobortis. Pellentesque sed neque sit amet sapien vestibulum consectetur. In at neque in lectus pulvinar ultrices. Mauris condimentum gravida metus nec pretium. Quisque a venenatis neque. Aenean quis tellus elementum, laoreet tortor in, vehicula lacus. Integer tellus massa, lacinia sit amet molestie vel, convallis a ante.\nNulla euismod justo ut rhoncus mattis. Sed quam quam, scelerisque sagittis nibh in, rutrum lacinia odio. Suspendisse gravida leo in elit elementum, at placerat arcu consequat. Integer justo dui, gravida sit amet ipsum non, vehicula fringilla nisl. Integer pretium vulputate purus, non efficitur tortor vestibulum nec. Etiam posuere nunc ipsum, vel venenatis ipsum vehicula eget. Mauris vulputate nibh at blandit euismod. Nulla laoreet feugiat efficitur. Suspendisse sit amet vulputate turpis, vitae tristique metus. Integer et condimentum arcu. Nulla efficitur dolor tortor, vitae sodales lectus rhoncus id. Etiam mollis porta urna sed tempus.", "https://raw.githubusercontent.com/MateusFerreiraSilva/ResearchesUFU/master/back-end/ResearchesUFU.API/Images/image06.jpg", "Artes..." }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "LastUpdated", "Name" },
                values: new object[,]
                {
                    { 1, "30/01/2023 20:32:27", "artificial intelligence" },
                    { 2, "30/01/2023 20:32:27", "ai" },
                    { 3, "30/01/2023 20:32:27", "cloud computing" },
                    { 4, "30/01/2023 20:32:27", "computer science" },
                    { 5, "30/01/2023 20:32:27", "biochemistry" },
                    { 6, "30/01/2023 20:32:27", "coffe" },
                    { 8, "30/01/2023 20:32:27", "ufu" },
                    { 9, "30/01/2023 20:32:27", "dna" },
                    { 10, "30/01/2023 20:32:27", "artes" },
                    { 11, "30/01/2023 20:32:27", "Pintura a Óleo" },
                    { 12, "30/01/2023 20:32:27", "Estudos sociais" },
                    { 13, "30/01/2023 20:32:27", "Sociologia" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthorId", "Email", "LastUpdated", "PasswordHash" },
                values: new object[,]
                {
                    { 1, 1, "teste@ufu.br", "30/01/2023 20:32:27", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4" },
                    { 2, 2, "batata@ufu.br", "30/01/2023 20:32:27", "f4610aa514477222afac2b77f971d069780ca2846f375849f3dfa3c0047ebbd1" },
                    { 3, 3, "teste2@ufu.br", "30/01/2023 20:32:27", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4" },
                    { 4, 4, "batata2@ufu.br", "30/01/2023 20:32:27", "f4610aa514477222afac2b77f971d069780ca2846f375849f3dfa3c0047ebbd1" },
                    { 5, 5, "teste3@ufu.br", "30/01/2023 20:32:27", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4" }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Bio", "Birthdate", "CoverPhotoUrl", "LastUpdated", "Name", "PhoneNumber", "ProfilePictureUrl", "UserId", "UserType" },
                values: new object[,]
                {
                    { 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ultricies dictum varius. Donec ultrices nibh eget tellus aliquet tincidunt. Proin fringilla leo quis nibh sollicitudin hendrerit. Nullam et massa non felis vestibulum ornare at eget velit. Maecenas tincidunt commodo tellus, quis vehicula massa pharetra a. Suspendisse eu fermentum nulla. Nam tempor velit vitae commodo interdum. Mauris feugiat euismod semper. Nam porta vitae eros eget lobortis. Pellentesque sed neque sit amet sapien vestibulum consectetur. In at neque in lectus pulvinar ultrices. Mauris condimentum gravida metus nec pretium. Quisque a venenatis neque. Aenean quis tellus elementum, laoreet tortor in, vehicula lacus. Integer tellus massa, lacinia sit amet molestie vel, convallis a ante.\nNulla euismod justo ut rhoncus mattis. Sed quam quam, scelerisque sagittis nibh in, rutrum lacinia odio. Suspendisse gravida leo in elit elementum, at placerat arcu consequat. Integer justo dui, gravida sit amet ipsum non, vehicula fringilla nisl. Integer pretium vulputate purus, non efficitur tortor vestibulum nec. Etiam posuere nunc ipsum, vel venenatis ipsum vehicula eget. Mauris vulputate nibh at blandit euismod. Nulla laoreet feugiat efficitur. Suspendisse sit amet vulputate turpis, vitae tristique metus. Integer et condimentum arcu. Nulla efficitur dolor tortor, vitae sodales lectus rhoncus id. Etiam mollis porta urna sed tempus.", "20/07/1990", "", "30/01/2023 20:32:27", "Elisabete Sintra Barateiro", "553498153388", "", 1, "Publicator" },
                    { 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ultricies dictum varius. Donec ultrices nibh eget tellus aliquet tincidunt. Proin fringilla leo quis nibh sollicitudin hendrerit. Nullam et massa non felis vestibulum ornare at eget velit. Maecenas tincidunt commodo tellus, quis vehicula massa pharetra a. Suspendisse eu fermentum nulla. Nam tempor velit vitae commodo interdum. Mauris feugiat euismod semper. Nam porta vitae eros eget lobortis. Pellentesque sed neque sit amet sapien vestibulum consectetur. In at neque in lectus pulvinar ultrices. Mauris condimentum gravida metus nec pretium. Quisque a venenatis neque. Aenean quis tellus elementum, laoreet tortor in, vehicula lacus. Integer tellus massa, lacinia sit amet molestie vel, convallis a ante.\nNulla euismod justo ut rhoncus mattis. Sed quam quam, scelerisque sagittis nibh in, rutrum lacinia odio. Suspendisse gravida leo in elit elementum, at placerat arcu consequat. Integer justo dui, gravida sit amet ipsum non, vehicula fringilla nisl. Integer pretium vulputate purus, non efficitur tortor vestibulum nec. Etiam posuere nunc ipsum, vel venenatis ipsum vehicula eget. Mauris vulputate nibh at blandit euismod. Nulla laoreet feugiat efficitur. Suspendisse sit amet vulputate turpis, vitae tristique metus. Integer et condimentum arcu. Nulla efficitur dolor tortor, vitae sodales lectus rhoncus id. Etiam mollis porta urna sed tempus.", "05/05/1999", "", "30/01/2023 20:32:27", "Dominic Frota Godoi", "553498851389", "", 2, "Editor" },
                    { 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ultricies dictum varius. Donec ultrices nibh eget tellus aliquet tincidunt. Proin fringilla leo quis nibh sollicitudin hendrerit. Nullam et massa non felis vestibulum ornare at eget velit. Maecenas tincidunt commodo tellus, quis vehicula massa pharetra a. Suspendisse eu fermentum nulla. Nam tempor velit vitae commodo interdum. Mauris feugiat euismod semper. Nam porta vitae eros eget lobortis. Pellentesque sed neque sit amet sapien vestibulum consectetur. In at neque in lectus pulvinar ultrices. Mauris condimentum gravida metus nec pretium. Quisque a venenatis neque. Aenean quis tellus elementum, laoreet tortor in, vehicula lacus. Integer tellus massa, lacinia sit amet molestie vel, convallis a ante.\nNulla euismod justo ut rhoncus mattis. Sed quam quam, scelerisque sagittis nibh in, rutrum lacinia odio. Suspendisse gravida leo in elit elementum, at placerat arcu consequat. Integer justo dui, gravida sit amet ipsum non, vehicula fringilla nisl. Integer pretium vulputate purus, non efficitur tortor vestibulum nec. Etiam posuere nunc ipsum, vel venenatis ipsum vehicula eget. Mauris vulputate nibh at blandit euismod. Nulla laoreet feugiat efficitur. Suspendisse sit amet vulputate turpis, vitae tristique metus. Integer et condimentum arcu. Nulla efficitur dolor tortor, vitae sodales lectus rhoncus id. Etiam mollis porta urna sed tempus.", "21/07/1993", "", "30/01/2023 20:32:27", "Ricardo Guedes Neres", "553498153388", "", 3, "Publicator" },
                    { 4, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ultricies dictum varius. Donec ultrices nibh eget tellus aliquet tincidunt. Proin fringilla leo quis nibh sollicitudin hendrerit. Nullam et massa non felis vestibulum ornare at eget velit. Maecenas tincidunt commodo tellus, quis vehicula massa pharetra a. Suspendisse eu fermentum nulla. Nam tempor velit vitae commodo interdum. Mauris feugiat euismod semper. Nam porta vitae eros eget lobortis. Pellentesque sed neque sit amet sapien vestibulum consectetur. In at neque in lectus pulvinar ultrices. Mauris condimentum gravida metus nec pretium. Quisque a venenatis neque. Aenean quis tellus elementum, laoreet tortor in, vehicula lacus. Integer tellus massa, lacinia sit amet molestie vel, convallis a ante.\nNulla euismod justo ut rhoncus mattis. Sed quam quam, scelerisque sagittis nibh in, rutrum lacinia odio. Suspendisse gravida leo in elit elementum, at placerat arcu consequat. Integer justo dui, gravida sit amet ipsum non, vehicula fringilla nisl. Integer pretium vulputate purus, non efficitur tortor vestibulum nec. Etiam posuere nunc ipsum, vel venenatis ipsum vehicula eget. Mauris vulputate nibh at blandit euismod. Nulla laoreet feugiat efficitur. Suspendisse sit amet vulputate turpis, vitae tristique metus. Integer et condimentum arcu. Nulla efficitur dolor tortor, vitae sodales lectus rhoncus id. Etiam mollis porta urna sed tempus.", "04/12/2001", "", "30/01/2023 20:32:27", "Cristiano Araújo Távora", "553498851389", "", 4, "Editor" },
                    { 5, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ultricies dictum varius. Donec ultrices nibh eget tellus aliquet tincidunt. Proin fringilla leo quis nibh sollicitudin hendrerit. Nullam et massa non felis vestibulum ornare at eget velit. Maecenas tincidunt commodo tellus, quis vehicula massa pharetra a. Suspendisse eu fermentum nulla. Nam tempor velit vitae commodo interdum. Mauris feugiat euismod semper. Nam porta vitae eros eget lobortis. Pellentesque sed neque sit amet sapien vestibulum consectetur. In at neque in lectus pulvinar ultrices. Mauris condimentum gravida metus nec pretium. Quisque a venenatis neque. Aenean quis tellus elementum, laoreet tortor in, vehicula lacus. Integer tellus massa, lacinia sit amet molestie vel, convallis a ante.\nNulla euismod justo ut rhoncus mattis. Sed quam quam, scelerisque sagittis nibh in, rutrum lacinia odio. Suspendisse gravida leo in elit elementum, at placerat arcu consequat. Integer justo dui, gravida sit amet ipsum non, vehicula fringilla nisl. Integer pretium vulputate purus, non efficitur tortor vestibulum nec. Etiam posuere nunc ipsum, vel venenatis ipsum vehicula eget. Mauris vulputate nibh at blandit euismod. Nulla laoreet feugiat efficitur. Suspendisse sit amet vulputate turpis, vitae tristique metus. Integer et condimentum arcu. Nulla efficitur dolor tortor, vitae sodales lectus rhoncus id. Etiam mollis porta urna sed tempus.", "01/06/2000", "", "30/01/2023 20:32:27", "Eric Cadaval Ornelas", "553498851389", "", 5, "Editor" }
                });

            migrationBuilder.InsertData(
                table: "ResearchField",
                columns: new[] { "FieldId", "ResearchId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 3, 5 },
                    { 5, 6 }
                });

            migrationBuilder.InsertData(
                table: "ResearchTag",
                columns: new[] { "ResearchId", "TagId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 4 },
                    { 2, 3 },
                    { 3, 5 },
                    { 3, 6 },
                    { 4, 12 },
                    { 4, 13 },
                    { 5, 5 },
                    { 5, 9 },
                    { 6, 10 },
                    { 6, 11 }
                });

            migrationBuilder.InsertData(
                table: "ResearchAuthor",
                columns: new[] { "AuthorId", "ResearchId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 1, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 3, 5 },
                    { 5, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_UserId",
                table: "Authors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResearchAuthor_AuthorId",
                table: "ResearchAuthor",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchField_FieldId",
                table: "ResearchField",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchTag_TagId",
                table: "ResearchTag",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResearchAuthor");

            migrationBuilder.DropTable(
                name: "ResearchField");

            migrationBuilder.DropTable(
                name: "ResearchTag");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Researches");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
