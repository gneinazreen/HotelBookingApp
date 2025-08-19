using System;
using System.Collections.Generic;

namespace HotelBookingApp.Validators
{
    public static class BookingValidator
    {
        /// <summary>
        /// Validate a booking using numeric IDs (API-friendly).
        /// </summary>
        public static List<string> ValidateBooking(
            string firstName,
            string lastName,
            int roomId,
            int requestId,
            DateTime checkIn,
            DateTime checkOut)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(firstName))
                errors.Add("First Name is required.");

            if (string.IsNullOrWhiteSpace(lastName))
                errors.Add("Last Name is required.");

            if (roomId <= 0)
                errors.Add("Room must be selected.");

            if (requestId <= 0)
                errors.Add("Special Request must be selected.");

            if (checkIn.Date >= checkOut.Date)
                errors.Add("Check-Out Date must be after Check-In Date.");

            return errors;
        }
    }
}
