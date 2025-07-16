using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelBookingApp.Views.Booking
{
    public partial class BookingForm : Form
    {
        public BookingForm()
        {
            InitializeComponent();
            LoadBookings();
        }

        private void LoadBookings()
        {
            listViewBookings.Items.Clear();
            foreach (var b in Data)
        }
        private void LName_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void BookingForm_Load(object sender, EventArgs e)
        {

        }

        private void listViewBookings_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
