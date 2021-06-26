using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PSW_backend.Migrations
{
    public partial class updateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<String>(
              name: "AuthenticationToken",
              table: "User",
              type: "text",
              nullable: false,
              defaultValue: ""
              );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
