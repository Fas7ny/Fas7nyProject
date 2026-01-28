using Fas7ny.Domain.Entities;
using Fas7ny.Infrastructure.Data.SeedData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace TourismApp.Data
{

    public class TourismDbContext
        : IdentityDbContext<ApplicationUser>

    {
        public TourismDbContext(DbContextOptions<TourismDbContext> options)
            : base(options)
        {
        }



        public DbSet<City> Cities { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<TouristPlace> TouristPlaces { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageDetail> PackageDetails { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<SearchLog> SearchLogs { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();

                entity.HasIndex(e => e.Name).IsUnique().HasDatabaseName("ix_categories_name");
            });

            // User
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.FullName).HasColumnName("full_name").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
                entity.Property(e => e.PasswordHash).HasColumnName("password_hash").IsRequired();
                entity.Property(e => e.Role).HasColumnName("role").HasMaxLength(50).IsRequired().HasDefaultValue("User");
                entity.Property(e => e.PreferencesJson).HasColumnName("preferences_json").HasColumnType("jsonb");
                entity.HasIndex(e => e.Email).IsUnique().HasDatabaseName("ix_users_email");
            });

            // City
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("cities");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Country).HasColumnName("country").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasColumnName("description").HasColumnType("text");
                entity.Property(e => e.ImageUrl).HasColumnName("image_url").HasMaxLength(500);
            });

            // Hotel
            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("hotels");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
                entity.Property(e => e.Address).HasColumnName("address").HasMaxLength(500).IsRequired();
                entity.Property(e => e.Description).HasColumnName("description").HasColumnType("text");
                entity.Property(e => e.PricePerNight).HasColumnName("price_per_night").HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.ImageUrl).HasColumnName("image_url").HasMaxLength(500);
                entity.Property(e => e.CityId).HasColumnName("city_id").IsRequired();

                entity.HasOne(e => e.City)
                    .WithMany(c => c.Hotels)
                    .HasForeignKey(e => e.CityId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_hotels_cities");

                   entity.HasOne(h => h.Category)
                          .WithMany(c => c.Hotels)
                          .HasForeignKey(h => h.CategoryId)
                          .OnDelete(DeleteBehavior.Restrict)
                          .HasConstraintName("fk_hotels_categories");
               });

            // HotelRoom
            modelBuilder.Entity<HotelRoom>(entity =>
            {
                entity.ToTable("hotel_rooms");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.HotelId).HasColumnName("hotel_id").IsRequired();
                entity.Property(e => e.RoomType).HasColumnName("room_type").HasMaxLength(50).IsRequired();
                entity.Property(e => e.Capacity).HasColumnName("capacity").IsRequired();
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Available).HasColumnName("available").HasDefaultValue(true).IsRequired();

                entity.HasOne(e => e.Hotel)
                    .WithMany(h => h.HotelRooms)
                    .HasForeignKey(e => e.HotelId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_hotel_rooms_hotels");
            });

            // Restaurant
            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("restaurants");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
                entity.Property(e => e.Cuisine).HasColumnName("cuisine").HasMaxLength(100);
                entity.Property(e => e.Description).HasColumnName("description").HasColumnType("text");
                entity.Property(e => e.PriceRange).HasColumnName("price_range").HasMaxLength(50);
                entity.Property(e => e.CityId).HasColumnName("city_id").IsRequired();

                entity.HasOne(e => e.City)
                    .WithMany(c => c.Restaurants)
                    .HasForeignKey(e => e.CityId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_restaurants_cities");

                entity.HasOne(r => r.Category)
                      .WithMany(c => c.Restaurants)
                      .HasForeignKey(r => r.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("fk_restaurants_categories");
            });

            // TouristPlace
            modelBuilder.Entity<TouristPlace>(entity =>
            {
                entity.ToTable("tourist_places");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
                entity.Property(e => e.Description).HasColumnName("description").HasColumnType("text");
                entity.Property(e => e.ImageUrl).HasColumnName("image_url").HasMaxLength(500);
                entity.Property(e => e.OpeningHours).HasColumnName("opening_hours").HasMaxLength(200);
                entity.Property(e => e.EntryFee).HasColumnName("entry_fee").HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.CityId).HasColumnName("city_id").IsRequired();

                entity.HasOne(e => e.City)
                    .WithMany(c => c.TouristPlaces)
                    .HasForeignKey(e => e.CityId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_tourist_places_cities");

                entity.HasOne(tp => tp.Category)
                      .WithMany(c => c.TouristPlaces)
                      .HasForeignKey(tp => tp.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("fk_tourist_places_categories");
            });

            // Package
            modelBuilder.Entity<Package>(entity =>
            {
                entity.ToTable("packages");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
                entity.Property(e => e.Description).HasColumnName("description").HasColumnType("text");
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.DurationDays).HasColumnName("duration_days").IsRequired();
                entity.Property(e => e.ImageUrl).HasColumnName("image_url").HasMaxLength(500);
                entity.Property(e => e.CityId).HasColumnName("city_id").IsRequired();
                entity.Property(e => e.HotelId).HasColumnName("hotel_id").IsRequired();

                entity.HasOne(e => e.City)
                    .WithMany(c => c.Packages)
                    .HasForeignKey(e => e.CityId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_packages_cities");

                entity.HasOne(e => e.Hotel)
                    .WithMany(h => h.Packages)
                    .HasForeignKey(e => e.HotelId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_packages_hotels");

                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Packages)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("fk_packages_categories");
            });

            // PackageDetail
            modelBuilder.Entity<PackageDetail>(entity =>
            {
                entity.ToTable("package_details");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.PackageId).HasColumnName("package_id").IsRequired();
                entity.Property(e => e.TouristPlaceId).HasColumnName("tourist_place_id").IsRequired();
                entity.Property(e => e.DayOrder).HasColumnName("day_order").IsRequired();
                entity.Property(e => e.Description).HasColumnName("description").HasColumnType("text");

                entity.HasOne(e => e.Package)
                    .WithMany(p => p.PackageDetails)
                    .HasForeignKey(e => e.PackageId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_package_details_packages");

                entity.HasOne(e => e.TouristPlace)
                    .WithMany(tp => tp.PackageDetails)
                    .HasForeignKey(e => e.TouristPlaceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_package_details_tourist_places");
            });

            // Booking
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("bookings");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
                entity.Property(e => e.BookingType).HasColumnName("booking_type").HasMaxLength(50).IsRequired();
                entity.Property(e => e.BookingItemId).HasColumnName("booking_item_id").IsRequired();
                entity.Property(e => e.StartDate).HasColumnName("start_date").HasColumnType("timestamp").IsRequired();
                entity.Property(e => e.EndDate).HasColumnName("end_date").HasColumnType("timestamp").IsRequired();
                entity.Property(e => e.TotalPrice).HasColumnName("total_price").HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(50).IsRequired().HasDefaultValue("Pending");

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Bookings)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_bookings_users");
            });

            // ChatMessage
            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.ToTable("chat_messages");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
                entity.Property(e => e.MessageText).HasColumnName("message_text").HasColumnType("text").IsRequired();
                entity.Property(e => e.ResponseText).HasColumnName("response_text").HasColumnType("text");
                entity.Property(e => e.Timestamp).HasColumnName("timestamp").HasColumnType("timestamp").IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(e => e.User)
                    .WithMany(u => u.ChatMessages)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_chat_messages_users");
            });

            // SearchLog
            modelBuilder.Entity<SearchLog>(entity =>
            {
                entity.ToTable("search_logs");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
                entity.Property(e => e.Query).HasColumnName("query").HasMaxLength(500).IsRequired();
                entity.Property(e => e.SearchDate).HasColumnName("search_date").HasColumnType("timestamp").IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(e => e.User)
                    .WithMany(u => u.SearchLogs)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_search_logs_users");
            });

            // Recommendation
            modelBuilder.Entity<Recommendation>(entity =>
            {
                entity.ToTable("recommendations");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
                entity.Property(e => e.RecommendedItemType).HasColumnName("recommended_item_type").HasMaxLength(50).IsRequired();
                entity.Property(e => e.ItemId).HasColumnName("item_id").IsRequired();
                entity.Property(e => e.Reason).HasColumnName("reason").HasColumnType("text");

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Recommendations)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_recommendations_users");
            });

            // Payment
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payments");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Amount).HasColumnName("amount").HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.PaymentDate).HasColumnName("payment_date").HasColumnType("timestamp").IsRequired();
                entity.Property(e => e.PaymentMethod).HasColumnName("payment_method").HasMaxLength(50).IsRequired();
                entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(50).IsRequired().HasDefaultValue("Pending");
                entity.Property(e => e.BookingId).HasColumnName("booking_id").IsRequired();

                // ✅ FIXED: Changed 'Book' to 'Booking'
                entity.HasOne(e => e.Book)
                    .WithOne(b => b.Payment)
                    .HasForeignKey<Payment>(e => e.BookingId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_payments_bookings");

                entity.HasIndex(e => e.BookingId).IsUnique().HasDatabaseName("ix_payments_booking_id");
            });

            // Review
            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("reviews");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.PackageId).HasColumnName("package_id").IsRequired();
                entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
                // ✅ FIXED: This line - DON'T map navigation property 'User', map data property if exists
                // REMOVED: entity.Property(e => e.User).HasColumnName("user_name")...
                entity.Property(e => e.Rating).HasColumnName("rating").IsRequired();
                entity.Property(e => e.Comment).HasColumnName("comment").HasColumnType("text");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(e => e.Package)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(e => e.PackageId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_reviews_packages");

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Reviews)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_reviews_users");

                entity.HasIndex(e => new { e.PackageId, e.UserId }).HasDatabaseName("ix_reviews_package_user");
            });

            // UserInteraction
            modelBuilder.Entity<UserInteraction>(entity =>
            {
                entity.ToTable("user_interactions");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
                entity.Property(e => e.Username).HasColumnName("username").HasMaxLength(100).IsRequired();
                entity.Property(e => e.ItemType).HasColumnName("item_type").HasMaxLength(50).IsRequired();
                entity.Property(e => e.ItemId).HasColumnName("item_id").IsRequired();
                entity.Property(e => e.InteractionType).HasColumnName("interaction_type").HasMaxLength(50).IsRequired();
                entity.Property(e => e.Timestamp).HasColumnName("timestamp").HasColumnType("timestamp").HasDefaultValueSql("CURRENT_TIMESTAMP");

                // ✅ FIXED: Changed 'user' to 'User'
                entity.HasOne(e => e.User)
                    .WithMany(u => u.UserInteractions)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_user_interactions_users");

                entity.HasIndex(e => new { e.UserId, e.ItemType, e.ItemId }).HasDatabaseName("ix_user_interactions_user_item");
            });

            // UserPreference
            modelBuilder.Entity<UserPreference>(entity =>
            {
                entity.ToTable("user_preferences");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
                entity.Property(e => e.Username).HasColumnName("username").HasMaxLength(100).IsRequired();
                entity.Property(e => e.StayDuration).HasColumnName("stay_duration").IsRequired();
                entity.Property(e => e.Budget).HasColumnName("budget").HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.CategoryPreference).HasColumnName("category_preference").HasMaxLength(50).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").HasDefaultValueSql("CURRENT_TIMESTAMP");

                // ✅ FIXED: Changed 'user' to 'User'
                entity.HasOne(e => e.User)
                    .WithMany(u => u.UserPreferences)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_user_preferences_users");

                entity.HasIndex(e => e.UserId).HasDatabaseName("ix_user_preferences_user_id");
            });

            // Carts
            modelBuilder.Entity<Carts>(entity =>
            {
                entity.ToTable("carts");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp");

                entity.HasOne(e => e.User)
                    .WithOne(u => u.Cart)
                    .HasForeignKey<Carts>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_carts_users");

                entity.HasIndex(e => e.UserId).IsUnique().HasDatabaseName("ix_carts_user_id");
            });

            // CartItems
            modelBuilder.Entity<CartItems>(entity =>
            {
                entity.ToTable("cart_items");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.CartId).HasColumnName("cart_id").IsRequired();
                entity.Property(e => e.BookingId).HasColumnName("booking_id").IsRequired();
                entity.Property(e => e.Quantity).HasColumnName("quantity").IsRequired().HasDefaultValue(1);
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(18,2)").IsRequired();

                entity.HasOne(e => e.Cart)
                    .WithMany(c => c.CartItems)
                    .HasForeignKey(e => e.CartId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_cart_items_carts");

                entity.HasOne(e => e.Booking)
                    .WithMany(b => b.CartItems)
                    .HasForeignKey(e => e.BookingId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_cart_items_bookings");

                entity.HasIndex(e => new { e.CartId, e.BookingId }).IsUnique().HasDatabaseName("ix_cart_items_cart_booking");
            });
            SeedData.Apply(modelBuilder);

        }
    }
}