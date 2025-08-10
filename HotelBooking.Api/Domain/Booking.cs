namespace HotelBooking.Api.Domain
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int RoomId { get; set; }
        public int RequestId { get; set; }
        public bool IsRecurring { get; set; }
        public string RecurrencePattern { get; set; } = "None";
    }
}
