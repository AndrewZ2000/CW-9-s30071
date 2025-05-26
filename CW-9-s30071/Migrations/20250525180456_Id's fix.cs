using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CW_9_s30071.Migrations
{
    /// <inheritdoc />
    public partial class Idsfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Prescription",
                newName: "IdPrescription");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Patient",
                newName: "IdPatient");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Medicament",
                newName: "IdMedicament");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Doctor",
                newName: "IdDoctor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdPrescription",
                table: "Prescription",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IdPatient",
                table: "Patient",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IdMedicament",
                table: "Medicament",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IdDoctor",
                table: "Doctor",
                newName: "Id");
        }
    }
}
