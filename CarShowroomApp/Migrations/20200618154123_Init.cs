using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShowroomApp.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Engine = table.Column<string>(nullable: true),
                    Power = table.Column<int>(nullable: false),
                    Production = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Mileage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
