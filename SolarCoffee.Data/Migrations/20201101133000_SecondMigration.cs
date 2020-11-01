using Microsoft.EntityFrameworkCore.Migrations;

namespace SolarCoffee.Data.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAddresses_PrimeAddressId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PrimeAddressId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PrimeAddressId",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "PrimaryAddressId",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PrimaryAddressId",
                table: "Customers",
                column: "PrimaryAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAddresses_PrimaryAddressId",
                table: "Customers",
                column: "PrimaryAddressId",
                principalTable: "CustomerAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAddresses_PrimaryAddressId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PrimaryAddressId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PrimaryAddressId",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "PrimeAddressId",
                table: "Customers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PrimeAddressId",
                table: "Customers",
                column: "PrimeAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAddresses_PrimeAddressId",
                table: "Customers",
                column: "PrimeAddressId",
                principalTable: "CustomerAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
