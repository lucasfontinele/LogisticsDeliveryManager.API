using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsDeliveryManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoNovasEntidadesDaDeveloper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Employees",
                newName: "Email_Address");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Customers",
                newName: "Email_Address");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Addresses",
                newName: "PostalCode_Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email_Address",
                table: "Employees",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Email_Address",
                table: "Customers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "PostalCode_Code",
                table: "Addresses",
                newName: "PostalCode");
        }
    }
}
