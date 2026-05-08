using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsDeliveryManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixSchemaFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CurrentDriverId",
                table: "Vehicles",
                column: "CurrentDriverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_CurrentDriverId",
                table: "Vehicles");
        }
    }
}
