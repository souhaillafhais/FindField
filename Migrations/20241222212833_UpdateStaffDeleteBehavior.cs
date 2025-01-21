using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionTerrains.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStaffDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Terrains_Staffs_StaffId",
                table: "Terrains");

            migrationBuilder.AddForeignKey(
                name: "FK_Terrains_Staffs_StaffId",
                table: "Terrains",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Terrains_Staffs_StaffId",
                table: "Terrains");

            migrationBuilder.AddForeignKey(
                name: "FK_Terrains_Staffs_StaffId",
                table: "Terrains",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
