using Fas7ny.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace TourismApp.Data
{
    public class TourismDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public TourismDbContext(DbContextOptions<TourismDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
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

            // User
            modelBuilder.Entity<User>(entity =>
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
        }
    }
}