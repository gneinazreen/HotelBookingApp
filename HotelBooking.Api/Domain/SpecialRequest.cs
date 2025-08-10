namespace HotelBooking.Api.Domain
{
    public class SpecialRequest { 
        public int RequestId { get; set; } 
        public string Description { get; set; } = ""; 
        public string Category { get; set; } = ""; 
    }
}
