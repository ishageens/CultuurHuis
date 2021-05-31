using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservaties");

            migrationBuilder.DropTable(
                name: "Klanten");

            migrationBuilder.DropTable(
                name: "Voorstellingen");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
