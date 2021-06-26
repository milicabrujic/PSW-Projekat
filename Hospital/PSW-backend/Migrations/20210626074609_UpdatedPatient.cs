using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSW_backend.Migrations
{
    public partial class UpdatedPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastCanceledDate",
                table: "Patient",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastCanceledDate",
                table: "Patient");
        }
    }
}
