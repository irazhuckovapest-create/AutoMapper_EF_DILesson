using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_DILesson.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieFieldsAndComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Films",
                newName: "ReleaseYear");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Films",
                newName: "PosterUrl");

            migrationBuilder.AddColumn<string>(
                name: "MovieUrl",
                table: "Films",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Films_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MovieId",
                table: "Comments",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropColumn(
                name: "MovieUrl",
                table: "Films");

            migrationBuilder.RenameColumn(
                name: "ReleaseYear",
                table: "Films",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "PosterUrl",
                table: "Films",
                newName: "ImageUrl");
        }
    }
}
