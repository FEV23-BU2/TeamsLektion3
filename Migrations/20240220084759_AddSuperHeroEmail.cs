using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamsLektion3.Migrations
{
    /// <inheritdoc />
    public partial class AddSuperHeroEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "SuperHeroes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "SuperHeroes");
        }
    }
}
