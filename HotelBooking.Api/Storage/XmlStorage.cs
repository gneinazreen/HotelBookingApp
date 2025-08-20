using System.Globalization;
using System.Xml.Linq;
using HotelBooking.Api.Domain;

namespace HotelBooking.Api.Storage
{
    public class XmlStorage : IStorage
    {
        private readonly string _path;
        private readonly object _lock = new();

        public XmlStorage(string filePath)
        {
            _path = filePath;
            EnsureFile();
        }

        private void EnsureFile()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_path)!);
            if (!File.Exists(_path))
            {
                new XDocument(
                    new XElement("Data",
                        new XElement("Bookings"),
                        new XElement("Rooms"),
                        new XElement("Requests")
                    )
                ).Save(_path);
            }
        }

        private XDocument LoadDoc() => XDocument.Load(_path);
        private void SaveDoc(XDocument doc) => doc.Save(_path);

        // ROOMS
        public IEnumerable<Room> GetRooms()
        {
            lock (_lock)
            {
                var root = LoadDoc().Root?.Element("Rooms");
                if (root is null) return Enumerable.Empty<Room>();
                return root.Elements("Room").Select(FromXRoom).ToList();
            }
        }

        public Room? GetRoom(int id) => GetRooms().FirstOrDefault(r => r.RoomId == id);

        public Room AddRoom(Room r)
        {
            lock (_lock)
            {
                var rooms = GetRooms();
                r.RoomId = rooms.Any() ? rooms.Max(x => x.RoomId) + 1 : 1;
                var doc = LoadDoc();
                doc.Root!.Element("Rooms")!.Add(ToX(r));
                SaveDoc(doc);
                return r;
            }
        }

        public Room? UpdateRoom(Room r)
        {
            lock (_lock)
            {
                var doc = LoadDoc();
                var el = doc.Root!.Element("Rooms")!.Elements("Room")
                    .FirstOrDefault(x => (int)x.Element("RoomId")! == r.RoomId);
                if (el is null) return null;

                el.Element("RoomType")!.Value = r.RoomType ?? string.Empty;
                el.Element("BasePrice")!.Value = r.BasePrice.ToString(CultureInfo.InvariantCulture);
                SaveDoc(doc);
                return r;
            }
        }

        public bool DeleteRoom(int id)
        {
            lock (_lock)
            {
                var doc = LoadDoc();
                var el = doc.Root!.Element("Rooms")!.Elements("Room")
                    .FirstOrDefault(x => (int)x.Element("RoomId")! == id);
                if (el is null) return false;
                el.Remove(); SaveDoc(doc); return true;
            }
        }

        // REQUESTS
        public IEnumerable<SpecialRequest> GetRequests()
        {
            lock (_lock)
            {
                var root = LoadDoc().Root?.Element("Requests");
                if (root is null) return Enumerable.Empty<SpecialRequest>();
                return root.Elements("Request").Select(FromXRequest).ToList();
            }
        }

        public SpecialRequest? GetRequest(int id) => GetRequests().FirstOrDefault(r => r.RequestId == id);

        public SpecialRequest AddRequest(SpecialRequest r)
        {
            lock (_lock)
            {
                var requests = GetRequests();
                r.RequestId = requests.Any() ? requests.Max(x => x.RequestId) + 1 : 1;
                var doc = LoadDoc();
                doc.Root!.Element("Requests")!.Add(ToX(r));
                SaveDoc(doc);
                return r;
            }
        }

        public SpecialRequest? UpdateRequest(SpecialRequest r)
        {
            lock (_lock)
            {
                var doc = LoadDoc();
                var el = doc.Root!.Element("Requests")!.Elements("Request")
                    .FirstOrDefault(x => (int)x.Element("RequestId")! == r.RequestId);
                if (el is null) return null;

                el.Element("Description")!.Value = r.Description ?? string.Empty;
                el.Element("Category")!.Value = r.Category ?? string.Empty;
                SaveDoc(doc);
                return r;
            }
        }

        public bool DeleteRequest(int id)
        {
            lock (_lock)
            {
                var doc = LoadDoc();
                var el = doc.Root!.Element("Requests")!.Elements("Request")
                    .FirstOrDefault(x => (int)x.Element("RequestId")! == id);
                if (el is null) return false;
                el.Remove(); SaveDoc(doc); return true;
            }
        }

        // BOOKINGS
        public IEnumerable<Booking> GetBookings()
        {
            lock (_lock)
            {
                var root = LoadDoc().Root?.Element("Bookings");
                if (root is null) return Enumerable.Empty<Booking>();
                return root.Elements("Booking").Select(FromXBooking).ToList();
            }
        }

        public Booking? GetBooking(int id) => GetBookings().FirstOrDefault(b => b.BookingId == id);

        public Booking AddBooking(Booking b)
        {
            lock (_lock)
            {
                var all = GetBookings();
                b.BookingId = all.Any() ? all.Max(x => x.BookingId) + 1 : 1;

                var doc = LoadDoc();
                doc.Root!.Element("Bookings")!.Add(ToX(b));
                SaveDoc(doc);
                return b;
            }
        }

        public Booking? UpdateBooking(Booking b)
        {
            lock (_lock)
            {
                var doc = LoadDoc();
                var el = doc.Root!.Element("Bookings")!.Elements("Booking")
                    .FirstOrDefault(x => (int)x.Element("BookingId")! == b.BookingId);
                if (el is null) return null;

                el.Element("FirstName")!.Value = b.FirstName ?? string.Empty;
                el.Element("LastName")!.Value = b.LastName ?? string.Empty;
                el.Element("CheckInDate")!.Value = b.CheckIn.ToString("o");
                el.Element("CheckOutDate")!.Value = b.CheckOut.ToString("o");
                el.Element("RoomId")!.Value = b.RoomId.ToString();
                el.Element("RequestId")!.Value = b.RequestId.ToString();
                el.Element("IsRecurring")!.Value = b.IsRecurring.ToString();
                el.Element("RecurrencePattern")!.Value = b.RecurrencePattern ?? "None";

                SaveDoc(doc);
                return b;
            }
        }

        public bool DeleteBooking(int id)
        {
            lock (_lock)
            {
                var doc = LoadDoc();
                var el = doc.Root!.Element("Bookings")!.Elements("Booking")
                    .FirstOrDefault(x => (int)x.Element("BookingId")! == id);
                if (el is null) return false;
                el.Remove(); SaveDoc(doc); return true;
            }
        }

        // XML (de)serializers
        private static XElement ToX(Room r) => new("Room",
            new XElement("RoomId", r.RoomId),
            new XElement("RoomType", r.RoomType ?? string.Empty),
            new XElement("BasePrice", r.BasePrice.ToString(CultureInfo.InvariantCulture)));

        private static Room FromXRoom(XElement e) => new()
        {
            RoomId = (int)e.Element("RoomId")!,
            RoomType = (string)e.Element("RoomType") ?? string.Empty,
            BasePrice = decimal.Parse((string)e.Element("BasePrice") ?? "0",
                                      NumberStyles.Float, CultureInfo.InvariantCulture)
        };

        private static XElement ToX(SpecialRequest r) => new("Request",
            new XElement("RequestId", r.RequestId),
            new XElement("Description", r.Description ?? string.Empty),
            new XElement("Category", r.Category ?? string.Empty));

        private static SpecialRequest FromXRequest(XElement e) => new()
        {
            RequestId = (int)e.Element("RequestId")!,
            Description = (string)e.Element("Description") ?? string.Empty,
            Category = (string)e.Element("Category") ?? string.Empty
        };

        private static XElement ToX(Booking b) => new("Booking",
            new XElement("BookingId", b.BookingId),
            new XElement("FirstName", b.FirstName ?? string.Empty),
            new XElement("LastName", b.LastName ?? string.Empty),
            new XElement("CheckInDate", b.CheckIn.ToString("o")),
            new XElement("CheckOutDate", b.CheckOut.ToString("o")),
            new XElement("RoomId", b.RoomId),
            new XElement("RequestId", b.RequestId),
            new XElement("IsRecurring", b.IsRecurring),
            new XElement("RecurrencePattern", b.RecurrencePattern ?? "None"));

        private static Booking FromXBooking(XElement e) => new()
        {
            BookingId = (int)e.Element("BookingId")!,
            FirstName = (string)e.Element("FirstName") ?? string.Empty,
            LastName = (string)e.Element("LastName") ?? string.Empty,
            CheckIn = DateTime.Parse((string)e.Element("CheckInDate")!, null, DateTimeStyles.RoundtripKind),
            CheckOut = DateTime.Parse((string)e.Element("CheckOutDate")!, null, DateTimeStyles.RoundtripKind),
            RoomId = (int)e.Element("RoomId")!,
            RequestId = (int)e.Element("RequestId")!,
            IsRecurring = bool.TryParse((string?)e.Element("IsRecurring"), out var recur) && recur,
            RecurrencePattern = (string?)e.Element("RecurrencePattern") ?? "None"
        };
    }
}
