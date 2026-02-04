using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fas7ny.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    preferences_json = table.Column<string>(type: "jsonb", nullable: true),
                    role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "User"),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.id);
                    table.ForeignKey(
                        name: "fk_cities_countries",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "role_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_claims_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    booking_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    booking_item_id = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Pending")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.id);
                    table.ForeignKey(
                        name: "fk_bookings_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.id);
                    table.ForeignKey(
                        name: "fk_carts_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chat_messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    message_text = table.Column<string>(type: "text", nullable: false),
                    response_text = table.Column<string>(type: "text", nullable: true),
                    timestamp = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_messages", x => x.id);
                    table.ForeignKey(
                        name: "fk_chat_messages_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recommendations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    recommended_item_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    item_id = table.Column<int>(type: "integer", nullable: false),
                    reason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recommendations", x => x.id);
                    table.ForeignKey(
                        name: "fk_recommendations_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "search_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    query = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    search_date = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_search_logs", x => x.id);
                    table.ForeignKey(
                        name: "fk_search_logs_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_claims_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "FK_user_logins_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_preferences",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    stay_duration = table.Column<int>(type: "integer", nullable: false),
                    budget = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    category_preference = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_preferences", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_preferences_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "FK_user_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "activities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    cost = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activities", x => x.id);
                    table.ForeignKey(
                        name: "fk_activities_cities",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "hotels",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price_per_night = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    image_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotels", x => x.id);
                    table.ForeignKey(
                        name: "fk_hotels_categories",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_hotels_cities",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "restaurants",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    cuisine = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    price_range = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restaurants", x => x.id);
                    table.ForeignKey(
                        name: "fk_restaurants_categories",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_restaurants_cities",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tourist_places",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    opening_hours = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    entry_fee = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tourist_places", x => x.id);
                    table.ForeignKey(
                        name: "fk_tourist_places_categories",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_tourist_places_cities",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    payment_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    payment_method = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    booking_id = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Pending")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_payments_bookings",
                        column: x => x.booking_id,
                        principalTable: "bookings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cart_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cart_id = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    booking_id = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_cart_items_bookings_ProductId",
                        column: x => x.ProductId,
                        principalTable: "bookings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cart_items_bookings",
                        column: x => x.booking_id,
                        principalTable: "bookings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cart_items_carts",
                        column: x => x.cart_id,
                        principalTable: "carts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hotel_rooms",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    hotel_id = table.Column<int>(type: "integer", nullable: false),
                    room_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    available = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotel_rooms", x => x.id);
                    table.ForeignKey(
                        name: "fk_hotel_rooms_hotels",
                        column: x => x.hotel_id,
                        principalTable: "hotels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "packages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    duration_days = table.Column<int>(type: "integer", nullable: false),
                    image_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    hotel_id = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_packages", x => x.id);
                    table.ForeignKey(
                        name: "fk_packages_categories",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_packages_cities",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_packages_hotels",
                        column: x => x.hotel_id,
                        principalTable: "hotels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_interactions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    item_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    item_id = table.Column<int>(type: "integer", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    TouristPlaceId = table.Column<int>(type: "integer", nullable: false),
                    ActivityId = table.Column<int>(type: "integer", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    interaction_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_interactions", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_interactions_activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "activities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_interactions_cities_CityId",
                        column: x => x.CityId,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_interactions_tourist_places_TouristPlaceId",
                        column: x => x.TouristPlaceId,
                        principalTable: "tourist_places",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_interactions_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "package_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    package_id = table.Column<int>(type: "integer", nullable: false),
                    tourist_place_id = table.Column<int>(type: "integer", nullable: false),
                    day_order = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_package_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_package_details_packages",
                        column: x => x.package_id,
                        principalTable: "packages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_package_details_tourist_places",
                        column: x => x.tourist_place_id,
                        principalTable: "tourist_places",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    package_id = table.Column<int>(type: "integer", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_packages",
                        column: x => x.package_id,
                        principalTable: "packages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reviews_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1, "Accommodation and hotels", "Hotels" },
                    { 2, "Dining and restaurants", "Restaurants" },
                    { 3, "Tourist attractions and landmarks", "Tourist Places" },
                    { 4, "Tourism packages and deals", "Packages" },
                    { 5, "Umrah and Hajj trips", "Religious Trips" },
                    { 6, "Family-friendly destinations", "Family Trips" },
                    { 7, "Beach and coastal destinations", "Beach Trips" },
                    { 8, "Adventure and outdoor activities", "Adventure" },
                    { 9, "Historical sites and monuments", "Historical" },
                    { 10, "Cultural experiences and museums", "Cultural" },
                    { 11, "Natural landscapes and parks", "Nature" },
                    { 12, "Shopping destinations and markets", "Shopping" }
                });

            migrationBuilder.InsertData(
                table: "countries",
                columns: new[] { "id", "code", "is_active", "name" },
                values: new object[,]
                {
                    { 1, "EG", true, "Egypt" },
                    { 2, "TR", true, "Turkey" },
                    { 3, "MV", true, "Maldives" },
                    { 4, "SA", true, "Saudi Arabia" }
                });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "id", "country_id", "description", "image_url", "is_active", "name" },
                values: new object[,]
                {
                    { 1001, 1, "Capital city with pyramids and museums", null, true, "Cairo" },
                    { 1002, 1, "Home of the Great Pyramids and Sphinx", null, true, "Giza" },
                    { 1003, 1, "Mediterranean pearl with ancient library", null, true, "Alexandria" },
                    { 1004, 1, "Beautiful Mediterranean beaches", null, true, "Marsa Matruh" },
                    { 1005, 1, "WWII historical site and new resort city", null, true, "El Alamein" },
                    { 1006, 1, "Luxury beach resorts along Mediterranean", null, true, "North Coast" },
                    { 1007, 1, "Ancient Thebes with Valley of Kings", null, true, "Luxor" },
                    { 1008, 1, "Nubian culture and beautiful Nile scenery", null, true, "Aswan" },
                    { 1009, 1, "Ramses II temples", null, true, "Abu Simbel" },
                    { 1010, 1, "Temple of Horus", null, true, "Edfu" },
                    { 1011, 1, "Double temple on the Nile", null, true, "Kom Ombo" },
                    { 1012, 1, "Ancient temple and Nile cruise stop", null, true, "Esna" },
                    { 1013, 1, "Popular Red Sea resort with diving", null, true, "Hurghada" },
                    { 1014, 1, "Premium resort city in South Sinai", null, true, "Sharm El Sheikh" },
                    { 1015, 1, "Laid-back diving and windsurfing destination", null, true, "Dahab" },
                    { 1016, 1, "Pristine diving spots and marine life", null, true, "Marsa Alam" },
                    { 1017, 1, "Luxury resort town near Hurghada", null, true, "El Gouna" },
                    { 1018, 1, "Exclusive resort destination", null, true, "Soma Bay" },
                    { 1019, 1, "Resort area with water sports", null, true, "Makadi Bay" },
                    { 1020, 1, "Upscale resort community", null, true, "Sahl Hasheesh" },
                    { 1021, 1, "Border resort with coral reefs", null, true, "Taba" },
                    { 1022, 1, "Quiet beaches and diving spots", null, true, "Nuweiba" },
                    { 1023, 1, "Mount Sinai and ancient monastery", null, true, "Saint Catherine" },
                    { 1024, 1, "Developing resort area", null, true, "Ras Sidr" },
                    { 1025, 1, "Close weekend beach destination", null, true, "Ain Sokhna" },
                    { 1026, 1, "Oasis with Wadi El Rayan waterfalls", null, true, "Fayoum" },
                    { 1027, 1, "Coptic monasteries in the desert", null, true, "Wadi El Natrun" },
                    { 1028, 1, "Remote desert oasis with unique culture", null, true, "Siwa Oasis" },
                    { 1029, 1, "White Desert and Black Desert gateway", null, true, "Bahariya Oasis" },
                    { 1030, 1, "Historical oasis with hot springs", null, true, "Dakhla Oasis" },
                    { 1031, 1, "Ancient temples and fortress", null, true, "Kharga Oasis" },
                    { 1032, 1, "Gateway to White Desert", null, true, "Farafra Oasis" },
                    { 1033, 1, "Suez Canal entrance city", null, true, "Port Said" },
                    { 1034, 1, "Beautiful city on Suez Canal", null, true, "Ismailia" },
                    { 1035, 1, "Strategic port city", null, true, "Suez" },
                    { 1036, 1, "Nile Delta coastal city", null, true, "Damietta" },
                    { 1037, 1, "Historical city where Rosetta Stone was found", null, true, "Rosetta (Rashid)" },
                    { 1038, 1, "Religious festivals destination", null, true, "Tanta" },
                    { 1039, 1, "Tuna el-Gebel and Beni Hassan tombs", null, true, "Minya" },
                    { 1040, 1, "Abydos temple complex", null, true, "Sohag" },
                    { 1041, 1, "Gateway to Dendera Temple", null, true, "Qena" },
                    { 2001, 2, "Historic city spanning two continents", null, true, "Istanbul" },
                    { 2002, 2, "Ottoman heritage and Uludağ ski resort", null, true, "Bursa" },
                    { 2003, 2, "Former Ottoman capital with beautiful mosques", null, true, "Edirne" },
                    { 2004, 2, "Troy ancient city and WWI memorials", null, true, "Çanakkale" },
                    { 2005, 2, "Modern coastal city with ancient Smyrna", null, true, "Izmir" },
                    { 2006, 2, "Luxury resort town with ancient ruins", null, true, "Bodrum" },
                    { 2007, 2, "Cruise port near Ephesus", null, true, "Kuşadası" },
                    { 2008, 2, "Beach resort with thermal springs", null, true, "Çeşme" },
                    { 2009, 2, "Popular beach resort and marina", null, true, "Marmaris" },
                    { 2010, 2, "Ölüdeniz Blue Lagoon and paragliding", null, true, "Fethiye" },
                    { 2011, 2, "Peaceful peninsula destination", null, true, "Datça" },
                    { 2012, 2, "Ancient cities of Ephesus and Aphrodisias", null, true, "Aydın" },
                    { 2013, 2, "Tourism capital of Turkish Riviera", null, true, "Antalya" },
                    { 2014, 2, "Beach resort with historical castle", null, true, "Alanya" },
                    { 2015, 2, "Ancient ruins on beautiful beaches", null, true, "Side" },
                    { 2016, 2, "Diving paradise and boutique town", null, true, "Kaş" },
                    { 2017, 2, "Upscale hillside resort town", null, true, "Kalkan" },
                    { 2018, 2, "Mountain-backed beach resort", null, true, "Kemer" },
                    { 2019, 2, "Golf and luxury resort destination", null, true, "Belek" },
                    { 2020, 2, "Capital city with Anıtkabir mausoleum", null, true, "Ankara" },
                    { 2021, 2, "Fairy chimneys and hot air balloons", null, true, "Cappadocia (Nevşehir)" },
                    { 2022, 2, "Cave hotels and rock formations", null, true, "Göreme" },
                    { 2023, 2, "Wine region in Cappadocia", null, true, "Ürgüp" },
                    { 2024, 2, "Mevlana Museum and Whirling Dervishes", null, true, "Konya" },
                    { 2025, 2, "Gateway to Cappadocia with Mount Erciyes", null, true, "Kayseri" },
                    { 2026, 2, "Sumela Monastery and Uzungöl lake", null, true, "Trabzon" },
                    { 2027, 2, "Tea plantations and lush green mountains", null, true, "Rize" },
                    { 2028, 2, "Highland plateau with hot springs", null, true, "Ayder" },
                    { 2029, 2, "Black Sea coastal city", null, true, "Samsun" },
                    { 2030, 2, "Historic Black Sea port", null, true, "Sinop" },
                    { 2031, 2, "Lake Van and ancient Armenian church", null, true, "Van" },
                    { 2032, 2, "Ski resort and historical city", null, true, "Erzurum" },
                    { 2033, 2, "Ancient city walls and Tigris River", null, true, "Diyarbakır" },
                    { 2034, 2, "Stone architecture and ancient monasteries", null, true, "Mardin" },
                    { 2035, 2, "Birthplace of Abraham, Göbekli Tepe", null, true, "Şanlıurfa" },
                    { 2036, 2, "White travertine terraces and Hierapolis", null, true, "Pamukkale (Denizli)" },
                    { 2037, 2, "Thermal springs and castle", null, true, "Afyonkarahisar" },
                    { 2038, 2, "Modern university city with river parks", null, true, "Eskişehir" },
                    { 3001, 3, "Capital city and main gateway", null, true, "Malé" },
                    { 3002, 3, "Reclaimed island with hotels and beaches", null, true, "Hulhumalé" },
                    { 3003, 3, "Closest luxury resorts to airport", null, true, "North Malé Atoll" },
                    { 3004, 3, "Surfing and local island experience", null, true, "Thulusdhoo" },
                    { 3005, 3, "Budget-friendly guesthouse island", null, true, "Maafushi" },
                    { 3006, 3, "Local island with beautiful beaches", null, true, "Gulhi" },
                    { 3007, 3, "Diving and surfing paradise", null, true, "South Malé Atoll" },
                    { 3008, 3, "Surfing and diving destination", null, true, "Guraidhoo" },
                    { 3009, 3, "Whale shark diving hotspot", null, true, "Ari Atoll (North)" },
                    { 3010, 3, "Luxury resorts and manta rays", null, true, "Ari Atoll (South)" },
                    { 3011, 3, "Hammerhead shark diving", null, true, "Rasdhoo" },
                    { 3012, 3, "Agricultural island with beaches", null, true, "Thoddoo" },
                    { 3013, 3, "UNESCO site with manta ray season", null, true, "Baa Atoll" },
                    { 3014, 3, "Gateway to Hanifaru Bay", null, true, "Dharavandhoo" },
                    { 3015, 3, "Atoll capital with local culture", null, true, "Eydhafushi" },
                    { 3016, 3, "Pristine dive sites", null, true, "Lhaviyani Atoll" },
                    { 3017, 3, "Atoll capital", null, true, "Naifaru" },
                    { 3018, 3, "Remote luxury resorts", null, true, "Raa Atoll" },
                    { 3019, 3, "Exclusive resorts", null, true, "Noonu Atoll" },
                    { 3020, 3, "Beautiful lagoons and diving", null, true, "Dhaalu Atoll" },
                    { 3021, 3, "Shark diving destination", null, true, "Vaavu Atoll" },
                    { 3022, 3, "Remote and unspoiled", null, true, "Laamu Atoll" },
                    { 3023, 3, "Second largest city, unique culture", null, true, "Addu City (Gan)" },
                    { 3024, 3, "Most populous island in Addu", null, true, "Hithadhoo" },
                    { 3025, 3, "Tiger shark diving", null, true, "Fuvahmulah" },
                    { 4001, 4, "Holiest city in Islam, Kaaba and Hajj", null, true, "Makkah (Mecca)" },
                    { 4002, 4, "Prophet's Mosque and Islamic heritage", null, true, "Madinah (Medina)" },
                    { 4003, 4, "Gateway to Makkah, Red Sea port city", null, true, "Jeddah" },
                    { 4004, 4, "Summer resort in mountains near Makkah", null, true, "Taif" },
                    { 4005, 4, "Red Sea diving and beaches", null, true, "Yanbu" },
                    { 4006, 4, "Coastal city between Jeddah and Madinah", null, true, "Rabigh" },
                    { 4007, 4, "Ancient Nabatean city of Hegra (Madain Saleh)", null, true, "AlUla" },
                    { 4008, 4, "Gateway to northwestern Saudi Arabia", null, true, "Tabuk" },
                    { 4009, 4, "Futuristic mega-city project", null, true, "NEOM" },
                    { 4010, 4, "Capital city with modern attractions", null, true, "Riyadh" },
                    { 4011, 4, "UNESCO heritage site, birthplace of Saudi state", null, true, "Diriyah" },
                    { 4012, 4, "Agricultural region near Riyadh", null, true, "Al Kharj" },
                    { 4013, 4, "Major port city on Arabian Gulf", null, true, "Dammam" },
                    { 4014, 4, "Modern city with corniche and shopping", null, true, "Al Khobar" },
                    { 4015, 4, "Oil industry hub", null, true, "Dhahran" },
                    { 4016, 4, "UNESCO heritage oasis with date palms", null, true, "Al Ahsa" },
                    { 4017, 4, "Industrial city with beaches", null, true, "Jubail" },
                    { 4018, 4, "Historic coastal city", null, true, "Qatif" },
                    { 4019, 4, "Popular beach destination near Khobar", null, true, "Half Moon Bay" },
                    { 4020, 4, "Mountain resort city with cable cars", null, true, "Abha" },
                    { 4021, 4, "Twin city to Abha", null, true, "Khamis Mushait" },
                    { 4022, 4, "Coastal city near Yemen border", null, true, "Jizan" },
                    { 4023, 4, "Pristine islands in Red Sea", null, true, "Farasan Islands" },
                    { 4024, 4, "Historical city near Yemen", null, true, "Najran" },
                    { 4025, 4, "Ancient rock art and historical sites", null, true, "Hail" },
                    { 4026, 4, "Northern region capital", null, true, "Sakaka" },
                    { 4027, 4, "Maldives of Saudi Arabia - pristine islands", null, true, "Umluj" },
                    { 4028, 4, "Luxury tourism destination under development", null, true, "Red Sea Project" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_activities_city_id",
                table: "activities",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_bookings_type_item",
                table: "bookings",
                columns: new[] { "booking_type", "booking_item_id" });

            migrationBuilder.CreateIndex(
                name: "IX_bookings_user_id",
                table: "bookings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_items_booking_id",
                table: "cart_items",
                column: "booking_id");

            migrationBuilder.CreateIndex(
                name: "ix_cart_items_cart_booking",
                table: "cart_items",
                columns: new[] { "cart_id", "booking_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cart_items_ProductId",
                table: "cart_items",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "ix_carts_user_id",
                table: "carts",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_categories_name",
                table: "categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_chat_messages_user_id",
                table: "chat_messages",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_cities_country_id",
                table: "cities",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_countries_code",
                table: "countries",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_countries_name",
                table: "countries",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_hotel_rooms_hotel_id",
                table: "hotel_rooms",
                column: "hotel_id");

            migrationBuilder.CreateIndex(
                name: "IX_hotels_CategoryId",
                table: "hotels",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_hotels_city_id",
                table: "hotels",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_package_details_package_id",
                table: "package_details",
                column: "package_id");

            migrationBuilder.CreateIndex(
                name: "IX_package_details_tourist_place_id",
                table: "package_details",
                column: "tourist_place_id");

            migrationBuilder.CreateIndex(
                name: "IX_packages_CategoryId",
                table: "packages",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_packages_city_id",
                table: "packages",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_packages_hotel_id",
                table: "packages",
                column: "hotel_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_booking_id",
                table: "payments",
                column: "booking_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_recommendations_user_id",
                table: "recommendations",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId1",
                table: "RefreshTokens",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_restaurants_CategoryId",
                table: "restaurants",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_restaurants_city_id",
                table: "restaurants",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_package_user",
                table: "reviews",
                columns: new[] { "package_id", "user_id" });

            migrationBuilder.CreateIndex(
                name: "IX_reviews_user_id",
                table: "reviews",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_claims_role_id",
                table: "role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_normalized_name",
                table: "roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_search_logs_user_id",
                table: "search_logs",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tourist_places_CategoryId",
                table: "tourist_places",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tourist_places_city_id",
                table: "tourist_places",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_claims_user_id",
                table: "user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_interactions_ActivityId",
                table: "user_interactions",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_user_interactions_CityId",
                table: "user_interactions",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_user_interactions_TouristPlaceId",
                table: "user_interactions",
                column: "TouristPlaceId");

            migrationBuilder.CreateIndex(
                name: "ix_user_interactions_user_item",
                table: "user_interactions",
                columns: new[] { "user_id", "item_type", "item_id" });

            migrationBuilder.CreateIndex(
                name: "ix_user_logins_user_id",
                table: "user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_preferences_user_id",
                table: "user_preferences",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "users",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_items");

            migrationBuilder.DropTable(
                name: "chat_messages");

            migrationBuilder.DropTable(
                name: "hotel_rooms");

            migrationBuilder.DropTable(
                name: "package_details");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "recommendations");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "restaurants");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "role_claims");

            migrationBuilder.DropTable(
                name: "search_logs");

            migrationBuilder.DropTable(
                name: "user_claims");

            migrationBuilder.DropTable(
                name: "user_interactions");

            migrationBuilder.DropTable(
                name: "user_logins");

            migrationBuilder.DropTable(
                name: "user_preferences");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "user_tokens");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "packages");

            migrationBuilder.DropTable(
                name: "activities");

            migrationBuilder.DropTable(
                name: "tourist_places");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "hotels");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "countries");
        }
    }
}
