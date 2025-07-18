using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.Validators
{
    public static class BookingValidator
    {
        public static List<string> ValidateBooking(string firstName, string lastName, string roomType, string request, DateTime checkIn, DateTime checkOut)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(firstName))
                errors.Add("First Name is required.");

            else if (string.IsNullOrWhiteSpace(lastName))
                errors.Add("Last Name is required.");

            else if (string.IsNullOrWhiteSpace(roomType))
                errors.Add("Room Type must be selected.");

            else if (checkIn >= checkOut)
                errors.Add("Check-Out Date must be after Check-In Date.");

            return errors;
        }
    }
}
