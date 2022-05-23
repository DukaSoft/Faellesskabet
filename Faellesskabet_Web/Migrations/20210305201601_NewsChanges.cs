using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Faellesskabet_Web.Migrations
{
    public partial class NewsChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Nyheder",
                newName: "TextContent");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Nyheder",
                newName: "PublishDate");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Nyheder",
                newName: "ImageContent");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "Nyheder",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "Nyheder");

            migrationBuilder.RenameColumn(
                name: "TextContent",
                table: "Nyheder",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "PublishDate",
                table: "Nyheder",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "ImageContent",
                table: "Nyheder",
                newName: "Content");
        }
    }
}
