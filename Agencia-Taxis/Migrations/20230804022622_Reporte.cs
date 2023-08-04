using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agencia_Taxis.Migrations
{
    public partial class Reporte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reportes_Choferes_ChofereId",
                table: "Reportes");

            migrationBuilder.RenameColumn(
                name: "ChofereId",
                table: "Reportes",
                newName: "ChoferId");

            migrationBuilder.RenameIndex(
                name: "IX_Reportes_ChofereId",
                table: "Reportes",
                newName: "IX_Reportes_ChoferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reportes_Choferes_ChoferId",
                table: "Reportes",
                column: "ChoferId",
                principalTable: "Choferes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reportes_Choferes_ChoferId",
                table: "Reportes");

            migrationBuilder.RenameColumn(
                name: "ChoferId",
                table: "Reportes",
                newName: "ChofereId");

            migrationBuilder.RenameIndex(
                name: "IX_Reportes_ChoferId",
                table: "Reportes",
                newName: "IX_Reportes_ChofereId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reportes_Choferes_ChofereId",
                table: "Reportes",
                column: "ChofereId",
                principalTable: "Choferes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
