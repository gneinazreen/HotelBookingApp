namespace HotelBooking.Api.Contracts
{
    public record WeeklyReportRow(DateTime Day, string Guest, string RoomType, string Request);
}
