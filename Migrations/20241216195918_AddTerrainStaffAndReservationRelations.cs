using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionTerrains.Migrations
{
    /// <inheritdoc />
    public partial class AddTerrainStaffAndReservationRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "Terrains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TerrainId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Terrains_StaffId",
                table: "Terrains",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TerrainId",
                table: "Reservations",
                column: "TerrainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Terrains_TerrainId",
                table: "Reservations",
                column: "TerrainId",
                principalTable: "Terrains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Terrains_Staffs_StaffId",
                table: "Terrains",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Terrains_TerrainId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Terrains_Staffs_StaffId",
                table: "Terrains");

            migrationBuilder.DropIndex(
                name: "IX_Terrains_StaffId",
                table: "Terrains");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_TerrainId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Terrains");

            migrationBuilder.DropColumn(
                name: "TerrainId",
                table: "Reservations");
        }
    }
}
