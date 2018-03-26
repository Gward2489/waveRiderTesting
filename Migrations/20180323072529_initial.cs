using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace waveRiderTester.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beach",
                columns: table => new
                {
                    BeachId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BeachName = table.Column<string>(nullable: false),
                    BreakType = table.Column<string>(nullable: false),
                    Latitude = table.Column<string>(nullable: false),
                    Longtitude = table.Column<string>(nullable: false),
                    Region = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beach", x => x.BeachId);
                });

            migrationBuilder.CreateTable(
                name: "Buoy",
                columns: table => new
                {
                    BuoyId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Latitude = table.Column<string>(nullable: false),
                    Longtitude = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    NbdcId = table.Column<string>(nullable: false),
                    Owner = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buoy", x => x.BuoyId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beach");

            migrationBuilder.DropTable(
                name: "Buoy");
        }
    }
}
