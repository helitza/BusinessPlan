using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFair.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mentor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sajam",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BrojSala = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sajam", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kompanija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HR = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Oblast = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    GodinaOsnivanja = table.Column<int>(type: "int", nullable: false),
                    SajamID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kompanija", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Kompanija_Sajam_SajamID",
                        column: x => x.SajamID,
                        principalTable: "Sajam",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SajamID = table.Column<int>(type: "int", nullable: true),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sala_Sajam_SajamID",
                        column: x => x.SajamID,
                        principalTable: "Sajam",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pozicija = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MentorID = table.Column<int>(type: "int", nullable: true),
                    KompanijaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Task_Kompanija_KompanijaID",
                        column: x => x.KompanijaID,
                        principalTable: "Kompanija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_Mentor_MentorID",
                        column: x => x.MentorID,
                        principalTable: "Mentor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prezentovanje",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KompanijaID = table.Column<int>(type: "int", nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalaID = table.Column<int>(type: "int", nullable: true),
                    SajamID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prezentovanje", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Prezentovanje_Kompanija_KompanijaID",
                        column: x => x.KompanijaID,
                        principalTable: "Kompanija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prezentovanje_Sajam_SajamID",
                        column: x => x.SajamID,
                        principalTable: "Sajam",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prezentovanje_Sala_SalaID",
                        column: x => x.SalaID,
                        principalTable: "Sala",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kompanija_SajamID",
                table: "Kompanija",
                column: "SajamID");

            migrationBuilder.CreateIndex(
                name: "IX_Prezentovanje_KompanijaID",
                table: "Prezentovanje",
                column: "KompanijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Prezentovanje_SajamID",
                table: "Prezentovanje",
                column: "SajamID");

            migrationBuilder.CreateIndex(
                name: "IX_Prezentovanje_SalaID",
                table: "Prezentovanje",
                column: "SalaID");

            migrationBuilder.CreateIndex(
                name: "IX_Sala_SajamID",
                table: "Sala",
                column: "SajamID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_KompanijaID",
                table: "Task",
                column: "KompanijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_MentorID",
                table: "Task",
                column: "MentorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prezentovanje");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Kompanija");

            migrationBuilder.DropTable(
                name: "Mentor");

            migrationBuilder.DropTable(
                name: "Sajam");
        }
    }
}
