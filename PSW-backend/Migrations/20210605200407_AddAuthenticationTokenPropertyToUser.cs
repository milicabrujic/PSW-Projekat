using Microsoft.EntityFrameworkCore.Migrations;

namespace PSW_backend.Migrations
{
    public partial class AddAuthenticationTokenPropertyToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthenticationToken",
                table: "User",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthenticationToken",
                table: "User");
        }
    }
}
