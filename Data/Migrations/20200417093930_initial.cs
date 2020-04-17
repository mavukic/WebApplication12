using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication12.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sklonište",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    Grad = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Web = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sklonište", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Udruga",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    Grad = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Web = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Udruga", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ljubimac",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    SkloništeId = table.Column<int>(nullable: true),
                    Mjesto = table.Column<string>(nullable: true),
                    Vrsta = table.Column<string>(nullable: true),
                    Godine = table.Column<int>(nullable: false),
                    Slika = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ljubimac", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ljubimac_Sklonište_SkloništeId",
                        column: x => x.SkloništeId,
                        principalTable: "Sklonište",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostSkloništa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    SkloništeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSkloništa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostSkloništa_Sklonište_SkloništeId",
                        column: x => x.SkloništeId,
                        principalTable: "Sklonište",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostUdruge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    UdrugaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostUdruge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostUdruge_Udruga_UdrugaId",
                        column: x => x.UdrugaId,
                        principalTable: "Udruga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posvajatelj",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Mjesto = table.Column<string>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    BrMob = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    LjubimacId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posvajatelj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posvajatelj_Ljubimac_LjubimacId",
                        column: x => x.LjubimacId,
                        principalTable: "Ljubimac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ljubimac_SkloništeId",
                table: "Ljubimac",
                column: "SkloništeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostSkloništa_SkloništeId",
                table: "PostSkloništa",
                column: "SkloništeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostUdruge_UdrugaId",
                table: "PostUdruge",
                column: "UdrugaId");

            migrationBuilder.CreateIndex(
                name: "IX_Posvajatelj_LjubimacId",
                table: "Posvajatelj",
                column: "LjubimacId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostSkloništa");

            migrationBuilder.DropTable(
                name: "PostUdruge");

            migrationBuilder.DropTable(
                name: "Posvajatelj");

            migrationBuilder.DropTable(
                name: "Udruga");

            migrationBuilder.DropTable(
                name: "Ljubimac");

            migrationBuilder.DropTable(
                name: "Sklonište");
        }
    }
}
