using Microsoft.EntityFrameworkCore.Migrations;

namespace PSW_backend.Migrations
{
    public partial class UpdatePrescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Prescription",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Prescription",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_DoctorId",
                table: "Prescription",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_PatientId",
                table: "Prescription",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Doctor_DoctorId",
                table: "Prescription",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Patient_PatientId",
                table: "Prescription",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Doctor_DoctorId",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Patient_PatientId",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_DoctorId",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_PatientId",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Prescription");
        }
    }
}
