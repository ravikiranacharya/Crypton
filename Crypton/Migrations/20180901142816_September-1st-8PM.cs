using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypton.Migrations
{
    public partial class September1st8PM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "providerId",
                table: "Currency",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Currency_providerId",
                table: "Currency",
                column: "providerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Currency_Provider_providerId",
                table: "Currency",
                column: "providerId",
                principalTable: "Provider",
                principalColumn: "providerID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currency_Provider_providerId",
                table: "Currency");

            migrationBuilder.DropIndex(
                name: "IX_Currency_providerId",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "providerId",
                table: "Currency");
        }
    }
}
