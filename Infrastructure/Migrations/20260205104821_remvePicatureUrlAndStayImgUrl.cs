using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fas7ny.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class remvePicatureUrlAndStayImgUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "activities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "activities",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
