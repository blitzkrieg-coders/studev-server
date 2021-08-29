using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Studev.Server.Migrations
{
    public partial class AddStudentProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "Student",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 29, 9, 17, 1, 9, DateTimeKind.Local).AddTicks(6140),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 8, 28, 21, 44, 59, 813, DateTimeKind.Local).AddTicks(2844));

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Student",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Student",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Student",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Student",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Student");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "Student",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 28, 21, 44, 59, 813, DateTimeKind.Local).AddTicks(2844),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 8, 29, 9, 17, 1, 9, DateTimeKind.Local).AddTicks(6140));
        }
    }
}
