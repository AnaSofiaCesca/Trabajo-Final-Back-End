using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TpFinal.DL.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Matricula = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Especialidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Dni = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ObraSocial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turnos_Doctores_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turnos_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Doctores",
                columns: new[] { "Id", "Apellido", "Email", "Especialidad", "Matricula", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "López", "laura.lopez@turnero.com", "Clínica Médica", "MP-1001", "Laura", "381-4000001" },
                    { 2, "Gómez", "martin.gomez@turnero.com", "Cardiología", "MP-1002", "Martín", "381-4000002" }
                });

            migrationBuilder.InsertData(
                table: "Pacientes",
                columns: new[] { "Id", "Apellido", "Dni", "Email", "FechaNacimiento", "Nombre", "ObraSocial", "Telefono" },
                values: new object[,]
                {
                    { 1, "García", "30111222", "juan.garcia@mail.com", new DateTime(1985, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juan", "OSDE", "381-5000001" },
                    { 2, "Fernández", "35222333", "maria.fernandez@mail.com", new DateTime(1992, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "María", "Swiss Medical", "381-5000002" }
                });

            migrationBuilder.InsertData(
                table: "Turnos",
                columns: new[] { "Id", "DoctorId", "Estado", "FechaHora", "Motivo", "Observaciones", "PacienteId" },
                values: new object[] { 1, 1, 1, new DateTime(2026, 12, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Control general", null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Doctores_Matricula",
                table: "Doctores",
                column: "Matricula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_Dni",
                table: "Pacientes",
                column: "Dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_DoctorId_FechaHora",
                table: "Turnos",
                columns: new[] { "DoctorId", "FechaHora" });

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_PacienteId",
                table: "Turnos",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Doctores");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
