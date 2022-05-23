using Microsoft.EntityFrameworkCore.Migrations;

namespace Faellesskabet_Web.Migrations
{
    public partial class NewsManagerRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"SET IDENTITY_INSERT [dbo].[AspNetRoles] ON
INSERT INTO[dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES(6, N'NyhedsManager', N'NYHEDSMANAGER', N'87b51921-feac-4dee-befc-8ccac8dc2642')
SET IDENTITY_INSERT[dbo].[AspNetRoles] OFF
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
