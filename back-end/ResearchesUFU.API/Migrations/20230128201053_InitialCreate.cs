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
                    LastUpdated = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Researches", x => x.Id);
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
                columns: new[] { "Id", "LastUpdated", "PublicationDate", "Status", "Summary", "Thumbnail", "Title" },
                values: new object[,]
                {
                    { 1, "28/01/2023 20:10:53", "02/02/2022", "Em andamento", "AAAAA", "https://cdn.vox-cdn.com/thumbor/WR9hE8wvdM4hfHysXitls9_bCZI=/0x0:1192x795/1400x1400/filters:focal(596x398:597x399)/cdn.vox-cdn.com/uploads/chorus_asset/file/22312759/rickroll_4k.jpg", "Primeira Pesquisa" },
                    { 2, "28/01/2023 20:10:53", "19/09/2019", "Finalizada", "BBBB", "https://cdn.vox-cdn.com/thumbor/WR9hE8wvdM4hfHysXitls9_bCZI=/0x0:1192x795/1400x1400/filters:focal(596x398:597x399)/cdn.vox-cdn.com/uploads/chorus_asset/file/22312759/rickroll_4k.jpg", "Segunda Pesquisa" },
                    { 3, "28/01/2023 20:10:53", "01/01/2011", "Cancelada", "Potato", "https://static.mundoeducacao.uol.com.br/mundoeducacao/conteudo_legenda/01325ea5fd7fd4ecab7e209393bf6188.jpg", "Batata" }
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
                name: "IX_ResearchField_FieldId",
                table: "ResearchField",
                column: "FieldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResearchField");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Researches");
        }
    }
}
