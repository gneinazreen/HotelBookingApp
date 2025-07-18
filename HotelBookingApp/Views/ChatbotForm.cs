using HotelBookingApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelBookingApp.Views
{
    public partial class ChatbotForm : Form
    {
        public ChatbotForm()
        {
            InitializeComponent();
            this.Name = "ChatbotForm";
        }

        private void ChatbotForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAsk_Click(object sender, EventArgs e)
        {
            string question = txtQuestion.Text.ToLower();
            string response = GetChatbotResponse(question);
            txtResponse.Text = response;

        }
        private string GetChatbotResponse(string question)
        {
            var bookings = DataStorage.Bookings;
            var rooms = DataStorage.Rooms;

            DateTime targetDate = DateTime.Today.AddDays(7); 
            var words = question.Split(' ');
            foreach (var word in words)
            {
                if (DateTime.TryParse(word, out DateTime parsedDate))
                {
                    targetDate = parsedDate;
                    break;
                }
            }
            if (question.Contains("available") && question.Contains("single"))
            {
                int singleRoomId = rooms.FirstOrDefault(r => r.RoomType.ToLower() == "single")?.RoomId ?? 1;
                int booked = bookings.Count(b => b.RoomId == singleRoomId && b.CheckInDate <= targetDate && b.CheckOutDate > targetDate);
                return booked < 5 ? $"Single rooms are available on {targetDate:MMMM dd}." : $"No Single room available on {targetDate:MMMM dd}.";
            }

            if (question.Contains("price") && question.Contains("double"))
            {
                var basePrice = rooms.FirstOrDefault(r => r.RoomType.ToLower() == "double")?.BasePrice ?? 100;
                int doubleBookings = bookings.Count(b => b.RoomId == 2);
                if (doubleBookings > 10)
                {
                    return $"Expected price: ${basePrice + 20} (high demand)";
                }
                else
                {
                    return $"Expected price: ${basePrice} (normal demand)";
                }
            }
            if (question.Contains("busiest"))
            {
                var busiestDay = bookings
                    .SelectMany(b => Enumerable.Range(0, (b.CheckOutDate - b.CheckInDate).Days)
                    .Select(offset => b.CheckInDate.AddDays(offset).DayOfWeek))
                    .GroupBy(d => d)
                    .OrderByDescending(g => g.Count())
                    .FirstOrDefault()?.Key;

                return $"The busiest day is likely: {busiestDay}";
            }
            return "Try Asking about availability, price or buisest day.";
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
