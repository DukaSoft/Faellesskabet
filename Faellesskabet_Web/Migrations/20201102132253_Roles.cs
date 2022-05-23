using Microsoft.EntityFrameworkCore.Migrations;

namespace Faellesskabet_Web.Migrations
{
    public partial class Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"SET IDENTITY_INSERT [dbo].[AspNetRoles] ON
INSERT INTO[dbo].[AspNetRoles]([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES(1, N'Admin', N'ADMIN', N'2e4a688b-49bc-4675-8731-84393a2fdb5f')
INSERT INTO[dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES(2, N'EventManager', N'EVENTMANAGER', N'fa479a20-1eb5-4727-a666-37c1aa4022bf')
INSERT INTO[dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES(3, N'BestyrelsesManager', N'BESTYRELSESMANAGER', N'ae9407e3-e8dc-4cec-ae01-73a869790f6b')
INSERT INTO[dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES(4, N'RegnskabsManager', N'REGNSKABSMANAGER', N'26684973-b4ab-47b7-a2a0-ed717bae45c1')
INSERT INTO[dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES(5, N'ReferatsManager', N'REFERATSMANAGER', N'093a197a-7c7f-4c63-b138-4e49729fab49')
SET IDENTITY_INSERT[dbo].[AspNetRoles] OFF
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
