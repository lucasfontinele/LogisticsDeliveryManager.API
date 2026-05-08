using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsDeliveryManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderAndVehicleForRouting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFullyLoaded",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReadyForOrders",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "AssignedVehicleId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CargoType",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeliveryProofImageBase64",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryWindowEnd",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryWindowStart",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "DestinationAddressId",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Volume",
                table: "Orders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Orders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AssignedVehicleId",
                table: "Orders",
                column: "AssignedVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DestinationAddressId",
                table: "Orders",
                column: "DestinationAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_DestinationAddressId",
                table: "Orders",
                column: "DestinationAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Vehicles_AssignedVehicleId",
                table: "Orders",
                column: "AssignedVehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_DestinationAddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Vehicles_AssignedVehicleId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AssignedVehicleId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DestinationAddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsFullyLoaded",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "IsReadyForOrders",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "AssignedVehicleId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CargoType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryProofImageBase64",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryWindowEnd",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryWindowStart",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DestinationAddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Orders");
        }
    }
}
