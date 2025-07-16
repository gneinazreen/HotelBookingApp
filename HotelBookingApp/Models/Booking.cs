using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public string RoomType { get; set; }

        public string SpecialRequests { get; set; }

        public bool IsRecurring { get; set; }

        public string RecurrencePattern { get; set; }

    }
}
