namespace HotelBooking.Api.Contracts
{
    public record SpecialRequestDto(
        int RequestId, 
        string Description, 
        string Category
     );
}
