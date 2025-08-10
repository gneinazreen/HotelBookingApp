using HotelBooking.Api.Contracts;
using HotelBooking.Api.Domain;

namespace HotelBooking.Api.Mapping
{
    public static class Map
    {
        public static RoomDto ToDto(Room r) => new(r.RoomId, r.RoomType, r.BasePrice);
        public static Room ToEntity(RoomDto d) => new() { RoomId = d.RoomId, RoomType = d.RoomType, BasePrice = d.BasePrice };
        public static SpecialRequestDto ToDto(SpecialRequest r) => new(r.RequestId, r.Description, r.Category);
        public static SpecialRequest ToEntity(SpecialRequestDto d) => new() { RequestId = d.RequestId, Description = d.Description, Category = d.Category };
        public static BookingDto ToDto(Booking b) => new(b.BookingId, b.FirstName, b.LastName, b.RoomId, b.RequestId, b.CheckIn, b.CheckOut, b.IsRecurring, b.RecurrencePattern);
        public static Booking ToEntity(BookingDto d) => new() { BookingId = d.BookingId, FirstName = d.FirstName, LastName = d.LastName, RoomId = d.RoomId, RequestId = d.RequestId, CheckIn = d.CheckIn, CheckOut = d.CheckOut, IsRecurring = d.IsRecurring, RecurrencePattern = d.RecurrencePattern };
    }

}
