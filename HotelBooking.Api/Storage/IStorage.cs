using HotelBooking.Api.Domain;

namespace HotelBooking.Api.Storage
{
    public interface IStorage
    {
        // Rooms
        IEnumerable<Room> GetRooms();
        Room? GetRoom(int id);
        Room AddRoom(Room r);
        Room? UpdateRoom(Room r);
        bool DeleteRoom(int id);

        // Requests
        IEnumerable<SpecialRequest> GetRequests();
        SpecialRequest? GetRequest(int id);
        SpecialRequest AddRequest(SpecialRequest r);
        SpecialRequest? UpdateRequest(SpecialRequest r);
        bool DeleteRequest(int id);

        // Bookings
        IEnumerable<Booking> GetBookings();
        Booking? GetBooking(int id);
        Booking AddBooking(Booking b);
        Booking? UpdateBooking(Booking b);
        bool DeleteBooking(int id);
    }

}
