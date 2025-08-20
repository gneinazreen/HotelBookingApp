using Microsoft.EntityFrameworkCore;
using HotelBooking.Api.Domain;

namespace Rooms.Api.Infrastructure
{
    public class RoomsContext : DbContext
    {
        public RoomsContext(DbContextOptions<RoomsContext> opt) : base(opt) { }
        public DbSet<Room> Rooms => Set<Room>();
        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Room>().HasKey(x => x.RoomId);
            b.Entity<Room>().Property(x => x.RoomType).HasMaxLength(100).IsRequired();
            b.Entity<Room>().Property(x => x.BasePrice).HasColumnType("decimal(18,2)");
        }
    }
}
