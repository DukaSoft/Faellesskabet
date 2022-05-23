using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Faellesskabet_Web.Migrations
{
    public partial class ReferatyearchangedtoDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Referater");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Referater",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Referater");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Referater",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
