using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeamsLektion3.Migrations
{
    /// <inheritdoc />
    public partial class AddSuperPower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "SuperHeroes");

            migrationBuilder.CreateTable(
                name: "SuperPowers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PowerLevel = table.Column<int>(type: "integer", nullable: false),
                    SuperHeroId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperPowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuperPowers_SuperHeroes_SuperHeroId",
                        column: x => x.SuperHeroId,
                        principalTable: "SuperHeroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuperPowers_SuperHeroId",
                table: "SuperPowers",
                column: "SuperHeroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperPowers");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "SuperHeroes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
