using Microsoft.EntityFrameworkCore;
using HotelBooking.Api.Domain; 

namespace Rooms.Api.Infrastructure
{
    public static class RoomsSeeder
    {
        public static async Task SeedAsync(RoomsContext db)
        {
            await db.Database.EnsureCreatedAsync();

            if (await db.Rooms.AnyAsync()) return;

            db.Rooms.AddRange(
                new Room { RoomType = "Standard", BasePrice = 100m },
                new Room { RoomType = "Deluxe", BasePrice = 150m },
                new Room { RoomType = "Suite", BasePrice = 250m }
            );

            await db.SaveChangesAsync();
        }
    }
}
