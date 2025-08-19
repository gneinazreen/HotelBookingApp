using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.Contracts;           // <— use the contracts library
using Newtonsoft.Json;                  // <— use Newtonsoft for (de)serialization

namespace HotelBookingApp.Services
{
    public class ApiClient
    {
        private readonly HttpClient _http;
        private readonly ApiOptions _opt;

        public ApiClient(HttpClient http, ApiOptions opt)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _opt = opt ?? throw new ArgumentNullException(nameof(opt));
            _http.BaseAddress = new Uri(_opt.BaseAddress);
        }

        private string P(string controller)
            => _opt.UseXmlRoutes ? $"api/xml/{controller}" : $"api/{controller}";

        // ---------- generic helpers (Newtonsoft + raw HttpClient) ----------
        private async Task<T> GetAsync<T>(string url)
        {
            var res = await _http.GetAsync(url).ConfigureAwait(false);
            res.EnsureSuccessStatusCode();
            var json = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(json);
        }

        private async Task<T> PostAsync<T>(string url, object body)
        {
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            var res = await _http.PostAsync(url, content).ConfigureAwait(false);
            res.EnsureSuccessStatusCode();
            var json = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(json);
        }

        private async Task PutAsync(string url, object body)
        {
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            var res = await _http.PutAsync(url, content).ConfigureAwait(false);
            res.EnsureSuccessStatusCode();
        }

        private async Task DeleteAsync(string url)
        {
            var res = await _http.DeleteAsync(url).ConfigureAwait(false);
            res.EnsureSuccessStatusCode();
        }

        // ---------- Rooms ----------
        public Task<List<RoomDto>> GetRooms() => GetAsync<List<RoomDto>>(P("rooms"));
        public Task<RoomDto> GetRoom(int id) => GetAsync<RoomDto>($"{P("rooms")}/{id}");
        public Task<RoomDto> CreateRoom(RoomDto dto) => PostAsync<RoomDto>(P("rooms"), dto);
        public Task UpdateRoom(RoomDto dto) => PutAsync($"{P("rooms")}/{dto.RoomId}", dto);
        public Task DeleteRoom(int id) => DeleteAsync($"{P("rooms")}/{id}");

        // ---------- Requests ----------
        public Task<List<SpecialRequestDto>> GetRequests() => GetAsync<List<SpecialRequestDto>>(P("requests"));
        public Task<SpecialRequestDto> GetRequest(int id) => GetAsync<SpecialRequestDto>($"{P("requests")}/{id}");
        public Task<SpecialRequestDto> CreateRequest(SpecialRequestDto dto) => PostAsync<SpecialRequestDto>(P("requests"), dto);
        public Task UpdateRequest(SpecialRequestDto dto) => PutAsync($"{P("requests")}/{dto.RequestId}", dto);
        public Task DeleteRequest(int id) => DeleteAsync($"{P("requests")}/{id}");

        // ---------- Bookings ----------
        public Task<List<BookingDto>> GetBookings() => GetAsync<List<BookingDto>>(P("bookings"));
        public Task<BookingDto> GetBooking(int id) => GetAsync<BookingDto>($"{P("bookings")}/{id}");
        public Task<BookingDto> CreateBooking(BookingDto dto) => PostAsync<BookingDto>(P("bookings"), dto);
        public Task UpdateBooking(BookingDto dto) => PutAsync($"{P("bookings")}/{dto.BookingId}", dto);
        public Task DeleteBooking(int id) => DeleteAsync($"{P("bookings")}/{id}");

        // ---------- Reports ----------
        public Task<List<WeeklyReportRow>> GetWeeklyReport(DateTime weekStart)
            => GetAsync<List<WeeklyReportRow>>($"{P("reports")}/weekly?weekStart={weekStart:O}");

        public Task<List<WeeklyReportRow>> GetDailyReport(DateTime start, DateTime end)
            => GetAsync<List<WeeklyReportRow>>($"{P("reports")}/daily?start={start:O}&end={end:O}");
    }
}
