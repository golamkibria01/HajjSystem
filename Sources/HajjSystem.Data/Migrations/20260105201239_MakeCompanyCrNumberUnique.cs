using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajjSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeCompanyCrNumberUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Companies_CrNumber",
                table: "Companies",
                column: "CrNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Companies_CrNumber",
                table: "Companies");
        }
    }
}
