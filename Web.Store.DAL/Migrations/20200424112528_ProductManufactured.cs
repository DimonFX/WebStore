using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Store.DAL.Migrations
{
    public partial class ProductManufactured : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Manufactured",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manufactured",
                table: "Products");
        }
    }
}
