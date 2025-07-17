using HotelBookingApp.Views.Booking;
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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = "Hotel Booking Management - Dashboard";
        }

        private void btnBookings_Click(object sender, EventArgs e)
        {
            BookingForm bookingForm = new BookingForm();
            bookingForm.ShowDialog();
        }

        private void btnRooms_Click(object sender, EventArgs e)
        {
            BookingForm bookingForm = new BookingForm();
            RoomForm roomForm = new RoomForm(bookingForm);
            roomForm.ShowDialog();
        }

        private void btnRequests_Click(object sender, EventArgs e)
        {
            RequestForm requestForm = new RequestForm();
            requestForm.ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ReportForm reportForm = new ReportForm();
            reportForm.ShowDialog();
        }

        private void btnChatbot_Click(object sender, EventArgs e)
        {
            ChatbotForm chatbotForm = new ChatbotForm();
            chatbotForm.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
