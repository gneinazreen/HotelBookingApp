using System;

namespace HotelBooking.Contracts
{
    public class RoomDto
    {
        public int RoomId { get; set; }
        public string RoomType { get; set; } = "";
        public decimal BasePrice { get; set; }
    }

    public class SpecialRequestDto
    {
        public int RequestId { get; set; }
        public string Description { get; set; } = "";
        public string Category { get; set; } = "";
    }

    public class BookingDto
    {
        public int BookingId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public int RoomId { get; set; }
        public int RequestId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool IsRecurring { get; set; }
        public string RecurrencePattern { get; set; } = "None";
    }

    public class WeeklyReportRow
    {
        public DateTime Day { get; set; }
        public string Guest { get; set; } = "";
        public string RoomType { get; set; } = "";
        public string Request { get; set; } = "";
    }
}
