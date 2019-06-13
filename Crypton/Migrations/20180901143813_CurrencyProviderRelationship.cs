using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypton.Migrations
{
    public partial class CurrencyProviderRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currency_Provider_providerId",
                table: "Currency");

            migrationBuilder.RenameColumn(
                name: "providerId",
                table: "Currency",
                newName: "providerID");

            migrationBuilder.RenameIndex(
                name: "IX_Currency_providerId",
                table: "Currency",
                newName: "IX_Currency_providerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Currency_Provider_providerID",
                table: "Currency",
                column: "providerID",
                principalTable: "Provider",
                principalColumn: "providerID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currency_Provider_providerID",
                table: "Currency");

            migrationBuilder.RenameColumn(
                name: "providerID",
                table: "Currency",
                newName: "providerId");

            migrationBuilder.RenameIndex(
                name: "IX_Currency_providerID",
                table: "Currency",
                newName: "IX_Currency_providerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Currency_Provider_providerId",
                table: "Currency",
                column: "providerId",
                principalTable: "Provider",
                principalColumn: "providerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
