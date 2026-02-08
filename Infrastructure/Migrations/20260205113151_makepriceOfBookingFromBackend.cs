using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fas7ny.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class makepriceOfBookingFromBackend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "booking_item_id",
                table: "bookings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "booking_item_id",
                table: "bookings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
