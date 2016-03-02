using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace cwitkpi.Migrations
{
    public partial class Initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    username = table.Column<string>(nullable: false),
                    clan = table.Column<string>(nullable: true),
                    honor = table.Column<int>(nullable: false),
                    vkLink = table.Column<string>(nullable: true),
                    weeklyPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.username);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("UserInfo");
        }
    }
}
