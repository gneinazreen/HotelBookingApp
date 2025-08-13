using HotelBookingApp.Services;
using System.Collections.Generic;
using System.Net.Http;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ApiClient
{
    private readonly HttpClient _http;
    private readonly ApiOptions _opt;
    public ApiClient(HttpClient http, ApiOptions opt) { _http = http; _opt = opt; _http.BaseAddress = new Uri(opt.BaseAddress); }

    private string P(string controller) => _opt.UseXmlRoutes ? $"api/xml/{controller}" : $"api/{controller}";

    // generic helpers
    protected async Task<T?> GetAsync<T>(string url) => await _http.GetFromJsonAsync<T>(url);
    protected async Task<T?> PostAsync<T>(string url, object body)
    {
        var res = await _http.PostAsJsonAsync(url, body);
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<T>();
    }
    protected async Task PutAsync(string url, object body)
    {
        var res = await _http.PutAsJsonAsync(url, body);
        res.EnsureSuccessStatusCode();
    }
    protected async Task DeleteAsync(string url)
    {
        var res = await _http.DeleteAsync(url);
        res.EnsureSuccessStatusCode();
    }

    // Rooms
    public Task<List<HotelBooking.Api.Contracts.RoomDto>?> GetRooms() =>
        GetAsync<List<HotelBooking.Api.Contracts.RoomDto>>(P("rooms"));
    public Task<HotelBooking.Api.Contracts.RoomDto?> GetRoom(int id) =>
        GetAsync<HotelBooking.Api.Contracts.RoomDto>($"{P("rooms")}/{id}");
    public Task<HotelBooking.Api.Contracts.RoomDto?> CreateRoom(HotelBooking.Api.Contracts.RoomDto dto) =>
        PostAsync<HotelBooking.Api.Contracts.RoomDto>(P("rooms"), dto);
    public Task UpdateRoom(HotelBooking.Api.Contracts.RoomDto dto) =>
        PutAsync($"{P("rooms")}/{dto.RoomId}", dto);
    public Task DeleteRoom(int id) => DeleteAsync($"{P("rooms")}/{id}");

    // Requests
    public Task<List<HotelBooking.Api.Contracts.SpecialRequestDto>?> GetRequests() =>
        GetAsync<List<HotelBooking.Api.Contracts.SpecialRequestDto>>(P("requests"));
    public Task<HotelBooking.Api.Contracts.SpecialRequestDto?> CreateRequest(HotelBooking.Api.Contracts.SpecialRequestDto dto) =>
        PostAsync<HotelBooking.Api.Contracts.SpecialRequestDto>(P("requests"), dto);

    // Bookings
    public Task<List<HotelBooking.Api.Contracts.BookingDto>?> GetBookings() =>
        GetAsync<List<HotelBooking.Api.Contracts.BookingDto>>(P("bookings"));
    public Task<HotelBooking.Api.Contracts.BookingDto?> CreateBooking(HotelBooking.Api.Contracts.BookingDto dto) =>
        PostAsync<HotelBooking.Api.Contracts.BookingDto>(P("bookings"), dto);

    // Reports
    public Task<List<HotelBooking.Api.Contracts.WeeklyReportRow>?> GetWeeklyReport(DateTime weekStart) =>
        GetAsync<List<HotelBooking.Api.Contracts.WeeklyReportRow>>($"{P("reports")}/weekly?weekStart={weekStart:O}");
}
