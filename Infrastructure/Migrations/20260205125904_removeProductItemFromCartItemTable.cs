using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fas7ny.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeProductItemFromCartItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_items_bookings_ProductId",
                table: "cart_items");

            migrationBuilder.DropIndex(
                name: "IX_cart_items_ProductId",
                table: "cart_items");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "cart_items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "cart_items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_cart_items_ProductId",
                table: "cart_items",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_items_bookings_ProductId",
                table: "cart_items",
                column: "ProductId",
                principalTable: "bookings",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
