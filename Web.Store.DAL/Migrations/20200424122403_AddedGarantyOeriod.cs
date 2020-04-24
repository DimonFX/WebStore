using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Store.DAL.Migrations
{
    public partial class AddedGarantyOeriod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manufactured",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "countDaysGaranty",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "countDaysGaranty",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Manufactured",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
