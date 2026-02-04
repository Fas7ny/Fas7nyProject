using Fas7ny.Application.DTOs.Ai.Request;
using Fas7ny.Domain.Entities;
using Fas7ny.Domain.RepoInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace APIs_Graduation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanMyTripController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        private readonly ILogger<PlanMyTripController> _logger;

        public PlanMyTripController(IUnitOfWork context, ILogger<PlanMyTripController> logger)
        {
            _context = context;
            _logger = logger;
        }

        private IQueryable<TouristPlace> ApplyPlaceFilters(
            IQueryable<TouristPlace> query,
            SimplePrompt filters)
        {
            if (!string.IsNullOrEmpty(filters.Location))
            {
                query = query.Where(p => p.City.Name.Contains(filters.Location));
            }

            if (!string.IsNullOrEmpty(filters.Landmark))
            {
                query = query.Where(p => p.Name.Contains(filters.Landmark) ||
                                        p.Description.Contains(filters.Landmark));
            }

            if (!string.IsNullOrEmpty(filters.Interest))
            {
                query = query.Where(p => p.CategoryId.ToString().Contains(filters.Interest) ||
                                        p.Description.Contains(filters.Interest));
            }

            if (filters.MaxPrice.HasValue)
            {
                query = query.Where(p => p.EntryFee <= (decimal)filters.MaxPrice.Value);
            }

            return query;
        }

        private string GetOpeningHours(string siteName)
        {
            var openingHours = new Dictionary<string, string>
            {
                { "pyramids", "8:00 AM - 5:00 PM" },
                { "sphinx", "8:00 AM - 5:00 PM" },
                { "egyptian museum", "9:00 AM - 5:00 PM" },
                { "karnak temple", "6:00 AM - 5:30 PM" },
                { "luxor temple", "6:00 AM - 9:00 PM" },
                { "valley of the kings", "6:00 AM - 5:00 PM" },
                { "abu simbel", "6:00 AM - 5:00 PM" },
                { "philae temple", "7:00 AM - 4:00 PM" },
                { "aswan dam", "7:00 AM - 5:00 PM" },
                { "citadel", "8:00 AM - 5:00 PM" },
                { "khan el khalili", "9:00 AM - 11:00 PM" },
                { "alexandria library", "11:00 AM - 7:00 PM" },
                { "montaza palace", "8:00 AM - 5:00 PM" },
                { "qaitbay citadel", "9:00 AM - 5:00 PM" }
            };

            var lowerSiteName = siteName.ToLower();
            foreach (var kvp in openingHours)
            {
                if (lowerSiteName.Contains(kvp.Key))
                {
                    return kvp.Value;
                }
            }

            return "9:00 AM - 5:00 PM";
        }

        private async Task<SimplePrompt> ParseEgyptianPromptAsync(string prompt)
        {
            var parsed = new SimplePrompt();
            var lowerPrompt = prompt.ToLower();

            var egyptianCities = new[] { "cairo", "giza", "luxor", "aswan", "alexandria", "hurghada", "sharm el sheikh", "dahab" };
            foreach (var city in egyptianCities)
            {
                if (lowerPrompt.Contains(city))
                {
                    parsed.Location = city;
                    break;
                }
            }

            var landmarks = new[] { "pyramids", "sphinx", "museum", "temple", "valley", "nile", "citadel", "palace" };
            foreach (var landmark in landmarks)
            {
                if (lowerPrompt.Contains(landmark))
                {
                    parsed.Landmark = landmark;
                    break;
                }
            }

            var interests = new Dictionary<string, string>
            {
                { "history", "Historical" },
                { "ancient", "Historical" },
                { "pharaoh", "Historical" },
                { "beach", "Beach" },
                { "diving", "Water Sports" },
                { "snorkeling", "Water Sports" },
                { "museum", "Museum" },
                { "culture", "Cultural" },
                { "shopping", "Shopping" },
                { "market", "Shopping" },
                { "adventure", "Adventure" },
                { "desert", "Desert" }
            };

            foreach (var interest in interests)
            {
                if (lowerPrompt.Contains(interest.Key))
                {
                    parsed.Interest = interest.Value;
                    break;
                }
            }

            parsed.IncludeHotels = lowerPrompt.Contains("hotel") ||
                                   lowerPrompt.Contains("accommodation") ||
                                   lowerPrompt.Contains("stay");

            var priceMatch = Regex.Match(lowerPrompt, @"(\d+)\s*(egp|egyptian pound|le|budget)");
            if (priceMatch.Success)
            {
                if (double.TryParse(priceMatch.Groups[1].Value, out double price))
                {
                    parsed.MaxPrice = price;
                }
            }

            return parsed;
        }

        [HttpPost("generate-egypt-trip")]
        public async Task<IActionResult> GenerateEgyptTrip([FromBody] AITripRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Generating Egypt trip for prompt: {Prompt}", request.Prompt);

            var filters = await ParseEgyptianPromptAsync(request.Prompt);

            var placesQuery = _context.TouristPlaces.GetAllAsync().Result.AsQueryable();
            placesQuery = ApplyPlaceFilters(placesQuery, filters);

            var places = placesQuery
                .OrderByDescending(p => p.EntryFee)
                .Take(10)
                .ToList();

            var hotels = new List<Hotel>();
            if (filters.IncludeHotels && !string.IsNullOrEmpty(filters.Location))
            {
                hotels = (await _context.Hotels.GetAllAsync())
                    .Where(h => h.City.Name.Contains(filters.Location))
                    .OrderByDescending(h => h.PricePerNight)
                    .Take(5)
                    .ToList();
            }

            var itinerary = new List<object>();
            var dayNumber = 1;

            foreach (var place in places)
            {
                itinerary.Add(new
                {
                    day = dayNumber,
                    activity = place.Name,
                    description = place.Description,
                    location = $"{place.City?.Name ?? "Egypt"}",
                    category = place.Category?.Name ?? "Tourist Attraction",
                    entryFee = place.EntryFee,
                    openingHours = GetOpeningHours(place.Name),
                    estimatedDuration = "2-3 hours"
                });

                if (dayNumber % 2 == 0 && hotels.Any())
                {
                    var hotel = hotels[dayNumber / 2 % hotels.Count];
                    itinerary.Add(new
                    {
                        day = dayNumber,
                        activity = $"Overnight at {hotel.Name}",
                        description = hotel.Description,
                        location = $"{hotel.City?.Name ?? "Egypt"}",
                        category = "Accommodation",
                        pricePerNight = hotel.PricePerNight
                    });
                }

                dayNumber++;
            }

            var totalCost = places.Sum(p => p.EntryFee);
            if (hotels.Any())
            {
                totalCost += hotels.Take(3).Sum(h => h.PricePerNight) * 1;
            }

            var response = new
            {
                success = true,
                message = "Egypt trip generated successfully",
                tripSummary = new
                {
                    destination = filters.Location ?? "Egypt",
                    duration = $"{places.Count} days",
                    totalActivities = places.Count,
                    totalHotels = hotels.Count,
                    estimatedCost = totalCost,
                    currency = "EGP"
                },
                itinerary,
                recommendedPlaces = places.Select(p => new
                {
                    id = p.Id,
                    name = p.Name,
                    description = p.Description,
                    city = p.City?.Name ?? "Unknown",
                    category = p.Category?.Name ?? "Attraction",
                    price = p.EntryFee,
                    imageUrl = p.ImageUrl
                }),
                recommendedHotels = hotels.Select(h => new
                {
                    id = h.Id,
                    name = h.Name,
                    description = h.Description,
                    city = h.City?.Name ?? "Unknown",
                    pricePerNight = h.PricePerNight,
                    imageUrl = h.ImageUrl
                }),
                filters = new
                {
                    parsedLocation = filters.Location,
                    parsedLandmark = filters.Landmark,
                    parsedInterest = filters.Interest,
                    includeHotels = filters.IncludeHotels,
                    maxPrice = filters.MaxPrice
                },
                generatedAt = DateTime.UtcNow
            };

            return Ok(response);
        }

        private class SimplePrompt
        {
            public string Landmark { get; set; }
            public string Location { get; set; }
            public string Interest { get; set; }
            public bool IncludeHotels { get; set; }
            public double? MaxPrice { get; set; }
        }
    }


}