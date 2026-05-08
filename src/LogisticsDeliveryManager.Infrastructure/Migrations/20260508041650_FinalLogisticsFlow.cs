using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsDeliveryManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FinalLogisticsFlow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CurrentDriverId",
                table: "Vehicles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Orders",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CurrentDriverId",
                table: "Vehicles",
                column: "CurrentDriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Drivers_CurrentDriverId",
                table: "Vehicles",
                column: "CurrentDriverId",
                principalTable: "Drivers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Drivers_CurrentDriverId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_CurrentDriverId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CurrentDriverId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Orders");
        }
    }
}
