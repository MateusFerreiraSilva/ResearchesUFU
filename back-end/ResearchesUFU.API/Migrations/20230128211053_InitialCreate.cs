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

            migrationBuilder.InsertData(
                table: "Fields",
                columns: new[] { "Id", "Acronym", "Name" },
                values: new object[,]
                {
                    { 1, "SI", "Sistemas de Informação" },
                    { 2, "CC", "Ciência da Computação" },
                    { 3, "BIO", "Biologia" }
                });

            migrationBuilder.InsertData(
                table: "Researches",
                columns: new[] { "Id", "LastUpdated", "PublicationDate", "ResearchStructure", "Status", "Summary", "Thumbnail", "Title" },
                values: new object[,]
                {
                    { 1, "28/01/2023 21:10:53", "02/02/2022", "", "Em andamento", "AAAAA", "https://cdn.vox-cdn.com/thumbor/WR9hE8wvdM4hfHysXitls9_bCZI=/0x0:1192x795/1400x1400/filters:focal(596x398:597x399)/cdn.vox-cdn.com/uploads/chorus_asset/file/22312759/rickroll_4k.jpg", "Primeira Pesquisa" },
                    { 2, "28/01/2023 21:10:53", "19/09/2019", "", "Finalizada", "BBBB", "https://cdn.vox-cdn.com/thumbor/WR9hE8wvdM4hfHysXitls9_bCZI=/0x0:1192x795/1400x1400/filters:focal(596x398:597x399)/cdn.vox-cdn.com/uploads/chorus_asset/file/22312759/rickroll_4k.jpg", "Segunda Pesquisa" },
                    { 3, "28/01/2023 21:10:53", "01/01/2011", "", "Cancelada", "Potato", "https://static.mundoeducacao.uol.com.br/mundoeducacao/conteudo_legenda/01325ea5fd7fd4ecab7e209393bf6188.jpg", "Batata" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthorId", "Email", "LastUpdated", "PasswordHash" },
                values: new object[,]
                {
                    { 1, 1, "johndoe@ufu.br", "28/01/2023 21:10:53", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4" },
                    { 2, 2, "jannedoe@ufu.br", "28/01/2023 21:10:53", "f4610aa514477222afac2b77f971d069780ca2846f375849f3dfa3c0047ebbd1" }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Bio", "Birthdate", "CoverPhotoUrl", "LastUpdated", "Name", "PhoneNumber", "ProfilePictureUrl", "UserId", "UserType" },
                values: new object[,]
                {
                    { 1, "blablablablabla", "20/07/1990", "", "28/01/2023 21:10:53", "saul goodman", "553498153388", "https://i.kym-cdn.com/entries/icons/original/000/040/009/3dsaulcover.jpg", 1, "Publicator" },
                    { 2, ".........................", "01/05/2001", "", "28/01/2023 21:10:53", "Janne Doe", "553498851389", "https://hiperideal.vteximg.com.br/arquivos/ids/167660/27502.jpg?v=636615816147030000", 2, "Publicator" }
                });

            migrationBuilder.InsertData(
                table: "ResearchField",
                columns: new[] { "FieldId", "ResearchId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_UserId",
                table: "Authors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResearchField_FieldId",
                table: "ResearchField",
                column: "FieldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "ResearchField");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Researches");
        }
    }
}
