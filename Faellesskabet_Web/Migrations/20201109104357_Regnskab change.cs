using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Faellesskabet_Web.Migrations
{
    public partial class Regnskabchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Regnskaber");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "Regnskaber",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Regnskaber");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Regnskaber",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
