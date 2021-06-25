using Microsoft.EntityFrameworkCore.Migrations;

namespace PSW_backend.Migrations
{
    public partial class UpdateDrug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drug_Prescription_PrescriptionId",
                table: "Drug");

            migrationBuilder.DropIndex(
                name: "IX_Drug_PrescriptionId",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "Drug");

            migrationBuilder.CreateTable(
                name: "DrugPrescription",
                columns: table => new
                {
                    DrugsId = table.Column<int>(type: "integer", nullable: false),
                    PrescriptionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugPrescription", x => new { x.DrugsId, x.PrescriptionsId });
                    table.ForeignKey(
                        name: "FK_DrugPrescription_Drug_DrugsId",
                        column: x => x.DrugsId,
                        principalTable: "Drug",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugPrescription_Prescription_PrescriptionsId",
                        column: x => x.PrescriptionsId,
                        principalTable: "Prescription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrugPrescription_PrescriptionsId",
                table: "DrugPrescription",
                column: "PrescriptionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrugPrescription");

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionId",
                table: "Drug",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Drug_PrescriptionId",
                table: "Drug",
                column: "PrescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drug_Prescription_PrescriptionId",
                table: "Drug",
                column: "PrescriptionId",
                principalTable: "Prescription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
