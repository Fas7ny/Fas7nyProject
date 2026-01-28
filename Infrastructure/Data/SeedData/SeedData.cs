using Fas7ny.Domain.Entities;
using Fas7ny.Domain.Entities.Fas7ny.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fas7ny.Infrastructure.Data.SeedData
{
    public static class SeedData
    {
        public static void Apply(ModelBuilder modelBuilder)
        {
            SeedCountries(modelBuilder);
            SeedCategories(modelBuilder);
            SeedCities(modelBuilder);
        }

        private static void SeedCountries(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "Egypt", Code = "EG", IsActive = true },
                new Country { Id = 2, Name = "Turkey", Code = "TR", IsActive = true },
                new Country { Id = 3, Name = "Maldives", Code = "MV", IsActive = true },
                new Country { Id = 4, Name = "Saudi Arabia", Code = "SA", IsActive = true }
            );
        }

        private static void SeedCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Hotels", Description = "Accommodation and hotels" },
                new Category { Id = 2, Name = "Restaurants", Description = "Dining and restaurants" },
                new Category { Id = 3, Name = "Tourist Places", Description = "Tourist attractions and landmarks" },
                new Category { Id = 4, Name = "Packages", Description = "Tourism packages and deals" },
                new Category { Id = 5, Name = "Religious Trips", Description = "Umrah and Hajj trips" },
                new Category { Id = 6, Name = "Family Trips", Description = "Family-friendly destinations" },
                new Category { Id = 7, Name = "Beach Trips", Description = "Beach and coastal destinations" },
                new Category { Id = 8, Name = "Adventure", Description = "Adventure and outdoor activities" },
                new Category { Id = 9, Name = "Historical", Description = "Historical sites and monuments" },
                new Category { Id = 10, Name = "Cultural", Description = "Cultural experiences and museums" },
                new Category { Id = 11, Name = "Nature", Description = "Natural landscapes and parks" },
                new Category { Id = 12, Name = "Shopping", Description = "Shopping destinations and markets" }
            );
        }

        private static void SeedCities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(

                // ========================================
                // 🇪🇬 EGYPT - All Touristic Cities
                // ========================================

                // Cairo & Giza
                new City { Id = 1001, Name = "Cairo", CountryId = 1, IsActive = true, Description = "Capital city with pyramids and museums" },
                new City { Id = 1002, Name = "Giza", CountryId = 1, IsActive = true, Description = "Home of the Great Pyramids and Sphinx" },

                // Mediterranean Coast
                new City { Id = 1003, Name = "Alexandria", CountryId = 1, IsActive = true, Description = "Mediterranean pearl with ancient library" },
                new City { Id = 1004, Name = "Marsa Matruh", CountryId = 1, IsActive = true, Description = "Beautiful Mediterranean beaches" },
                new City { Id = 1005, Name = "El Alamein", CountryId = 1, IsActive = true, Description = "WWII historical site and new resort city" },
                new City { Id = 1006, Name = "North Coast", CountryId = 1, IsActive = true, Description = "Luxury beach resorts along Mediterranean" },

                // Upper Egypt (Historical)
                new City { Id = 1007, Name = "Luxor", CountryId = 1, IsActive = true, Description = "Ancient Thebes with Valley of Kings" },
                new City { Id = 1008, Name = "Aswan", CountryId = 1, IsActive = true, Description = "Nubian culture and beautiful Nile scenery" },
                new City { Id = 1009, Name = "Abu Simbel", CountryId = 1, IsActive = true, Description = "Ramses II temples" },
                new City { Id = 1010, Name = "Edfu", CountryId = 1, IsActive = true, Description = "Temple of Horus" },
                new City { Id = 1011, Name = "Kom Ombo", CountryId = 1, IsActive = true, Description = "Double temple on the Nile" },
                new City { Id = 1012, Name = "Esna", CountryId = 1, IsActive = true, Description = "Ancient temple and Nile cruise stop" },

                // Red Sea Coast
                new City { Id = 1013, Name = "Hurghada", CountryId = 1, IsActive = true, Description = "Popular Red Sea resort with diving" },
                new City { Id = 1014, Name = "Sharm El Sheikh", CountryId = 1, IsActive = true, Description = "Premium resort city in South Sinai" },
                new City { Id = 1015, Name = "Dahab", CountryId = 1, IsActive = true, Description = "Laid-back diving and windsurfing destination" },
                new City { Id = 1016, Name = "Marsa Alam", CountryId = 1, IsActive = true, Description = "Pristine diving spots and marine life" },
                new City { Id = 1017, Name = "El Gouna", CountryId = 1, IsActive = true, Description = "Luxury resort town near Hurghada" },
                new City { Id = 1018, Name = "Soma Bay", CountryId = 1, IsActive = true, Description = "Exclusive resort destination" },
                new City { Id = 1019, Name = "Makadi Bay", CountryId = 1, IsActive = true, Description = "Resort area with water sports" },
                new City { Id = 1020, Name = "Sahl Hasheesh", CountryId = 1, IsActive = true, Description = "Upscale resort community" },

                // Sinai Peninsula
                new City { Id = 1021, Name = "Taba", CountryId = 1, IsActive = true, Description = "Border resort with coral reefs" },
                new City { Id = 1022, Name = "Nuweiba", CountryId = 1, IsActive = true, Description = "Quiet beaches and diving spots" },
                new City { Id = 1023, Name = "Saint Catherine", CountryId = 1, IsActive = true, Description = "Mount Sinai and ancient monastery" },
                new City { Id = 1024, Name = "Ras Sidr", CountryId = 1, IsActive = true, Description = "Developing resort area" },

                // Near Cairo
                new City { Id = 1025, Name = "Ain Sokhna", CountryId = 1, IsActive = true, Description = "Close weekend beach destination" },
                new City { Id = 1026, Name = "Fayoum", CountryId = 1, IsActive = true, Description = "Oasis with Wadi El Rayan waterfalls" },
                new City { Id = 1027, Name = "Wadi El Natrun", CountryId = 1, IsActive = true, Description = "Coptic monasteries in the desert" },

                // Desert Oases
                new City { Id = 1028, Name = "Siwa Oasis", CountryId = 1, IsActive = true, Description = "Remote desert oasis with unique culture" },
                new City { Id = 1029, Name = "Bahariya Oasis", CountryId = 1, IsActive = true, Description = "White Desert and Black Desert gateway" },
                new City { Id = 1030, Name = "Dakhla Oasis", CountryId = 1, IsActive = true, Description = "Historical oasis with hot springs" },
                new City { Id = 1031, Name = "Kharga Oasis", CountryId = 1, IsActive = true, Description = "Ancient temples and fortress" },
                new City { Id = 1032, Name = "Farafra Oasis", CountryId = 1, IsActive = true, Description = "Gateway to White Desert" },

                // Canal Cities
                new City { Id = 1033, Name = "Port Said", CountryId = 1, IsActive = true, Description = "Suez Canal entrance city" },
                new City { Id = 1034, Name = "Ismailia", CountryId = 1, IsActive = true, Description = "Beautiful city on Suez Canal" },
                new City { Id = 1035, Name = "Suez", CountryId = 1, IsActive = true, Description = "Strategic port city" },

                // Delta Cities
                new City { Id = 1036, Name = "Damietta", CountryId = 1, IsActive = true, Description = "Nile Delta coastal city" },
                new City { Id = 1037, Name = "Rosetta (Rashid)", CountryId = 1, IsActive = true, Description = "Historical city where Rosetta Stone was found" },
                new City { Id = 1038, Name = "Tanta", CountryId = 1, IsActive = true, Description = "Religious festivals destination" },

                // Other Notable Cities
                new City { Id = 1039, Name = "Minya", CountryId = 1, IsActive = true, Description = "Tuna el-Gebel and Beni Hassan tombs" },
                new City { Id = 1040, Name = "Sohag", CountryId = 1, IsActive = true, Description = "Abydos temple complex" },
                new City { Id = 1041, Name = "Qena", CountryId = 1, IsActive = true, Description = "Gateway to Dendera Temple" },

                // ========================================
                // 🇹🇷 TURKEY - All Major Touristic Cities
                // ========================================

                // Marmara Region
                new City { Id = 2001, Name = "Istanbul", CountryId = 2, IsActive = true, Description = "Historic city spanning two continents" },
                new City { Id = 2002, Name = "Bursa", CountryId = 2, IsActive = true, Description = "Ottoman heritage and Uludağ ski resort" },
                new City { Id = 2003, Name = "Edirne", CountryId = 2, IsActive = true, Description = "Former Ottoman capital with beautiful mosques" },
                new City { Id = 2004, Name = "Çanakkale", CountryId = 2, IsActive = true, Description = "Troy ancient city and WWI memorials" },

                // Aegean Coast
                new City { Id = 2005, Name = "Izmir", CountryId = 2, IsActive = true, Description = "Modern coastal city with ancient Smyrna" },
                new City { Id = 2006, Name = "Bodrum", CountryId = 2, IsActive = true, Description = "Luxury resort town with ancient ruins" },
                new City { Id = 2007, Name = "Kuşadası", CountryId = 2, IsActive = true, Description = "Cruise port near Ephesus" },
                new City { Id = 2008, Name = "Çeşme", CountryId = 2, IsActive = true, Description = "Beach resort with thermal springs" },
                new City { Id = 2009, Name = "Marmaris", CountryId = 2, IsActive = true, Description = "Popular beach resort and marina" },
                new City { Id = 2010, Name = "Fethiye", CountryId = 2, IsActive = true, Description = "Ölüdeniz Blue Lagoon and paragliding" },
                new City { Id = 2011, Name = "Datça", CountryId = 2, IsActive = true, Description = "Peaceful peninsula destination" },
                new City { Id = 2012, Name = "Aydın", CountryId = 2, IsActive = true, Description = "Ancient cities of Ephesus and Aphrodisias" },

                // Mediterranean Coast (Turkish Riviera)
                new City { Id = 2013, Name = "Antalya", CountryId = 2, IsActive = true, Description = "Tourism capital of Turkish Riviera" },
                new City { Id = 2014, Name = "Alanya", CountryId = 2, IsActive = true, Description = "Beach resort with historical castle" },
                new City { Id = 2015, Name = "Side", CountryId = 2, IsActive = true, Description = "Ancient ruins on beautiful beaches" },
                new City { Id = 2016, Name = "Kaş", CountryId = 2, IsActive = true, Description = "Diving paradise and boutique town" },
                new City { Id = 2017, Name = "Kalkan", CountryId = 2, IsActive = true, Description = "Upscale hillside resort town" },
                new City { Id = 2018, Name = "Kemer", CountryId = 2, IsActive = true, Description = "Mountain-backed beach resort" },
                new City { Id = 2019, Name = "Belek", CountryId = 2, IsActive = true, Description = "Golf and luxury resort destination" },

                // Central Anatolia
                new City { Id = 2020, Name = "Ankara", CountryId = 2, IsActive = true, Description = "Capital city with Anıtkabir mausoleum" },
                new City { Id = 2021, Name = "Cappadocia (Nevşehir)", CountryId = 2, IsActive = true, Description = "Fairy chimneys and hot air balloons" },
                new City { Id = 2022, Name = "Göreme", CountryId = 2, IsActive = true, Description = "Cave hotels and rock formations" },
                new City { Id = 2023, Name = "Ürgüp", CountryId = 2, IsActive = true, Description = "Wine region in Cappadocia" },
                new City { Id = 2024, Name = "Konya", CountryId = 2, IsActive = true, Description = "Mevlana Museum and Whirling Dervishes" },
                new City { Id = 2025, Name = "Kayseri", CountryId = 2, IsActive = true, Description = "Gateway to Cappadocia with Mount Erciyes" },

                // Black Sea Region
                new City { Id = 2026, Name = "Trabzon", CountryId = 2, IsActive = true, Description = "Sumela Monastery and Uzungöl lake" },
                new City { Id = 2027, Name = "Rize", CountryId = 2, IsActive = true, Description = "Tea plantations and lush green mountains" },
                new City { Id = 2028, Name = "Ayder", CountryId = 2, IsActive = true, Description = "Highland plateau with hot springs" },
                new City { Id = 2029, Name = "Samsun", CountryId = 2, IsActive = true, Description = "Black Sea coastal city" },
                new City { Id = 2030, Name = "Sinop", CountryId = 2, IsActive = true, Description = "Historic Black Sea port" },

                // Eastern Turkey
                new City { Id = 2031, Name = "Van", CountryId = 2, IsActive = true, Description = "Lake Van and ancient Armenian church" },
                new City { Id = 2032, Name = "Erzurum", CountryId = 2, IsActive = true, Description = "Ski resort and historical city" },
                new City { Id = 2033, Name = "Diyarbakır", CountryId = 2, IsActive = true, Description = "Ancient city walls and Tigris River" },
                new City { Id = 2034, Name = "Mardin", CountryId = 2, IsActive = true, Description = "Stone architecture and ancient monasteries" },
                new City { Id = 2035, Name = "Şanlıurfa", CountryId = 2, IsActive = true, Description = "Birthplace of Abraham, Göbekli Tepe" },

                // Western Interior
                new City { Id = 2036, Name = "Pamukkale (Denizli)", CountryId = 2, IsActive = true, Description = "White travertine terraces and Hierapolis" },
                new City { Id = 2037, Name = "Afyonkarahisar", CountryId = 2, IsActive = true, Description = "Thermal springs and castle" },
                new City { Id = 2038, Name = "Eskişehir", CountryId = 2, IsActive = true, Description = "Modern university city with river parks" },

                // ========================================
                // 🇲🇻 MALDIVES - All Tourist Destinations
                // ========================================

                new City { Id = 3001, Name = "Malé", CountryId = 3, IsActive = true, Description = "Capital city and main gateway" },
                new City { Id = 3002, Name = "Hulhumalé", CountryId = 3, IsActive = true, Description = "Reclaimed island with hotels and beaches" },

                // North Malé Atoll
                new City { Id = 3003, Name = "North Malé Atoll", CountryId = 3, IsActive = true, Description = "Closest luxury resorts to airport" },
                new City { Id = 3004, Name = "Thulusdhoo", CountryId = 3, IsActive = true, Description = "Surfing and local island experience" },
                new City { Id = 3005, Name = "Maafushi", CountryId = 3, IsActive = true, Description = "Budget-friendly guesthouse island" },
                new City { Id = 3006, Name = "Gulhi", CountryId = 3, IsActive = true, Description = "Local island with beautiful beaches" },

                // South Malé Atoll
                new City { Id = 3007, Name = "South Malé Atoll", CountryId = 3, IsActive = true, Description = "Diving and surfing paradise" },
                new City { Id = 3008, Name = "Guraidhoo", CountryId = 3, IsActive = true, Description = "Surfing and diving destination" },

                // Ari Atoll
                new City { Id = 3009, Name = "Ari Atoll (North)", CountryId = 3, IsActive = true, Description = "Whale shark diving hotspot" },
                new City { Id = 3010, Name = "Ari Atoll (South)", CountryId = 3, IsActive = true, Description = "Luxury resorts and manta rays" },
                new City { Id = 3011, Name = "Rasdhoo", CountryId = 3, IsActive = true, Description = "Hammerhead shark diving" },
                new City { Id = 3012, Name = "Thoddoo", CountryId = 3, IsActive = true, Description = "Agricultural island with beaches" },

                // Baa Atoll (UNESCO Biosphere Reserve)
                new City { Id = 3013, Name = "Baa Atoll", CountryId = 3, IsActive = true, Description = "UNESCO site with manta ray season" },
                new City { Id = 3014, Name = "Dharavandhoo", CountryId = 3, IsActive = true, Description = "Gateway to Hanifaru Bay" },
                new City { Id = 3015, Name = "Eydhafushi", CountryId = 3, IsActive = true, Description = "Atoll capital with local culture" },

                // Lhaviyani Atoll
                new City { Id = 3016, Name = "Lhaviyani Atoll", CountryId = 3, IsActive = true, Description = "Pristine dive sites" },
                new City { Id = 3017, Name = "Naifaru", CountryId = 3, IsActive = true, Description = "Atoll capital" },

                // Raa Atoll
                new City { Id = 3018, Name = "Raa Atoll", CountryId = 3, IsActive = true, Description = "Remote luxury resorts" },

                // Noonu Atoll
                new City { Id = 3019, Name = "Noonu Atoll", CountryId = 3, IsActive = true, Description = "Exclusive resorts" },

                // Dhaalu Atoll
                new City { Id = 3020, Name = "Dhaalu Atoll", CountryId = 3, IsActive = true, Description = "Beautiful lagoons and diving" },

                // Vaavu Atoll
                new City { Id = 3021, Name = "Vaavu Atoll", CountryId = 3, IsActive = true, Description = "Shark diving destination" },

                // Laamu Atoll
                new City { Id = 3022, Name = "Laamu Atoll", CountryId = 3, IsActive = true, Description = "Remote and unspoiled" },

                // Addu Atoll (Southernmost)
                new City { Id = 3023, Name = "Addu City (Gan)", CountryId = 3, IsActive = true, Description = "Second largest city, unique culture" },
                new City { Id = 3024, Name = "Hithadhoo", CountryId = 3, IsActive = true, Description = "Most populous island in Addu" },

                // Fuvahmulah
                new City { Id = 3025, Name = "Fuvahmulah", CountryId = 3, IsActive = true, Description = "Tiger shark diving" },

                // ========================================
                // 🇸🇦 SAUDI ARABIA - Religious & Tourist Cities
                // ========================================

                // Holy Cities (Umrah & Hajj)
                new City { Id = 4001, Name = "Makkah (Mecca)", CountryId = 4, IsActive = true, Description = "Holiest city in Islam, Kaaba and Hajj" },
                new City { Id = 4002, Name = "Madinah (Medina)", CountryId = 4, IsActive = true, Description = "Prophet's Mosque and Islamic heritage" },

                // Western Region (Hijaz)
                new City { Id = 4003, Name = "Jeddah", CountryId = 4, IsActive = true, Description = "Gateway to Makkah, Red Sea port city" },
                new City { Id = 4004, Name = "Taif", CountryId = 4, IsActive = true, Description = "Summer resort in mountains near Makkah" },
                new City { Id = 4005, Name = "Yanbu", CountryId = 4, IsActive = true, Description = "Red Sea diving and beaches" },
                new City { Id = 4006, Name = "Rabigh", CountryId = 4, IsActive = true, Description = "Coastal city between Jeddah and Madinah" },

                // Northwestern Region
                new City { Id = 4007, Name = "AlUla", CountryId = 4, IsActive = true, Description = "Ancient Nabatean city of Hegra (Madain Saleh)" },
                new City { Id = 4008, Name = "Tabuk", CountryId = 4, IsActive = true, Description = "Gateway to northwestern Saudi Arabia" },
                new City { Id = 4009, Name = "NEOM", CountryId = 4, IsActive = true, Description = "Futuristic mega-city project" },

                // Central Region (Najd)
                new City { Id = 4010, Name = "Riyadh", CountryId = 4, IsActive = true, Description = "Capital city with modern attractions" },
                new City { Id = 4011, Name = "Diriyah", CountryId = 4, IsActive = true, Description = "UNESCO heritage site, birthplace of Saudi state" },
                new City { Id = 4012, Name = "Al Kharj", CountryId = 4, IsActive = true, Description = "Agricultural region near Riyadh" },

                // Eastern Region
                new City { Id = 4013, Name = "Dammam", CountryId = 4, IsActive = true, Description = "Major port city on Arabian Gulf" },
                new City { Id = 4014, Name = "Al Khobar", CountryId = 4, IsActive = true, Description = "Modern city with corniche and shopping" },
                new City { Id = 4015, Name = "Dhahran", CountryId = 4, IsActive = true, Description = "Oil industry hub" },
                new City { Id = 4016, Name = "Al Ahsa", CountryId = 4, IsActive = true, Description = "UNESCO heritage oasis with date palms" },
                new City { Id = 4017, Name = "Jubail", CountryId = 4, IsActive = true, Description = "Industrial city with beaches" },
                new City { Id = 4018, Name = "Qatif", CountryId = 4, IsActive = true, Description = "Historic coastal city" },
                new City { Id = 4019, Name = "Half Moon Bay", CountryId = 4, IsActive = true, Description = "Popular beach destination near Khobar" },

                // Southern Region (Asir)
                new City { Id = 4020, Name = "Abha", CountryId = 4, IsActive = true, Description = "Mountain resort city with cable cars" },
                new City { Id = 4021, Name = "Khamis Mushait", CountryId = 4, IsActive = true, Description = "Twin city to Abha" },
                new City { Id = 4022, Name = "Jizan", CountryId = 4, IsActive = true, Description = "Coastal city near Yemen border" },
                new City { Id = 4023, Name = "Farasan Islands", CountryId = 4, IsActive = true, Description = "Pristine islands in Red Sea" },
                new City { Id = 4024, Name = "Najran", CountryId = 4, IsActive = true, Description = "Historical city near Yemen" },

                // Northern Region
                new City { Id = 4025, Name = "Hail", CountryId = 4, IsActive = true, Description = "Ancient rock art and historical sites" },
                new City { Id = 4026, Name = "Sakaka", CountryId = 4, IsActive = true, Description = "Northern region capital" },

                // Red Sea Project Areas
                new City { Id = 4027, Name = "Umluj", CountryId = 4, IsActive = true, Description = "Maldives of Saudi Arabia - pristine islands" },
                new City { Id = 4028, Name = "Red Sea Project", CountryId = 4, IsActive = true, Description = "Luxury tourism destination under development" }
            );
        }
    }
}