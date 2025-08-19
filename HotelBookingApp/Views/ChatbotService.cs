using System;
using System.Linq;
using System.Collections.Generic;
using HotelBookingApp.Models;

public static class ChatbotService
{
    public static string GetResponse(string question)
    {
        var bookings = DataStorage.Bookings;
        var rooms = DataStorage.Rooms;

        DateTime targetDate = ExtractDate(question) ?? DateTime.Today.AddDays(7);
        question = question.ToLower();
        if (question.Contains("price") || question.Contains("cost"))
        {
            return PredictPrice(question, rooms, bookings);
        }
        else if (question.Contains("available") || question.Contains("room"))
        {
            return CheckAvailability(question, targetDate, rooms, bookings);
        }
        else if (
            question.Contains("busiest") ||
            question.Contains("crowded") ||
            (question.Contains("most") && question.Contains("booked")) ||
            question.Contains("most booked day") ||
            question.Contains("day with most bookings")
)
        {
            return PredictBusiestDay(bookings);
        }

        return "Try asking about availability, room prices, or the busiest day.";
    }

    private static DateTime? ExtractDate(string question)
    {
        foreach (var word in question.Split(' '))
        {
            if (DateTime.TryParse(word, out DateTime parsed))
                return parsed;
        }
        return null;
    }

    private static string CheckAvailability(string question, DateTime date, List<Room> rooms, List<Booking> bookings)
    {
        string roomType = rooms.FirstOrDefault(r => question.Contains(r.RoomType.ToLower()))?.RoomType;

        if (string.IsNullOrEmpty(roomType))
            return "Please specify a room type (e.g., single, double, suite).";

        int roomId = rooms.First(r => r.RoomType.ToLower() == roomType.ToLower()).RoomId;
        int booked = bookings.Count(b => b.RoomId == roomId && b.CheckInDate <= date && b.CheckOutDate > date);
        int totalAvailable = 5; 

        return booked < totalAvailable
            ? $"{roomType} rooms are available on {date:MMMM dd}."
            : $"No {roomType} rooms available on {date:MMMM dd}.";
    }

    private static string PredictPrice(string question, List<Room> rooms, List<Booking> bookings)
    {
        string roomType = rooms.FirstOrDefault(r => question.Contains(r.RoomType.ToLower()))?.RoomType;

        if (string.IsNullOrEmpty(roomType))
            return "Please specify a room type for price prediction.";

        var room = rooms.First(r => r.RoomType.ToLower() == roomType.ToLower());
        int count = bookings.Count(b => b.RoomId == room.RoomId);
        decimal predictedPrice = count > 10 ? room.BasePrice + 20 : room.BasePrice;

        return $"Expected price for {roomType}: ${predictedPrice} ({(count > 10 ? "high" : "normal")} demand)";
    }

    private static string PredictBusiestDay(List<Booking> bookings)
    {
        var busiest = bookings
            .SelectMany(b => Enumerable.Range(0, (b.CheckOutDate - b.CheckInDate).Days)
            .Select(offset => b.CheckInDate.AddDays(offset).DayOfWeek))
            .GroupBy(d => d)
            .OrderByDescending(g => g.Count())
            .FirstOrDefault()?.Key;

        return $"The busiest day is likely: {busiest}";
    }
}
