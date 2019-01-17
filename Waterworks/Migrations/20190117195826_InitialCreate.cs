using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Waterworks.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "cena_za_m3",
                table: "cennik_woda",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "BasicCreateObjectViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SposobRozliczenia = table.Column<string>(nullable: false),
                    NumerKlienta = table.Column<int>(nullable: true),
                    Geometria = table.Column<string>(nullable: false),
                    Miejscowosc = table.Column<string>(nullable: false),
                    KodPocztowy = table.Column<string>(nullable: false),
                    NrDomu = table.Column<string>(nullable: false),
                    Ulica = table.Column<string>(nullable: false),
                    NrLokalu = table.Column<string>(nullable: false),
                    NrDzialki = table.Column<string>(nullable: true),
                    Poczta = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicCreateObjectViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectInListViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IdKlienta = table.Column<int>(nullable: true),
                    SposobRozliczenia = table.Column<string>(nullable: false),
                    Miejscowosc = table.Column<string>(nullable: false),
                    KodPocztowy = table.Column<string>(nullable: false),
                    NrDomu = table.Column<string>(nullable: false),
                    Ulica = table.Column<string>(nullable: false),
                    NrLokalu = table.Column<string>(nullable: false),
                    NrDzialki = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectInListViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CenaZaM3 = table.Column<float>(nullable: false),
                    RodzajKlienta = table.Column<string>(nullable: true),
                    RodzajKlientaId = table.Column<int>(nullable: false),
                    NazwaCennika = table.Column<string>(nullable: true),
                    TypCennika = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPriceViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    OkresRozliczeniowy = table.Column<string>(nullable: true),
                    Oplata = table.Column<float>(nullable: false),
                    RodzajKlienta = table.Column<string>(nullable: true),
                    RodzajKlientaId = table.Column<int>(nullable: false),
                    NazwaCennika = table.Column<string>(nullable: true),
                    TypCennika = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPriceViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wodomierz",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    nr_wodomierza = table.Column<int>(nullable: false),
                    data_ewidencji = table.Column<DateTime>(type: "date", nullable: false),
                    data_legalizacji = table.Column<DateTime>(type: "date", nullable: false),
                    obiekt_id = table.Column<int>(nullable: false),
                    typ_wodomierza = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wodomierz", x => x.id);
                    table.ForeignKey(
                        name: "wodomierz_obiekt",
                        column: x => x.obiekt_id,
                        principalTable: "obiekt",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "formuly_zmniejszajace",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    obiekt_id = table.Column<int>(nullable: false),
                    wodomierz_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formuly_zmniejszajace", x => x.id);
                    table.ForeignKey(
                        name: "formuly_zmniejszajace_wodomierz",
                        column: x => x.wodomierz_id,
                        principalTable: "wodomierz",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_formuly_zmniejszajace_wodomierz_id",
                table: "formuly_zmniejszajace",
                column: "wodomierz_id");

            migrationBuilder.CreateIndex(
                name: "IX_wodomierz_obiekt_id",
                table: "wodomierz",
                column: "obiekt_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasicCreateObjectViewModel");

            migrationBuilder.DropTable(
                name: "formuly_zmniejszajace");

            migrationBuilder.DropTable(
                name: "ObjectInListViewModel");

            migrationBuilder.DropTable(
                name: "PriceViewModel");

            migrationBuilder.DropTable(
                name: "SubscriptionPriceViewModel");

            migrationBuilder.DropTable(
                name: "wodomierz");

            migrationBuilder.AlterColumn<int>(
                name: "cena_za_m3",
                table: "cennik_woda",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
