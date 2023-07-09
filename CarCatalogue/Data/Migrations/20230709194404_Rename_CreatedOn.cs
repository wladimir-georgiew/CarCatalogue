using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarCatalogue.Data.Migrations
{
    /// <inheritdoc />
    public partial class Rename_CreatedOn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Cars",
                newName: "CreatedOrModifiedOn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOrModifiedOn",
                table: "Cars",
                newName: "CreatedOn");
        }
    }
}
