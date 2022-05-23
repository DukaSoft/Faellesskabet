using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Faellesskabet_Web.Migrations
{
    public partial class ReferatChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Referarter",
                table: "Referarter");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Referarter");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Referarter");

            migrationBuilder.RenameTable(
                name: "Referarter",
                newName: "Referater");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Regnskaber",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Referater",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "Referater",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Referater",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Referater",
                table: "Referater",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Referater",
                table: "Referater");

            migrationBuilder.DropColumn(
                name: "File",
                table: "Referater");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Referater");

            migrationBuilder.RenameTable(
                name: "Referater",
                newName: "Referarter");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Regnskaber",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Referarter",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Referarter",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Referarter",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Referarter",
                table: "Referarter",
                column: "Id");
        }
    }
}
