using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajjSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeSeasonTitleUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Seasons_Title",
                table: "Seasons",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Seasons_Title",
                table: "Seasons");
        }
    }
}
