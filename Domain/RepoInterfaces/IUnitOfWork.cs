using Fas7ny.Domain.Entities;
using Fas7ny.Domain.Entities.Fas7ny.Domain.Entities;
using Fas7ny.Domain.Entities.Transpotrations;

namespace Fas7ny.Domain.RepoInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Repository Properties - تضيف Repository لكل Entity
        IGenericRepository<Activity> Activities { get; }
        IGenericRepository<Booking> Bookings { get; }
        IGenericRepository<ChatMessage> ChatMessages { get; }
        IGenericRepository<City> Cities { get; }
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Destination> Destinations { get; }
        IGenericRepository<Hotel> Hotels { get; }
        IGenericRepository<HotelRoom> HotelRooms { get; }
        IGenericRepository<Package> Packages { get; }
        IGenericRepository<Recommendation> Recommendations { get; }
        IGenericRepository<Restaurant> Restaurants { get; }
        IGenericRepository<SearchLog> SearchLogs { get; }
        IGenericRepository<TouristPlace> TouristPlaces { get; }
        IGenericRepository<Bus> Buses { get; }
        IGenericRepository<Flight> Flights { get; }
        IGenericRepository<CartItems> CartItem { get; }
        IGenericRepository<Carts> Carts { get; }
        IGenericRepository<Review> Reviews { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<BookingCustomTrip> BookingCustomTrips { get; }
        IGenericRepository<UserPreference> UserPreferences { get; }
        IGenericRepository<BookingCustomTripDetail> BookingCustomTripDetail { get; }


        // Generic method to get any repository
        IGenericRepository<T> Repository<T>() where T : class;

        // Save Changes
        Task<int> SaveChangesAsync();
        int SaveChanges();

        // Transaction Management
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }

}
