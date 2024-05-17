using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOANCOSO26.Migrations
{
    /// <inheritdoc />
    public partial class addColumnCopanyToBus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Buses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Buses");
        }
    }
}
