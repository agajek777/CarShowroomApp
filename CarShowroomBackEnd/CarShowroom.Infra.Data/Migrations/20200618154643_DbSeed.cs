using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShowroom.Infra.Data.Migrations
{
    public partial class DbSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "Description", "Engine", "ImagePath", "Mileage", "Model", "Power", "Price", "Production" },
                values: new object[] { 1, "Audi", "Autko w super stanie! Garażowane! Od fanatyka!", "V10 5.2", "https://apollo-ireland.akamaized.net/v1/files/eyJmbiI6IjV1Z3FkOTBqbzBqNzEtT1RPTU9UT1BMIiwidyI6W3siZm4iOiJ3ZzRnbnFwNnkxZi1PVE9NT1RPUEwiLCJzIjoiMTYiLCJwIjoiMTAsLTEwIiwiYSI6IjAifV19.RWMwGPIAdM3kRNxqXNsJhlDtpAK4npaZ2LUk4TheVJE/image;s=1080x720;cars_;/937787999_;slot=10;filename=eyJmbiI6IjV1Z3FkOTBqbzBqNzEtT1RPTU9UT1BMIiwidyI6W3siZm4iOiJ3ZzRnbnFwNnkxZi1PVE9NT1RPUEwiLCJzIjoiMTYiLCJwIjoiMTAsLTEwIiwiYSI6IjAifV19.RWMwGPIAdM3kRNxqXNsJhlDtpAK4npaZ2LUk4TheVJE_rev001.jpg", 130000.0, "RS6", 580, 120000.0, new DateTime(2012, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "Description", "Engine", "ImagePath", "Mileage", "Model", "Power", "Price", "Production" },
                values: new object[] { 2, "Audi", "Niestety koszty utrzymania samochodu to fanaberia!", "V10 5.2", "http://blog.ozonee.pl/wp-content/uploads/2018/07/Audi-R8-V10.jpg", 54000.0, "R8", 525, 320000.0, new DateTime(2010, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
