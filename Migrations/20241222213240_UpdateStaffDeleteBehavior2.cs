using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionTerrains.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStaffDeleteBehavior2 : Migration
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
                onDelete: ReferentialAction.SetNull);
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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
