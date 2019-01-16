using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Waterworks.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pg_buffercache", "'pg_buffercache', '', ''")
                .Annotation("Npgsql:PostgresExtension:pg_stat_statements", "'pg_stat_statements', '', ''")
                .Annotation("Npgsql:PostgresExtension:postgis", "'postgis', '', ''");

            migrationBuilder.CreateTable(
                name: "adres_korespondencyjny",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    miejscowosc = table.Column<string>(maxLength: 40, nullable: true),
                    kod_pocztowy = table.Column<string>(maxLength: 6, nullable: true),
                    poczta = table.Column<string>(maxLength: 20, nullable: true),
                    ulica = table.Column<string>(maxLength: 40, nullable: true),
                    nr_domu = table.Column<string>(maxLength: 5, nullable: true),
                    nr_lokalu = table.Column<string>(maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adres_korespondencyjny", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "klasyfikatory",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nazwa_klasyfikatora = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_klasyfikatory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rodzaj_klienta",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    typ_klienta = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rodzaj_klienta", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "wartosci_klasyfikatorow",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    klasyfikatory_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wartosci_klasyfikatorow", x => x.id);
                    table.ForeignKey(
                        name: "wartosci_klasyfikatorow_klasyfikatory",
                        column: x => x.klasyfikatory_id,
                        principalTable: "klasyfikatory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cennik_scieki",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    cena_za_m3 = table.Column<float>(nullable: false),
                    rodzaj_klienta_id = table.Column<int>(nullable: false),
                    nazwa_cennika = table.Column<string>(maxLength: 40, nullable: false),
                    typ_cennika = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cennik_scieki", x => x.id);
                    table.ForeignKey(
                        name: "cennik_scieki_rodzaj_klienta",
                        column: x => x.rodzaj_klienta_id,
                        principalTable: "rodzaj_klienta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cennik_woda",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    cena_za_m3 = table.Column<int>(nullable: false),
                    rodzaj_klienta_id = table.Column<int>(nullable: false),
                    nazwa_cennika = table.Column<string>(maxLength: 40, nullable: false),
                    typ_cennika = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cennik_woda", x => x.id);
                    table.ForeignKey(
                        name: "cennik_woda_rodzaj_klienta",
                        column: x => x.rodzaj_klienta_id,
                        principalTable: "rodzaj_klienta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "klient",
                columns: table => new
                {
                    id_klienta = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    czy_aktywny = table.Column<bool>(nullable: false),
                    rodzaj_klienta_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_klient", x => x.id_klienta);
                    table.ForeignKey(
                        name: "klient_rodzaj_klienta",
                        column: x => x.rodzaj_klienta_id,
                        principalTable: "rodzaj_klienta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "normy_zuzycia_wody",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    zuzycie_m3_na_miesiac = table.Column<float>(nullable: false),
                    nazwa_normy = table.Column<int>(nullable: false),
                    zuzycie_dm3_na_dobe = table.Column<int>(nullable: false),
                    rodzaj_klienta_id = table.Column<int>(nullable: false),
                    opis_normy = table.Column<string>(maxLength: 40, nullable: false),
                    typ_normy = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_normy_zuzycia_wody", x => x.id);
                    table.ForeignKey(
                        name: "normy_zuzycia_wody_rodzaj_klienta",
                        column: x => x.rodzaj_klienta_id,
                        principalTable: "rodzaj_klienta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "oplaty_abonamentowe_scieki",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    okres_rozliczeniowy = table.Column<string>(maxLength: 40, nullable: false),
                    oplata = table.Column<float>(nullable: false),
                    rodzaj_klienta_id = table.Column<int>(nullable: false),
                    nazwa = table.Column<string>(maxLength: 30, nullable: false),
                    typ_cennika = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oplaty_abonamentowe_scieki", x => x.id);
                    table.ForeignKey(
                        name: "oplaty_abonamentowe_scieki_rodzaj_klienta",
                        column: x => x.rodzaj_klienta_id,
                        principalTable: "rodzaj_klienta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "oplaty_abonamentowe_woda",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    okres_rozliczeniowy = table.Column<string>(maxLength: 40, nullable: false),
                    oplata = table.Column<float>(nullable: false),
                    rodzaj_klienta_id = table.Column<int>(nullable: false),
                    nazwa = table.Column<string>(maxLength: 30, nullable: false),
                    typ_cennika = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oplaty_abonamentowe_woda", x => x.id);
                    table.ForeignKey(
                        name: "oplaty_abonamentowe_woda_rodzaj_klienta",
                        column: x => x.rodzaj_klienta_id,
                        principalTable: "rodzaj_klienta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "aktywny_odbiorca",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    imie = table.Column<string>(maxLength: 40, nullable: false),
                    nazwisko = table.Column<string>(maxLength: 40, nullable: false),
                    miejscowosc = table.Column<string>(maxLength: 40, nullable: false),
                    kod_pocztowy = table.Column<string>(maxLength: 6, nullable: false),
                    poczta = table.Column<string>(maxLength: 20, nullable: false),
                    ulica = table.Column<string>(maxLength: 40, nullable: false),
                    nr_domu = table.Column<string>(maxLength: 5, nullable: false),
                    nr_lokalu = table.Column<string>(maxLength: 5, nullable: true),
                    telefon = table.Column<string>(maxLength: 15, nullable: false),
                    telefon_2 = table.Column<string>(maxLength: 15, nullable: true),
                    email = table.Column<string>(maxLength: 20, nullable: false),
                    email_2 = table.Column<string>(maxLength: 20, nullable: true),
                    klient_id_klienta = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aktywny_odbiorca", x => x.id);
                    table.ForeignKey(
                        name: "aktywny_odbiorca_klient",
                        column: x => x.klient_id_klienta,
                        principalTable: "klient",
                        principalColumn: "id_klienta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "dane_klienta",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    imie = table.Column<string>(maxLength: 40, nullable: false),
                    nazwisko = table.Column<string>(maxLength: 40, nullable: false),
                    miejscowosc = table.Column<string>(maxLength: 40, nullable: false),
                    kod_pocztowy = table.Column<string>(maxLength: 6, nullable: false),
                    poczta = table.Column<string>(maxLength: 20, nullable: false),
                    ulica = table.Column<string>(maxLength: 40, nullable: false),
                    nr_domu = table.Column<string>(maxLength: 5, nullable: false),
                    nr_lokalu = table.Column<string>(maxLength: 5, nullable: true),
                    telefon = table.Column<string>(maxLength: 15, nullable: false),
                    telefon_2 = table.Column<string>(maxLength: 15, nullable: true),
                    email = table.Column<string>(maxLength: 20, nullable: false),
                    email_2 = table.Column<string>(maxLength: 20, nullable: true),
                    adres_korespondencyjny_id = table.Column<int>(nullable: true),
                    id_klienta = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dane_klienta", x => x.id);
                    table.ForeignKey(
                        name: "dane_klienta_adres_korespondencyjny",
                        column: x => x.adres_korespondencyjny_id,
                        principalTable: "adres_korespondencyjny",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "dane_klienta_klient",
                        column: x => x.id_klienta,
                        principalTable: "klient",
                        principalColumn: "id_klienta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "identyfikator_podatkowy",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    pesel = table.Column<string>(maxLength: 11, nullable: true),
                    nip = table.Column<string>(maxLength: 12, nullable: true),
                    regon = table.Column<string>(maxLength: 9, nullable: true),
                    krs = table.Column<string>(maxLength: 10, nullable: true),
                    klient_id_klienta = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identyfikator_podatkowy", x => x.id);
                    table.ForeignKey(
                        name: "identyfikator_podatkowy_klient",
                        column: x => x.klient_id_klienta,
                        principalTable: "klient",
                        principalColumn: "id_klienta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "obiekt",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    klient_id_klienta = table.Column<int>(nullable: true),
                    geometria = table.Column<Point>(nullable: true),
                    sposob_rozliczenia = table.Column<string>(maxLength: 20, nullable: false),
                    aktywny_odbiorca_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_obiekt", x => x.id);
                    table.ForeignKey(
                        name: "obiekt_aktywny_odbiorca",
                        column: x => x.aktywny_odbiorca_id,
                        principalTable: "aktywny_odbiorca",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "obiekt_klient",
                        column: x => x.klient_id_klienta,
                        principalTable: "klient",
                        principalColumn: "id_klienta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "adres_obiektu",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    miejscowosc = table.Column<string>(maxLength: 40, nullable: false),
                    ulica = table.Column<string>(maxLength: 40, nullable: false),
                    nr_domu = table.Column<string>(maxLength: 5, nullable: false),
                    nr_lokalu = table.Column<string>(maxLength: 5, nullable: true),
                    nr_dzialki = table.Column<string>(maxLength: 30, nullable: false),
                    kod_pocztowy = table.Column<string>(maxLength: 6, nullable: false),
                    poczta = table.Column<string>(maxLength: 20, nullable: true),
                    obiekt_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adres_obiektu", x => x.id);
                    table.ForeignKey(
                        name: "adres_obiektu_obiekt",
                        column: x => x.obiekt_id,
                        principalTable: "obiekt",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "klasyfikatory_obiektu",
                columns: table => new
                {
                    obiekt_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    klasyfikatory_id = table.Column<int>(nullable: false),
                    wartosci_klasyfikatorow_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_klasyfikatory_obiektu", x => x.obiekt_id);
                    table.ForeignKey(
                        name: "klasyfikatory_obiektu_klasyfikatory",
                        column: x => x.klasyfikatory_id,
                        principalTable: "klasyfikatory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "klasyfikatory_obiektu_obiekt",
                        column: x => x.obiekt_id,
                        principalTable: "obiekt",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "klasyfikatory_obiektu_wartosci_klasyfikatorow",
                        column: x => x.wartosci_klasyfikatorow_id,
                        principalTable: "wartosci_klasyfikatorow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_adres_obiektu_obiekt_id",
                table: "adres_obiektu",
                column: "obiekt_id");

            migrationBuilder.CreateIndex(
                name: "IX_aktywny_odbiorca_klient_id_klienta",
                table: "aktywny_odbiorca",
                column: "klient_id_klienta");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cennik_scieki_rodzaj_klienta_id",
                table: "cennik_scieki",
                column: "rodzaj_klienta_id");

            migrationBuilder.CreateIndex(
                name: "IX_cennik_woda_rodzaj_klienta_id",
                table: "cennik_woda",
                column: "rodzaj_klienta_id");

            migrationBuilder.CreateIndex(
                name: "IX_dane_klienta_adres_korespondencyjny_id",
                table: "dane_klienta",
                column: "adres_korespondencyjny_id");

            migrationBuilder.CreateIndex(
                name: "IX_dane_klienta_id_klienta",
                table: "dane_klienta",
                column: "id_klienta");

            migrationBuilder.CreateIndex(
                name: "IX_identyfikator_podatkowy_klient_id_klienta",
                table: "identyfikator_podatkowy",
                column: "klient_id_klienta");

            migrationBuilder.CreateIndex(
                name: "IX_klasyfikatory_obiektu_klasyfikatory_id",
                table: "klasyfikatory_obiektu",
                column: "klasyfikatory_id");

            migrationBuilder.CreateIndex(
                name: "IX_klasyfikatory_obiektu_wartosci_klasyfikatorow_id",
                table: "klasyfikatory_obiektu",
                column: "wartosci_klasyfikatorow_id");

            migrationBuilder.CreateIndex(
                name: "IX_klient_rodzaj_klienta_id",
                table: "klient",
                column: "rodzaj_klienta_id");

            migrationBuilder.CreateIndex(
                name: "IX_normy_zuzycia_wody_rodzaj_klienta_id",
                table: "normy_zuzycia_wody",
                column: "rodzaj_klienta_id");

            migrationBuilder.CreateIndex(
                name: "IX_obiekt_aktywny_odbiorca_id",
                table: "obiekt",
                column: "aktywny_odbiorca_id");

            migrationBuilder.CreateIndex(
                name: "IX_obiekt_klient_id_klienta",
                table: "obiekt",
                column: "klient_id_klienta");

            migrationBuilder.CreateIndex(
                name: "IX_oplaty_abonamentowe_scieki_rodzaj_klienta_id",
                table: "oplaty_abonamentowe_scieki",
                column: "rodzaj_klienta_id");

            migrationBuilder.CreateIndex(
                name: "IX_oplaty_abonamentowe_woda_rodzaj_klienta_id",
                table: "oplaty_abonamentowe_woda",
                column: "rodzaj_klienta_id");

            migrationBuilder.CreateIndex(
                name: "IX_wartosci_klasyfikatorow_klasyfikatory_id",
                table: "wartosci_klasyfikatorow",
                column: "klasyfikatory_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adres_obiektu");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "cennik_scieki");

            migrationBuilder.DropTable(
                name: "cennik_woda");

            migrationBuilder.DropTable(
                name: "dane_klienta");

            migrationBuilder.DropTable(
                name: "identyfikator_podatkowy");

            migrationBuilder.DropTable(
                name: "klasyfikatory_obiektu");

            migrationBuilder.DropTable(
                name: "normy_zuzycia_wody");

            migrationBuilder.DropTable(
                name: "oplaty_abonamentowe_scieki");

            migrationBuilder.DropTable(
                name: "oplaty_abonamentowe_woda");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "adres_korespondencyjny");

            migrationBuilder.DropTable(
                name: "obiekt");

            migrationBuilder.DropTable(
                name: "wartosci_klasyfikatorow");

            migrationBuilder.DropTable(
                name: "aktywny_odbiorca");

            migrationBuilder.DropTable(
                name: "klasyfikatory");

            migrationBuilder.DropTable(
                name: "klient");

            migrationBuilder.DropTable(
                name: "rodzaj_klienta");
        }
    }
}
