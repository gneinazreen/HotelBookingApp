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
        private ApiClient _api;
        public Main()
        {
            InitializeComponent();
            _api = null;
        }
        public Main(ApiClient api) : this()
        {
            _api = api;
        }
        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = "Hotel Booking Management - Dashboard";
        }

        private void btnBookings_Click(object sender, EventArgs e)
        {
            //BookingForm bookingForm = new BookingForm();
            //bookingForm.ShowDialog();
            if (!EnsureApi()) return;
            using (var bookingForm = new BookingForm(_api))
                bookingForm.ShowDialog(this);
        }

        private void btnRooms_Click(object sender, EventArgs e)
        {
            //BookingForm bookingForm = new BookingForm();
            //RoomForm roomForm = new RoomForm(bookingForm);
            //roomForm.ShowDialog();
            if (!EnsureApi()) return;
            using (var roomForm = new RoomForm(_api))
                roomForm.ShowDialog(this);
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
        private bool EnsureApi()
        {
            if (_api != null) return true;
            MessageBox.Show(
                "API client is not configured. Make sure you start the app via Program.cs that passes an ApiClient.",
                "Configuration",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }
    }
}
