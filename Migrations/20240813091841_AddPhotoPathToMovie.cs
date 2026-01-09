using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2Screen.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoPathToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photopath",
                table: "movies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photopath",
                table: "movies");
        }
    }
}
