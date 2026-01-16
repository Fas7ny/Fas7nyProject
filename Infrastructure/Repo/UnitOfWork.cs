using Fas7ny.Domain.Entities;
using Fas7ny.Domain.Entities.Transpotrations;
using Fas7ny.Domain.Repo;
using Fas7ny.Domain.RepoInterfaces;
using TourismApp.Data;

namespace Fas7ny.Infrastructure.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TourismDbContext _context;
        private readonly Dictionary<Type, object> _repositories;

        // Repository instances
        private IGenericRepository<Activity> _activities;
        private IGenericRepository<Booking> _bookings;
        private IGenericRepository<ChatMessage> _chatMessages;
        private IGenericRepository<City> _cities;
        private IGenericRepository<Country> _countries;
        private IGenericRepository<Destination> _destinations;
        private IGenericRepository<Hotel> _hotels;
        private IGenericRepository<HotelRoom> _hotelRooms;
        private IGenericRepository<Package> _packages;
        private IGenericRepository<Recommendation> _recommendations;
        private IGenericRepository<Restaurant> _restaurants;
        private IGenericRepository<SearchLog> _searchLogs;
        private IGenericRepository<TouristPlace> _touristPlaces;
        private IGenericRepository<Bus> _buses;
        private IGenericRepository<Flight> _flights;

        public UnitOfWork(TourismDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        // Repository Properties
        public IGenericRepository<Activity> Activities =>
            _activities ??= new GenericRepository<Activity>(_context);

        public IGenericRepository<Booking> Bookings =>
            _bookings ??= new GenericRepository<Booking>(_context);

        public IGenericRepository<ChatMessage> ChatMessages =>
            _chatMessages ??= new GenericRepository<ChatMessage>(_context);

        public IGenericRepository<City> Cities =>
            _cities ??= new GenericRepository<City>(_context);

        public IGenericRepository<Country> Countries =>
            _countries ??= new GenericRepository<Country>(_context);

        public IGenericRepository<Destination> Destinations =>
            _destinations ??= new GenericRepository<Destination>(_context);

        public IGenericRepository<Hotel> Hotels =>
            _hotels ??= new GenericRepository<Hotel>(_context);

        public IGenericRepository<HotelRoom> HotelRooms =>
            _hotelRooms ??= new GenericRepository<HotelRoom>(_context);

        public IGenericRepository<Package> Packages =>
            _packages ??= new GenericRepository<Package>(_context);

        public IGenericRepository<Recommendation> Recommendations =>
            _recommendations ??= new GenericRepository<Recommendation>(_context);

        public IGenericRepository<Restaurant> Restaurants =>
            _restaurants ??= new GenericRepository<Restaurant>(_context);

        public IGenericRepository<SearchLog> SearchLogs =>
            _searchLogs ??= new GenericRepository<SearchLog>(_context);

        public IGenericRepository<TouristPlace> TouristPlaces =>
            _touristPlaces ??= new GenericRepository<TouristPlace>(_context);

        public IGenericRepository<Bus> Buses =>
            _buses ??= new GenericRepository<Bus>(_context);

        public IGenericRepository<Flight> Flights =>
            _flights ??= new GenericRepository<Flight>(_context);

        // Generic Repository Access
        public IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new GenericRepository<T>(_context);
            }
            return (IGenericRepository<T>)_repositories[type];
        }

        // Save Changes
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        // Transaction Management
        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        // Dispose
        public void Dispose()
        {
            _context?.Dispose();
        }
    }

}
