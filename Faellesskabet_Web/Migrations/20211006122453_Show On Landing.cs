using Microsoft.EntityFrameworkCore.Migrations;

namespace Faellesskabet_Web.Migrations
{
    public partial class ShowOnLanding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowOnLanding",
                table: "Nyheder",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowOnLanding",
                table: "Nyheder");
        }
    }
}
