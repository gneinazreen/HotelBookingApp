using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelBookingApp.Views;

namespace HotelBookingApp
{
    public partial class NavigationMenu : UserControl
    {
        public NavigationMenu()
        {
            InitializeComponent();
            InitializeMenu();
            //var menu = new NavigationMenu();
            //menu.Dock = DockStyle.Top;
            //this.Controls.Add(menu);
        }
        private void InitializeMenu()
        {
            var menu = new MenuStrip();
            menu.Dock = DockStyle.Top;
            var bookings = new ToolStripMenuItem("Bookings");
            bookings.Click += (s, e) =>
            {
                var openForm = Application.OpenForms["BookingForm"];
                if (openForm == null)
                    new Views.Booking.BookingForm().Show();
                else
                    openForm.BringToFront();
            };

            var rooms = new ToolStripMenuItem("Rooms");
            
            rooms.Click += (s, e) =>
            {
                var openForm = Application.OpenForms["RoomForm"];
                if (openForm == null)
                {
                    var bookingForm = Application.OpenForms["RoomForm"] as Views.Booking.BookingForm;
                    new Views.RoomForm(bookingForm).Show();
                }
                else
                {
                    openForm.BringToFront();
                }
            };

            var requests = new ToolStripMenuItem("Requests");
            requests.Click += (s, e) =>
            {
                var openForm = Application.OpenForms["RequestForm"];
                if (openForm == null)
                    new Views.RequestForm().Show();
                else
                    openForm.BringToFront();
            };

            var reports = new ToolStripMenuItem("Reports");
            reports.Click += (s, e) =>
            {
                var openForm = Application.OpenForms["ReportForm"];
                if (openForm == null)
                    new Views.ReportForm().Show();
                else
                    openForm.BringToFront();
            };

            var chatbot = new ToolStripMenuItem("Chatbot");
            chatbot.Click += (s, e) =>
            {
                var openForm = Application.OpenForms["ChatbotForm"];
                if (openForm == null)
                    new Views.ChatbotForm().Show();
                else
                    openForm.BringToFront();
            };
            // Add items to menu
            menu.Items.Add(bookings);
            menu.Items.Add(rooms);
            menu.Items.Add(requests);
            menu.Items.Add(reports);
            menu.Items.Add(chatbot);

            // Add menu to the UserControl
            this.Controls.Add(menu);

        }
        private void NavigationMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
