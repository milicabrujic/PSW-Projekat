using Microsoft.EntityFrameworkCore.Migrations;

namespace PSW_backend.Migrations
{
    public partial class AddedDoctorType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Doctor",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Doctor");
        }
    }
}
