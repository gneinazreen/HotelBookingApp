using HotelBookingApp.Services;
using System;
using System.Windows.Forms;

namespace HotelBookingApp.Views
{
    public partial class Main : Form
    {
        private ApiClient _api;

        // Designer-safe ctor (no network work here)
        public Main()
        {
            InitializeComponent();
            _api = null;
        }

        // Runtime ctor — inject ApiClient from Program.cs
        public Main(ApiClient api) : this()
        {
            _api = api;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = "Hotel Booking Management - Dashboard";
            WireNavigationMenuApi();   // pass ApiClient to the top menu (if present)
        }

        private void btnBookings_Click(object sender, EventArgs e)
        {
            if (!EnsureApi()) return;
            using (var bookingForm = new BookingForm(_api))
                bookingForm.ShowDialog(this);
        }

        private void btnRooms_Click(object sender, EventArgs e)
        {
            if (!EnsureApi()) return;
            using (var roomForm = new RoomForm(_api))
                roomForm.ShowDialog(this);
        }

        private void btnRequests_Click(object sender, EventArgs e)
        {
            if (!EnsureApi()) return;
            using (var requestForm = new RequestForm(_api))
                requestForm.ShowDialog(this);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            if (!EnsureApi()) return;
            using (var reportForm = new ReportForm(_api))
                reportForm.ShowDialog(this);
        }

        private void btnChatbot_Click(object sender, EventArgs e)
        {
            // ChatbotForm can stay local-data based for now
            using (var chatbotForm = new ChatbotForm())
                chatbotForm.ShowDialog(this);
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private bool EnsureApi()
        {
            if (_api != null) return true;
            MessageBox.Show(
                "API client is not configured. Start the app via Program.cs that passes an ApiClient.",
                "Configuration",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }

        /// <summary>
        /// If the designer added a NavigationMenu control named 'navigationMenu1',
        /// inject the ApiClient so its items open API-backed forms.
        /// </summary>
        private void WireNavigationMenuApi()
        {
            try
            {
                // The control is generated in Main.Designer.cs if you dropped it on the form.
                var menuField = this.GetType().GetField("navigationMenu1",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                if (menuField == null) return; // no menu on this form

                var menu = menuField.GetValue(this) as NavigationMenu;
                if (menu == null) return;

                if (_api != null)
                {
                    menu.SetApi(_api); // method provided in the updated NavigationMenu.cs
                }
            }
            catch
            {
                // Safe no-op if the control name or type differs.
            }
        }
    }
}
