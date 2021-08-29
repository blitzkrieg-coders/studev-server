using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Studev.Server.Migrations
{
    public partial class AddRegistrationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "Student",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 28, 21, 44, 59, 813, DateTimeKind.Local).AddTicks(2844));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "Student");
        }
    }
}
