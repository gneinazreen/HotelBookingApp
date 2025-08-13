using HotelBooking.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Infrastructure;

public static class DataSeeder
{
    public static async Task SeedAsync(HotelContext db)
    {
        // If already seeded, do nothing
        if (await db.Rooms.AnyAsync() || await db.Bookings.AnyAsync()) return;

        db.Rooms.AddRange(
            new Room { RoomType = "Standard", BasePrice = 100m },
            new Room { RoomType = "Deluxe", BasePrice = 150m },
            new Room { RoomType = "Suite", BasePrice = 250m }
        );

        db.Requests.AddRange(
            new SpecialRequest { Description = "Airport Pickup", Category = "Transport" },
            new SpecialRequest { Description = "Vegan Breakfast", Category = "Meal" }
        );

        var today = DateTime.Today;
        db.Bookings.AddRange(
            new Booking
            {
                FirstName = "Aisha",
                LastName = "Perera",
                RoomId = 1,
                RequestId = 1,
                CheckIn = today.AddDays(1),
                CheckOut = today.AddDays(3),
                IsRecurring = false,
                RecurrencePattern = "None"
            },
            new Booking
            {
                FirstName = "Ravi",
                LastName = "Silva",
                RoomId = 2,
                RequestId = 2,
                CheckIn = today.AddDays(2),
                CheckOut = today.AddDays(5),
                IsRecurring = false,
                RecurrencePattern = "None"
            }
        );

        await db.SaveChangesAsync();
    }
}
