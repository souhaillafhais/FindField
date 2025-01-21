using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionTerrains.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStaffDeleteBehavior3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Terrains_TerrainId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Terrains_Staffs_StaffId",
                table: "Terrains");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Terrains_TerrainId",
                table: "Reservations",
                column: "TerrainId",
                principalTable: "Terrains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Terrains_Staffs_StaffId",
                table: "Terrains",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
