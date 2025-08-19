namespace HotelBooking.Api.Contracts
{
    public record BookingDto(
        int BookingId, 
        string FirstName, 
        string LastName,
        int RoomId, 
        int RequestId, 
        DateTime CheckIn, 
        DateTime CheckOut,
        bool IsRecurring, 
        string RecurrencePattern
     );
}
