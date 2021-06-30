using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PSW_backend.Migrations
{
    public partial class HopsitalAppMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drug",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drug", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Surname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Username = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Password = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false),
                    IsMalicious = table.Column<bool>(type: "boolean", nullable: false),
                    AuthenticationToken = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrator_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctor_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CancelledMedicalAppointments = table.Column<int>(type: "integer", nullable: false),
                    GeneralDoctorId = table.Column<int>(type: "integer", nullable: false),
                    LastCanceledDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_Doctor_GeneralDoctorId",
                        column: x => x.GeneralDoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patient_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAppointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAppointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalAppointment_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalAppointment_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientFeedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsPosted = table.Column<bool>(type: "boolean", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientFeedback_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: true),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    DoctorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescription_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescription_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    SpecialistDoctorId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recommendations_Doctor_SpecialistDoctorId",
                        column: x => x.SpecialistDoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recommendations_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "Drug",
                columns: new[] { "Id", "Amount", "Name" },
                values: new object[,]
                {
                    { 1, 15, "Brufen" },
                    { 2, 20, "Aspirin" },
                    { 3, 30, "Paracetamol" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "AuthenticationToken", "Email", "IsBlocked", "IsMalicious", "Name", "Password", "PhoneNumber", "Role", "Surname", "Username" },
                values: new object[,]
                {
                    { 1, "Laze Teleckog 1, Novi Sad", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiQWRtaW5pc3RyYXRvciIsIklkIjoiMjMiLCJleHAiOjE2MjUwMDIwNzl9.04bjhzsBD-9bYL-8CtuYMYTnNvrvxrReah_eF8-Hbro", "admin@gmail.com", false, false, "admin", "Admin123!", "060000000", 2, "admin", "admin" },
                    { 2, "Laze Teleckog 1, Novi Sad", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiRG9jdG9yIiwiSWQiOiIzIiwiZXhwIjoxNjI1MDAyODMyfQ.2eYr9v6qV-OnlBVAd23Y501ouDbhUbrKN_m5UnF2YJw", "ii@gmail.com", false, false, "Ivan", "Admin123!", "060000000", 1, "Ivanovic", "ii" },
                    { 3, "Laze Teleckog 1, Novi Sad", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiRG9jdG9yIiwiSWQiOiIzIiwiZXhwIjoxNjI1MDc4MDgyfQ.FjZVW13Q2sQ4kpDqfPxS0LJDYDN1aVRc9B97FVZMAUA", "nn@gmail.com", false, false, "Nena", "Admin123!", "060000000", 1, "Nenic", "nn" },
                    { 4, "Laze Teleckog 1, Novi Sad", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiRG9jdG9yIiwiSWQiOiI0IiwiZXhwIjoxNjI1MDc4MTY3fQ.P-GUf_xNmiBTpzChik3CAuPx2FkWNGvtFaDMBNwI758", "dd@gmail.com", false, false, "Dara", "Admin123!", "060000000", 1, "Daric", "dd" },
                    { 5, "Laze Teleckog 1, Novi Sad", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiRG9jdG9yIiwiSWQiOiI1IiwiZXhwIjoxNjI1MDc4NTEwfQ.9zWG0T0foHWCnuVGUofP-V-OEkPrAw9OfbJACog1m34", "vv@gmail.com", false, false, "Vera", "Admin123!", "060000000", 1, "Veric", "vv" },
                    { 6, "Laze Teleckog 1, Novi Sad", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiUGF0aWVudCIsIklkIjoiMjEiLCJleHAiOjE2MjQ5OTY4OTd9.nsZgWxwWSeB-RxQFfvLSr5ruLoXtCFsHwzyABiYL7i0", "mb@gmail.com", false, false, "Milica", "Admin123!", "060000000", 0, "Brujic", "mb" },
                    { 7, "Laze Teleckog 1, Novi Sad", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiUGF0aWVudCIsIklkIjoiNyIsImV4cCI6MTYyNTA3ODY1NX0.2Fq9gQDhhfkVFg7_RoKTECIPZv9Aq0F1kmjWyHGB_Pw", "aa@gmail.com", false, false, "Ana", "Admin123!", "060000000", 0, "Antic", "aa" },
                    { 8, "Laze Teleckog 1, Novi Sad", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiUGF0aWVudCIsIklkIjoiOCIsImV4cCI6MTYyNTA3ODg0N30.U2S_GgPYTsermyI_8jiZMvSqR--uPNE0M5Qo_mk6SRw", "ll@gmail.com", false, false, "Laza", "Admin123!", "060000000", 0, "Lazic", "ll" }
                });

            migrationBuilder.InsertData(
                table: "Administrator",
                column: "Id",
                value: 1);

            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 2, 0 },
                    { 3, 0 },
                    { 4, 1 },
                    { 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "Id", "CancelledMedicalAppointments", "GeneralDoctorId", "LastCanceledDate" },
                values: new object[,]
                {
                    { 6, 2, 2, new DateTime(2021, 6, 30, 19, 59, 58, 707, DateTimeKind.Local).AddTicks(5275) },
                    { 7, 1, 2, new DateTime(2021, 6, 30, 19, 59, 58, 709, DateTimeKind.Local).AddTicks(9199) },
                    { 8, 1, 3, new DateTime(2021, 6, 30, 19, 59, 58, 709, DateTimeKind.Local).AddTicks(9239) }
                });

            migrationBuilder.InsertData(
                table: "PatientFeedback",
                columns: new[] { "Id", "Date", "IsPosted", "PatientId", "Text" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 6, 30, 19, 59, 58, 710, DateTimeKind.Local).AddTicks(2861), true, 6, "Great Hospital." },
                    { 3, new DateTime(2021, 6, 30, 19, 59, 58, 710, DateTimeKind.Local).AddTicks(4230), true, 6, "Great treetment." },
                    { 6, new DateTime(2021, 6, 30, 19, 59, 58, 710, DateTimeKind.Local).AddTicks(4241), true, 6, "Polite workers." },
                    { 2, new DateTime(2021, 6, 30, 19, 59, 58, 710, DateTimeKind.Local).AddTicks(4196), true, 7, "Great service." },
                    { 7, new DateTime(2021, 6, 30, 19, 59, 58, 710, DateTimeKind.Local).AddTicks(4244), false, 7, "Bad service." },
                    { 8, new DateTime(2021, 6, 30, 19, 59, 58, 710, DateTimeKind.Local).AddTicks(4247), false, 7, "Impolite doctors." },
                    { 4, new DateTime(2021, 6, 30, 19, 59, 58, 710, DateTimeKind.Local).AddTicks(4234), true, 8, "Nice doctors." },
                    { 5, new DateTime(2021, 6, 30, 19, 59, 58, 710, DateTimeKind.Local).AddTicks(4237), true, 8, "Nice personel." },
                    { 9, new DateTime(2021, 6, 30, 19, 59, 58, 710, DateTimeKind.Local).AddTicks(4250), false, 8, "Long waitings." }
                });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "Id", "DoctorId", "PatientId", "Text" },
                values: new object[,]
                {
                    { 1, 2, 6, "Take this medicine 3 times a week." },
                    { 3, 4, 6, "Take this medicine once a day." },
                    { 2, 3, 7, "Take this medicine 2 times a day." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrugPrescription_PrescriptionsId",
                table: "DrugPrescription",
                column: "PrescriptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAppointment_DoctorId",
                table: "MedicalAppointment",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAppointment_PatientId",
                table: "MedicalAppointment",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_GeneralDoctorId",
                table: "Patient",
                column: "GeneralDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFeedback_PatientId",
                table: "PatientFeedback",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_DoctorId",
                table: "Prescription",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_PatientId",
                table: "Prescription",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_PatientId",
                table: "Recommendations",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_SpecialistDoctorId",
                table: "Recommendations",
                column: "SpecialistDoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "DrugPrescription");

            migrationBuilder.DropTable(
                name: "MedicalAppointment");

            migrationBuilder.DropTable(
                name: "PatientFeedback");

            migrationBuilder.DropTable(
                name: "Recommendations");

            migrationBuilder.DropTable(
                name: "Drug");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
