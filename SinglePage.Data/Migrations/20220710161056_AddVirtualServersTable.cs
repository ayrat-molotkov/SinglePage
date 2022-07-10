using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinglePage.Data.Migrations
{
    public partial class AddVirtualServersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VirtualServers",
                columns: table => new
                {
                    VirtualServerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemoveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualServers", x => x.VirtualServerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VirtualServers");
        }
    }
}
