using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_DILesson.Migrations
{
    /// <inheritdoc />
    public partial class AddYearAndRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Films",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Films",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Films");
        }
    }
}
