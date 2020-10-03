using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShowroom.Infra.Data.Migrations
{
    public partial class AddSent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Sent",
                table: "Messages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sent",
                table: "Messages");
        }
    }
}
