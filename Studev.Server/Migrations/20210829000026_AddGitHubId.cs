using Microsoft.EntityFrameworkCore.Migrations;

namespace Studev.Server.Migrations
{
    public partial class AddGitHubId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "GithubLogin",
                table: "Student",
                newName: "GitHubLogin");

            migrationBuilder.AddColumn<int>(
                name: "GitHubId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GitHubId",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "GitHubLogin",
                table: "Student",
                newName: "GithubLogin");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Student",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
