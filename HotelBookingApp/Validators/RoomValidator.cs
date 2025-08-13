using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingApp.Models;

namespace HotelBookingApp.Validators
{
    public static class RoomValidator
    {
        public static List<string> ValidateRoom(Room room)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(room.RoomType))
                errors.Add("Room Type is required.");

            if (room.BasePrice <= 0m)
                errors.Add("Base Price must be a positive number.");

            return errors;
        }
    }
}
