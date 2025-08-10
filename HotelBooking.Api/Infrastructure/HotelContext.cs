using HotelBooking.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Infrastructure
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> opts) : base(opts) { }
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<SpecialRequest> Requests => Set<SpecialRequest>();
        public DbSet<Booking> Bookings => Set<Booking>();
    }

}
