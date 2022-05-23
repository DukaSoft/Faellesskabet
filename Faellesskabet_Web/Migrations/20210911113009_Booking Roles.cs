using Microsoft.EntityFrameworkCore.Migrations;

namespace Faellesskabet_Web.Migrations
{
    public partial class BookingRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"SET IDENTITY_INSERT [dbo].[AspNetRoles] ON
INSERT INTO[dbo].[AspNetRoles]([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES(7, N'BookingSystemManagerCreateEdit', N'BOOKINGSYSTEMMANAGERCREATEEDIT', N'47ba814d-523b-4356-82ed-cf3d5358bc88')
INSERT INTO[dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES(8, N'BookingSystemManagerDelete', N'BOOKINGSYSTEMMANAGERDELETE', N'80814e1c-5cfb-4c47-acac-8973066d42cd')
SET IDENTITY_INSERT[dbo].[AspNetRoles] OFF
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
