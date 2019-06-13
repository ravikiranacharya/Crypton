using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypton.Migrations
{
    public partial class GlobalDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlertType",
                columns: table => new
                {
                    alertTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    alertType = table.Column<string>(type: "varchar(50)", nullable: false),
                    isEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertType", x => x.alertTypeID);
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
                name: "Currency",
                columns: table => new
                {
                    currencyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    providerCurrencyID = table.Column<string>(type: "varchar(10)", nullable: false),
                    currencyName = table.Column<string>(type: "varchar(50)", nullable: false),
                    currencyCode = table.Column<string>(type: "varchar(10)", nullable: false),
                    description = table.Column<string>(type: "varchar(250)", nullable: true),
                    totalVolume = table.Column<double>(nullable: false),
                    currentVolume = table.Column<double>(nullable: false),
                    currentMarketCap = table.Column<double>(nullable: false),
                    officialWebsite = table.Column<string>(type: "varchar(50)", nullable: true),
                    marketRank = table.Column<int>(nullable: false),
                    lastUpdated = table.Column<DateTime>(nullable: false),
                    isEnabled = table.Column<bool>(nullable: false),
                    logoPath = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.currencyID);
                });

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    providerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    providerName = table.Column<string>(type: "varchar(50)", nullable: false),
                    providerWebsite = table.Column<string>(type: "varchar(50)", nullable: false),
                    apiUrl = table.Column<string>(type: "varchar(50)", nullable: false),
                    apiKey = table.Column<string>(type: "varchar(250)", nullable: true),
                    isEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.providerID);
                });

            migrationBuilder.CreateTable(
                name: "Variation",
                columns: table => new
                {
                    variationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    variationType = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variation", x => x.variationID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConversionRate",
                columns: table => new
                {
                    conversionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sourceCurrencyID = table.Column<int>(nullable: false),
                    targetCurrencyID = table.Column<int>(nullable: false),
                    conversionRate = table.Column<double>(nullable: false),
                    conversionDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversionRate", x => x.conversionID);
                    table.ForeignKey(
                        name: "FK_ConversionRate_Currency_sourceCurrencyID",
                        column: x => x.sourceCurrencyID,
                        principalTable: "Currency",
                        principalColumn: "currencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConversionRate_Currency_targetCurrencyID",
                        column: x => x.targetCurrencyID,
                        principalTable: "Currency",
                        principalColumn: "currencyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyPrice",
                columns: table => new
                {
                    currencyPriceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    currencyID = table.Column<int>(nullable: false),
                    providerID = table.Column<int>(nullable: false),
                    priceDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    priceUSD = table.Column<double>(nullable: false),
                    priceEuro = table.Column<double>(nullable: false),
                    priceBTC = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyPrice", x => x.currencyPriceID);
                    table.ForeignKey(
                        name: "FK_CurrencyPrice_Currency_currencyID",
                        column: x => x.currencyID,
                        principalTable: "Currency",
                        principalColumn: "currencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrencyPrice_Provider_providerID",
                        column: x => x.providerID,
                        principalTable: "Provider",
                        principalColumn: "providerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GlobalData",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    activeCurrencies = table.Column<int>(nullable: false),
                    activeMarkets = table.Column<int>(nullable: false),
                    bitcoinDominance = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    totalMarketCap = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    lastUpdated = table.Column<DateTime>(nullable: false),
                    providerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalData", x => x.id);
                    table.ForeignKey(
                        name: "FK_GlobalData_Provider_providerId",
                        column: x => x.providerId,
                        principalTable: "Provider",
                        principalColumn: "providerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alert",
                columns: table => new
                {
                    alertID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    userID = table.Column<int>(nullable: false),
                    limit = table.Column<double>(nullable: false),
                    createdOn = table.Column<DateTime>(nullable: false),
                    isFulfilled = table.Column<bool>(nullable: false),
                    fulfilledOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    conversionID = table.Column<int>(nullable: false),
                    variationID = table.Column<int>(nullable: false),
                    alertTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alert", x => x.alertID);
                    table.ForeignKey(
                        name: "FK_Alert_AlertType_alertTypeID",
                        column: x => x.alertTypeID,
                        principalTable: "AlertType",
                        principalColumn: "alertTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alert_ConversionRate_conversionID",
                        column: x => x.conversionID,
                        principalTable: "ConversionRate",
                        principalColumn: "conversionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alert_Variation_variationID",
                        column: x => x.variationID,
                        principalTable: "Variation",
                        principalColumn: "variationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    logID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    status = table.Column<string>(type: "varchar(50)", nullable: true),
                    description = table.Column<string>(type: "varchar(250)", nullable: true),
                    logDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    alertID = table.Column<int>(nullable: false),
                    alertTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.logID);
                    table.ForeignKey(
                        name: "FK_Log_Alert_alertID",
                        column: x => x.alertID,
                        principalTable: "Alert",
                        principalColumn: "alertID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_AlertType_alertTypeID",
                        column: x => x.alertTypeID,
                        principalTable: "AlertType",
                        principalColumn: "alertTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alert_alertTypeID",
                table: "Alert",
                column: "alertTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Alert_conversionID",
                table: "Alert",
                column: "conversionID");

            migrationBuilder.CreateIndex(
                name: "IX_Alert_variationID",
                table: "Alert",
                column: "variationID");

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
                name: "IX_ConversionRate_sourceCurrencyID",
                table: "ConversionRate",
                column: "sourceCurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionRate_targetCurrencyID",
                table: "ConversionRate",
                column: "targetCurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyPrice_currencyID",
                table: "CurrencyPrice",
                column: "currencyID");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyPrice_providerID",
                table: "CurrencyPrice",
                column: "providerID");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalData_providerId",
                table: "GlobalData",
                column: "providerId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_alertID",
                table: "Log",
                column: "alertID");

            migrationBuilder.CreateIndex(
                name: "IX_Log_alertTypeID",
                table: "Log",
                column: "alertTypeID");
        }

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
                name: "CurrencyPrice");

            migrationBuilder.DropTable(
                name: "GlobalData");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Provider");

            migrationBuilder.DropTable(
                name: "Alert");

            migrationBuilder.DropTable(
                name: "AlertType");

            migrationBuilder.DropTable(
                name: "ConversionRate");

            migrationBuilder.DropTable(
                name: "Variation");

            migrationBuilder.DropTable(
                name: "Currency");
        }
    }
}
