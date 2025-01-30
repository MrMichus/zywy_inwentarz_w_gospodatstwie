using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inwentarz.Migrations
{
    /// <inheritdoc />
    public partial class migracja1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanZywieniowy",
                columns: table => new
                {
                    KarmienieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoraKarmienia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RodzajPaszy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Ilosc = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanZywieniowy", x => x.KarmienieId);
                });

            migrationBuilder.CreateTable(
                name: "Rasa",
                columns: table => new
                {
                    RasyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gatunek = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NazwaRasy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rasa", x => x.RasyId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Pracownik",
                columns: table => new
                {
                    IdPracownika = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Stanowisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataZatrudnienia = table.Column<DateOnly>(type: "date", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownik", x => x.IdPracownika);
                    table.ForeignKey(
                        name: "FK_Pracownik_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zwierze",
                columns: table => new
                {
                    ZwierzeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gatunek = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Rasa = table.Column<int>(type: "int", nullable: true),
                    DataUrodzenia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Plec = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PrzyjazdData = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Waga = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    StatusZdrowotny = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OpiekunId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zwierze", x => x.ZwierzeId);
                    table.ForeignKey(
                        name: "FK_Zwierze_Pracownik_OpiekunId",
                        column: x => x.OpiekunId,
                        principalTable: "Pracownik",
                        principalColumn: "IdPracownika");
                    table.ForeignKey(
                        name: "FK_Zwierze_Rasa_Rasa",
                        column: x => x.Rasa,
                        principalTable: "Rasa",
                        principalColumn: "RasyId");
                });

            migrationBuilder.CreateTable(
                name: "ZabiegWeterynaryjny",
                columns: table => new
                {
                    LeczenieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZwierzeId = table.Column<int>(type: "int", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RodzajStworzenia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weterynarz = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZabiegWeterynaryjny", x => x.LeczenieId);
                    table.ForeignKey(
                        name: "FK_ZabiegWeterynaryjny_Zwierze_ZwierzeId",
                        column: x => x.ZwierzeId,
                        principalTable: "Zwierze",
                        principalColumn: "ZwierzeId");
                });

            migrationBuilder.CreateTable(
                name: "HistoriaZdrowotna",
                columns: table => new
                {
                    RekordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentyfikatorZwierzecia = table.Column<int>(type: "int", nullable: true),
                    Diagnoza = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DataDiagnozy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LeczenieId = table.Column<int>(type: "int", nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriaZdrowotna", x => x.RekordId);
                    table.ForeignKey(
                        name: "FK_HistoriaZdrowotna_ZabiegWeterynaryjny_LeczenieId",
                        column: x => x.LeczenieId,
                        principalTable: "ZabiegWeterynaryjny",
                        principalColumn: "LeczenieId");
                    table.ForeignKey(
                        name: "FK_HistoriaZdrowotna_Zwierze_IdentyfikatorZwierzecia",
                        column: x => x.IdentyfikatorZwierzecia,
                        principalTable: "Zwierze",
                        principalColumn: "ZwierzeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriaZdrowotna_IdentyfikatorZwierzecia",
                table: "HistoriaZdrowotna",
                column: "IdentyfikatorZwierzecia");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriaZdrowotna_LeczenieId",
                table: "HistoriaZdrowotna",
                column: "LeczenieId");

            migrationBuilder.CreateIndex(
                name: "IX_Pracownik_ApplicationUserId",
                table: "Pracownik",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ZabiegWeterynaryjny_ZwierzeId",
                table: "ZabiegWeterynaryjny",
                column: "ZwierzeId");

            migrationBuilder.CreateIndex(
                name: "IX_Zwierze_OpiekunId",
                table: "Zwierze",
                column: "OpiekunId");

            migrationBuilder.CreateIndex(
                name: "IX_Zwierze_Rasa",
                table: "Zwierze",
                column: "Rasa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "HistoriaZdrowotna");

            migrationBuilder.DropTable(
                name: "PlanZywieniowy");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ZabiegWeterynaryjny");

            migrationBuilder.DropTable(
                name: "Zwierze");

            migrationBuilder.DropTable(
                name: "Pracownik");

            migrationBuilder.DropTable(
                name: "Rasa");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
