using HotelBooking.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Infrastructure;

public class HotelContext : DbContext
{
    public HotelContext(DbContextOptions<HotelContext> options) : base(options) { }

    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<SpecialRequest> Requests => Set<SpecialRequest>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        // Room
        b.Entity<Room>().HasKey(x => x.RoomId);
        b.Entity<Room>().Property(x => x.RoomType).HasMaxLength(100).IsRequired();
        b.Entity<Room>().Property(x => x.BasePrice).HasColumnType("decimal(18,2)");

        // SpecialRequest
        b.Entity<SpecialRequest>().HasKey(x => x.RequestId);
        b.Entity<SpecialRequest>().Property(x => x.Description).HasMaxLength(200).IsRequired();
        b.Entity<SpecialRequest>().Property(x => x.Category).HasMaxLength(60).IsRequired();

        // Booking
        b.Entity<Booking>().HasKey(x => x.BookingId);
        b.Entity<Booking>().Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        b.Entity<Booking>().Property(x => x.LastName).HasMaxLength(100).IsRequired();

        // helpful index for overlap lookups
        b.Entity<Booking>().HasIndex(x => new { x.RoomId, x.CheckIn, x.CheckOut });

        // (optional) FK integrity without nav props
        b.Entity<Booking>()
            .HasOne<Room>()
            .WithMany()
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        b.Entity<Booking>()
            .HasOne<SpecialRequest>()
            .WithMany()
            .HasForeignKey(x => x.RequestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
