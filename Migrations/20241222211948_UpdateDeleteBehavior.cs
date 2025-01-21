﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionTerrains.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Terrains_TerrainId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Terrains_TerrainId",
                table: "Reservations",
                column: "TerrainId",
                principalTable: "Terrains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Terrains_TerrainId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Terrains_TerrainId",
                table: "Reservations",
                column: "TerrainId",
                principalTable: "Terrains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
