using Microsoft.EntityFrameworkCore.Migrations;

namespace Faellesskabet_Web.Migrations
{
    public partial class BestyrelseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Bestyrelsen",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Bestyrelsen");
        }
    }
}
