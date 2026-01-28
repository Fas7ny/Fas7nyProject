using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ______________.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Code", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "EG", true, "Egypt" },
                    { 2, "TR", true, "Turkey" },
                    { 3, "MV", true, "Maldives" },
                    { 4, "SA", true, "Saudi Arabia" }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "Description", "name" },
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
                table: "cities",
                columns: new[] { "id", "country", "CountryId", "description", "image_url", "IsActive", "name" },
                values: new object[,]
                {
                    { 1001, "", 1, "Capital city with pyramids and museums", null, true, "Cairo" },
                    { 1002, "", 1, "Home of the Great Pyramids and Sphinx", null, true, "Giza" },
                    { 1003, "", 1, "Mediterranean pearl with ancient library", null, true, "Alexandria" },
                    { 1004, "", 1, "Beautiful Mediterranean beaches", null, true, "Marsa Matruh" },
                    { 1005, "", 1, "WWII historical site and new resort city", null, true, "El Alamein" },
                    { 1006, "", 1, "Luxury beach resorts along Mediterranean", null, true, "North Coast" },
                    { 1007, "", 1, "Ancient Thebes with Valley of Kings", null, true, "Luxor" },
                    { 1008, "", 1, "Nubian culture and beautiful Nile scenery", null, true, "Aswan" },
                    { 1009, "", 1, "Ramses II temples", null, true, "Abu Simbel" },
                    { 1010, "", 1, "Temple of Horus", null, true, "Edfu" },
                    { 1011, "", 1, "Double temple on the Nile", null, true, "Kom Ombo" },
                    { 1012, "", 1, "Ancient temple and Nile cruise stop", null, true, "Esna" },
                    { 1013, "", 1, "Popular Red Sea resort with diving", null, true, "Hurghada" },
                    { 1014, "", 1, "Premium resort city in South Sinai", null, true, "Sharm El Sheikh" },
                    { 1015, "", 1, "Laid-back diving and windsurfing destination", null, true, "Dahab" },
                    { 1016, "", 1, "Pristine diving spots and marine life", null, true, "Marsa Alam" },
                    { 1017, "", 1, "Luxury resort town near Hurghada", null, true, "El Gouna" },
                    { 1018, "", 1, "Exclusive resort destination", null, true, "Soma Bay" },
                    { 1019, "", 1, "Resort area with water sports", null, true, "Makadi Bay" },
                    { 1020, "", 1, "Upscale resort community", null, true, "Sahl Hasheesh" },
                    { 1021, "", 1, "Border resort with coral reefs", null, true, "Taba" },
                    { 1022, "", 1, "Quiet beaches and diving spots", null, true, "Nuweiba" },
                    { 1023, "", 1, "Mount Sinai and ancient monastery", null, true, "Saint Catherine" },
                    { 1024, "", 1, "Developing resort area", null, true, "Ras Sidr" },
                    { 1025, "", 1, "Close weekend beach destination", null, true, "Ain Sokhna" },
                    { 1026, "", 1, "Oasis with Wadi El Rayan waterfalls", null, true, "Fayoum" },
                    { 1027, "", 1, "Coptic monasteries in the desert", null, true, "Wadi El Natrun" },
                    { 1028, "", 1, "Remote desert oasis with unique culture", null, true, "Siwa Oasis" },
                    { 1029, "", 1, "White Desert and Black Desert gateway", null, true, "Bahariya Oasis" },
                    { 1030, "", 1, "Historical oasis with hot springs", null, true, "Dakhla Oasis" },
                    { 1031, "", 1, "Ancient temples and fortress", null, true, "Kharga Oasis" },
                    { 1032, "", 1, "Gateway to White Desert", null, true, "Farafra Oasis" },
                    { 1033, "", 1, "Suez Canal entrance city", null, true, "Port Said" },
                    { 1034, "", 1, "Beautiful city on Suez Canal", null, true, "Ismailia" },
                    { 1035, "", 1, "Strategic port city", null, true, "Suez" },
                    { 1036, "", 1, "Nile Delta coastal city", null, true, "Damietta" },
                    { 1037, "", 1, "Historical city where Rosetta Stone was found", null, true, "Rosetta (Rashid)" },
                    { 1038, "", 1, "Religious festivals destination", null, true, "Tanta" },
                    { 1039, "", 1, "Tuna el-Gebel and Beni Hassan tombs", null, true, "Minya" },
                    { 1040, "", 1, "Abydos temple complex", null, true, "Sohag" },
                    { 1041, "", 1, "Gateway to Dendera Temple", null, true, "Qena" },
                    { 2001, "", 2, "Historic city spanning two continents", null, true, "Istanbul" },
                    { 2002, "", 2, "Ottoman heritage and Uludağ ski resort", null, true, "Bursa" },
                    { 2003, "", 2, "Former Ottoman capital with beautiful mosques", null, true, "Edirne" },
                    { 2004, "", 2, "Troy ancient city and WWI memorials", null, true, "Çanakkale" },
                    { 2005, "", 2, "Modern coastal city with ancient Smyrna", null, true, "Izmir" },
                    { 2006, "", 2, "Luxury resort town with ancient ruins", null, true, "Bodrum" },
                    { 2007, "", 2, "Cruise port near Ephesus", null, true, "Kuşadası" },
                    { 2008, "", 2, "Beach resort with thermal springs", null, true, "Çeşme" },
                    { 2009, "", 2, "Popular beach resort and marina", null, true, "Marmaris" },
                    { 2010, "", 2, "Ölüdeniz Blue Lagoon and paragliding", null, true, "Fethiye" },
                    { 2011, "", 2, "Peaceful peninsula destination", null, true, "Datça" },
                    { 2012, "", 2, "Ancient cities of Ephesus and Aphrodisias", null, true, "Aydın" },
                    { 2013, "", 2, "Tourism capital of Turkish Riviera", null, true, "Antalya" },
                    { 2014, "", 2, "Beach resort with historical castle", null, true, "Alanya" },
                    { 2015, "", 2, "Ancient ruins on beautiful beaches", null, true, "Side" },
                    { 2016, "", 2, "Diving paradise and boutique town", null, true, "Kaş" },
                    { 2017, "", 2, "Upscale hillside resort town", null, true, "Kalkan" },
                    { 2018, "", 2, "Mountain-backed beach resort", null, true, "Kemer" },
                    { 2019, "", 2, "Golf and luxury resort destination", null, true, "Belek" },
                    { 2020, "", 2, "Capital city with Anıtkabir mausoleum", null, true, "Ankara" },
                    { 2021, "", 2, "Fairy chimneys and hot air balloons", null, true, "Cappadocia (Nevşehir)" },
                    { 2022, "", 2, "Cave hotels and rock formations", null, true, "Göreme" },
                    { 2023, "", 2, "Wine region in Cappadocia", null, true, "Ürgüp" },
                    { 2024, "", 2, "Mevlana Museum and Whirling Dervishes", null, true, "Konya" },
                    { 2025, "", 2, "Gateway to Cappadocia with Mount Erciyes", null, true, "Kayseri" },
                    { 2026, "", 2, "Sumela Monastery and Uzungöl lake", null, true, "Trabzon" },
                    { 2027, "", 2, "Tea plantations and lush green mountains", null, true, "Rize" },
                    { 2028, "", 2, "Highland plateau with hot springs", null, true, "Ayder" },
                    { 2029, "", 2, "Black Sea coastal city", null, true, "Samsun" },
                    { 2030, "", 2, "Historic Black Sea port", null, true, "Sinop" },
                    { 2031, "", 2, "Lake Van and ancient Armenian church", null, true, "Van" },
                    { 2032, "", 2, "Ski resort and historical city", null, true, "Erzurum" },
                    { 2033, "", 2, "Ancient city walls and Tigris River", null, true, "Diyarbakır" },
                    { 2034, "", 2, "Stone architecture and ancient monasteries", null, true, "Mardin" },
                    { 2035, "", 2, "Birthplace of Abraham, Göbekli Tepe", null, true, "Şanlıurfa" },
                    { 2036, "", 2, "White travertine terraces and Hierapolis", null, true, "Pamukkale (Denizli)" },
                    { 2037, "", 2, "Thermal springs and castle", null, true, "Afyonkarahisar" },
                    { 2038, "", 2, "Modern university city with river parks", null, true, "Eskişehir" },
                    { 3001, "", 3, "Capital city and main gateway", null, true, "Malé" },
                    { 3002, "", 3, "Reclaimed island with hotels and beaches", null, true, "Hulhumalé" },
                    { 3003, "", 3, "Closest luxury resorts to airport", null, true, "North Malé Atoll" },
                    { 3004, "", 3, "Surfing and local island experience", null, true, "Thulusdhoo" },
                    { 3005, "", 3, "Budget-friendly guesthouse island", null, true, "Maafushi" },
                    { 3006, "", 3, "Local island with beautiful beaches", null, true, "Gulhi" },
                    { 3007, "", 3, "Diving and surfing paradise", null, true, "South Malé Atoll" },
                    { 3008, "", 3, "Surfing and diving destination", null, true, "Guraidhoo" },
                    { 3009, "", 3, "Whale shark diving hotspot", null, true, "Ari Atoll (North)" },
                    { 3010, "", 3, "Luxury resorts and manta rays", null, true, "Ari Atoll (South)" },
                    { 3011, "", 3, "Hammerhead shark diving", null, true, "Rasdhoo" },
                    { 3012, "", 3, "Agricultural island with beaches", null, true, "Thoddoo" },
                    { 3013, "", 3, "UNESCO site with manta ray season", null, true, "Baa Atoll" },
                    { 3014, "", 3, "Gateway to Hanifaru Bay", null, true, "Dharavandhoo" },
                    { 3015, "", 3, "Atoll capital with local culture", null, true, "Eydhafushi" },
                    { 3016, "", 3, "Pristine dive sites", null, true, "Lhaviyani Atoll" },
                    { 3017, "", 3, "Atoll capital", null, true, "Naifaru" },
                    { 3018, "", 3, "Remote luxury resorts", null, true, "Raa Atoll" },
                    { 3019, "", 3, "Exclusive resorts", null, true, "Noonu Atoll" },
                    { 3020, "", 3, "Beautiful lagoons and diving", null, true, "Dhaalu Atoll" },
                    { 3021, "", 3, "Shark diving destination", null, true, "Vaavu Atoll" },
                    { 3022, "", 3, "Remote and unspoiled", null, true, "Laamu Atoll" },
                    { 3023, "", 3, "Second largest city, unique culture", null, true, "Addu City (Gan)" },
                    { 3024, "", 3, "Most populous island in Addu", null, true, "Hithadhoo" },
                    { 3025, "", 3, "Tiger shark diving", null, true, "Fuvahmulah" },
                    { 4001, "", 4, "Holiest city in Islam, Kaaba and Hajj", null, true, "Makkah (Mecca)" },
                    { 4002, "", 4, "Prophet's Mosque and Islamic heritage", null, true, "Madinah (Medina)" },
                    { 4003, "", 4, "Gateway to Makkah, Red Sea port city", null, true, "Jeddah" },
                    { 4004, "", 4, "Summer resort in mountains near Makkah", null, true, "Taif" },
                    { 4005, "", 4, "Red Sea diving and beaches", null, true, "Yanbu" },
                    { 4006, "", 4, "Coastal city between Jeddah and Madinah", null, true, "Rabigh" },
                    { 4007, "", 4, "Ancient Nabatean city of Hegra (Madain Saleh)", null, true, "AlUla" },
                    { 4008, "", 4, "Gateway to northwestern Saudi Arabia", null, true, "Tabuk" },
                    { 4009, "", 4, "Futuristic mega-city project", null, true, "NEOM" },
                    { 4010, "", 4, "Capital city with modern attractions", null, true, "Riyadh" },
                    { 4011, "", 4, "UNESCO heritage site, birthplace of Saudi state", null, true, "Diriyah" },
                    { 4012, "", 4, "Agricultural region near Riyadh", null, true, "Al Kharj" },
                    { 4013, "", 4, "Major port city on Arabian Gulf", null, true, "Dammam" },
                    { 4014, "", 4, "Modern city with corniche and shopping", null, true, "Al Khobar" },
                    { 4015, "", 4, "Oil industry hub", null, true, "Dhahran" },
                    { 4016, "", 4, "UNESCO heritage oasis with date palms", null, true, "Al Ahsa" },
                    { 4017, "", 4, "Industrial city with beaches", null, true, "Jubail" },
                    { 4018, "", 4, "Historic coastal city", null, true, "Qatif" },
                    { 4019, "", 4, "Popular beach destination near Khobar", null, true, "Half Moon Bay" },
                    { 4020, "", 4, "Mountain resort city with cable cars", null, true, "Abha" },
                    { 4021, "", 4, "Twin city to Abha", null, true, "Khamis Mushait" },
                    { 4022, "", 4, "Coastal city near Yemen border", null, true, "Jizan" },
                    { 4023, "", 4, "Pristine islands in Red Sea", null, true, "Farasan Islands" },
                    { 4024, "", 4, "Historical city near Yemen", null, true, "Najran" },
                    { 4025, "", 4, "Ancient rock art and historical sites", null, true, "Hail" },
                    { 4026, "", 4, "Northern region capital", null, true, "Sakaka" },
                    { 4027, "", 4, "Maldives of Saudi Arabia - pristine islands", null, true, "Umluj" },
                    { 4028, "", 4, "Luxury tourism destination under development", null, true, "Red Sea Project" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_cities_CountryId",
                table: "cities",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_cities_Country_CountryId",
                table: "cities",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cities_Country_CountryId",
                table: "cities");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropIndex(
                name: "IX_cities_CountryId",
                table: "cities");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1016);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1018);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1019);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1020);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1021);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1022);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1023);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1024);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1025);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1026);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1027);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1028);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1029);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1030);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1031);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1032);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1033);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1034);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1035);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1036);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1037);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1038);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1039);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1040);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1041);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2001);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2002);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2003);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2004);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2005);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2006);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2007);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2008);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2009);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2010);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2011);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2012);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2013);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2014);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2015);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2016);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2017);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2018);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2019);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2020);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2021);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2022);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2023);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2024);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2025);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2026);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2027);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2028);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2029);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2030);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2031);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2032);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2033);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2034);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2035);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2036);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2037);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2038);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3001);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3002);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3003);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3004);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3005);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3006);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3007);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3008);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3009);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3010);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3011);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3012);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3013);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3014);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3015);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3016);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3017);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3018);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3019);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3020);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3021);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3022);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3023);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3024);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3025);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4001);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4002);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4003);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4004);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4005);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4006);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4007);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4008);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4009);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4010);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4011);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4012);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4013);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4014);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4015);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4016);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4017);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4018);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4019);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4020);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4021);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4022);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4023);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4024);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4025);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4026);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4027);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4028);
        }
    }
}
