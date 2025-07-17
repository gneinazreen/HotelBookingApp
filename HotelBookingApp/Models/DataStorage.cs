using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HotelBookingApp.Models
{
    public static class DataStorage
    {
        public static List<Booking> Bookings { get; private set; } = new List<Booking>();
        public static List<Room> Rooms { get; private set; } = new List<Room>();
        public static List<SpecialRequest> Requests { get; private set; } = new List<SpecialRequest>();

        public static void Initialize()
        {
            string xml = Properties.Resources.InitialBookingsXML;

            XDocument doc = XDocument.Parse(xml);

            // Load Bookings
            var bookings = doc.Descendants("Booking").Select(b => new Booking
            {
                FirstName = b.Element("FirstName")?.Value,
                LastName = b.Element("LastName")?.Value,
                //RoomType = b.Element("RoomType")?.Value,
                RoomId = int.Parse(b.Element("RoomId")?.Value ?? "0"),
                CheckInDate = DateTime.Parse(b.Element("CheckInDate")?.Value),
                CheckOutDate = DateTime.Parse(b.Element("CheckOutDate")?.Value),
                //SpecialRequests = "Loaded from XML",
                //IsRecurring = false,
                //RecurrencePattern = "None"
                RequestId = int.Parse(b.Element("RequestId")?.Value ?? "0"),
                IsRecurring = bool.TryParse(b.Element("IsRecurring")?.Value, out var recur) && recur,
                RecurrencePattern = b.Element("RecurrencePattern")?.Value ?? "None"
            }).ToList();

            Bookings.AddRange(bookings);

            // Load Rooms
            var rooms = doc.Descendants("Room").Select(r => new Room
            {
                RoomId = int.Parse(r.Element("RoomId")?.Value),
                RoomType = r.Element("RoomType")?.Value,
                BasePrice = float.Parse(r.Element("BasePrice")?.Value)

            }).ToList();

            Rooms.AddRange(rooms);

            // Load Requests
            var requests = doc.Descendants("Request").Select(r => new SpecialRequest
            {
                RequestId = int.Parse(r.Element("RequestId")?.Value),
                Description = r.Element("Description")?.Value,
                Category = r.Element("Category")?.Value
            }).ToList();

            Requests.AddRange(requests);
        }
        public static void AddBooking(Booking b)
        {
            b.BookingId = Bookings.Count > 0 ? Bookings.Max(x => x.BookingId) + 1 : 1;
            Bookings.Add(b);
        }
        public static void DeleteBooking(int id)
        {
            var booking = Bookings.FirstOrDefault(b => b.BookingId == id);
            if (booking != null)
            {
                Bookings.Remove(booking);
            }
        }
        public static void AddRoom(Room r)
        {
            r.RoomId = Rooms.Count > 0 ? Rooms.Max(x => x.RoomId) + 1 : 1;
            Rooms.Add(r);
        }

        public static void DeleteRoom(int id)
        {
            Rooms.RemoveAll(r => r.RoomId == id);
        }

        public static void AddRequest(SpecialRequest r)
        {
            r.RequestId = Requests.Count > 0 ? Requests.Max(x => x.RequestId) + 1 : 1;
            Requests.Add(r);
        }
        public static void DeleteRequest(int id)
        {
            Requests.RemoveAll(r => r.RequestId == id);
        }

        public static Dictionary<string, List<Booking>> WeeklyReport()
        {
            return Bookings
                .Where(b=> b.CheckInDate >= DateTime.Today && b.CheckInDate < DateTime.Today.AddDays(7))
                .GroupBy(b => b.CheckInDate.DayOfWeek.ToString())
                .ToDictionary(g=>g.Key, g=> g.ToList());
        }
        public static string ChatbotResponse(string message)
        {
            var rand = new Random();
            if (message.ToLower().Contains("availability"))
            {
                var nextWeek = DateTime.Today.AddDays(7);
                int count = Bookings.Count(b => b.CheckInDate <= nextWeek && b.CheckOutDate >= nextWeek);
                return $"Rooms available next week: {Rooms.Count - count}";
            }else if (message.ToLower().Contains("price"))
            {
                var avg = Rooms.Average(r => r.BasePrice);
                return $"Predicted average price: ${avg + rand.Next(10, 30):0.00}";
            }
            return "Sorry, I didn't understand. Try asking about availability or pricing.";
        }
    }

    
}
