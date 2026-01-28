using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ______________.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "tourist_places",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "restaurants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "packages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "hotels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "cities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "cities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tourist_places_CategoryId",
                table: "tourist_places",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_restaurants_CategoryId",
                table: "restaurants",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_packages_CategoryId",
                table: "packages",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_hotels_CategoryId",
                table: "hotels",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "ix_categories_name",
                table: "categories",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_hotels_categories",
                table: "hotels",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_packages_categories",
                table: "packages",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_restaurants_categories",
                table: "restaurants",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_tourist_places_categories",
                table: "tourist_places",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_hotels_categories",
                table: "hotels");

            migrationBuilder.DropForeignKey(
                name: "fk_packages_categories",
                table: "packages");

            migrationBuilder.DropForeignKey(
                name: "fk_restaurants_categories",
                table: "restaurants");

            migrationBuilder.DropForeignKey(
                name: "fk_tourist_places_categories",
                table: "tourist_places");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropIndex(
                name: "IX_tourist_places_CategoryId",
                table: "tourist_places");

            migrationBuilder.DropIndex(
                name: "IX_restaurants_CategoryId",
                table: "restaurants");

            migrationBuilder.DropIndex(
                name: "IX_packages_CategoryId",
                table: "packages");

            migrationBuilder.DropIndex(
                name: "IX_hotels_CategoryId",
                table: "hotels");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "tourist_places");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "restaurants");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "packages");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "hotels");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "cities");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "cities");
        }
    }
}
