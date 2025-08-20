namespace HotelBooking.Api.Domain
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomType { get; set; } = "";
        public decimal BasePrice { get; set; }
    }
}
