using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajjSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeasonStatusToIsCurrent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Seasons",
                newName: "isCurrent");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Companies",
                newName: "CompanyName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isCurrent",
                table: "Seasons",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "Companies",
                newName: "Name");
        }
    }
}
