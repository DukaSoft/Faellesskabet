using Microsoft.EntityFrameworkCore.Migrations;

namespace Faellesskabet_Web.Migrations
{
    public partial class AddedTicketammountandcostinfotothearrangement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketPriceAdult",
                table: "Arrangementer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketPriceChild",
                table: "Arrangementer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketTotalAdult",
                table: "Arrangementer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketTotalChild",
                table: "Arrangementer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketPriceAdult",
                table: "Arrangementer");

            migrationBuilder.DropColumn(
                name: "TicketPriceChild",
                table: "Arrangementer");

            migrationBuilder.DropColumn(
                name: "TicketTotalAdult",
                table: "Arrangementer");

            migrationBuilder.DropColumn(
                name: "TicketTotalChild",
                table: "Arrangementer");
        }
    }
}
