using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class suatiepdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContestEntries_VideoId",
                table: "ContestEntries");

            migrationBuilder.CreateIndex(
                name: "IX_ContestEntries_VideoId",
                table: "ContestEntries",
                column: "VideoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContestEntries_VideoId",
                table: "ContestEntries");

            migrationBuilder.CreateIndex(
                name: "IX_ContestEntries_VideoId",
                table: "ContestEntries",
                column: "VideoId");
        }
    }
}
