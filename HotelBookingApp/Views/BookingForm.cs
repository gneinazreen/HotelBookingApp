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
            foreach (var b in DataStorage.Bookings)
            {
                var item = new ListViewItem(b.BookingId.ToString());
                item.SubItems.Add(b.FirstName);
                item.SubItems.Add(b.LastName);
                item.SubItems.Add(b.RoomType);
                item.SubItems.Add(b.CheckInDate.ToShortDateString());
                item.SubItems.Add(b.CheckOutDate.ToShortDateString());
                item.SubItems.Add(b.SpecialRequests);
                listViewBookings.Items.Add(item);
            }
        }
        private void LName_Click(object sender, EventArgs e)
        {

        }


        private void BookingForm_Load(object sender, EventArgs e)
        {

        }

        private void listViewBookings_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var newBooking = new Models.Booking
            {
                FirstName = txtFName.Text.Trim(),
                LastName = txtLName.Text.Trim(),
                RoomType = cmbRoomType.Text,
                CheckInDate = checkInDate.Value.Date,
                CheckOutDate = checkOutDate.Value.Date,
                SpecialRequests = txtSpecialRequest.Text.Trim(),
                IsRecurring = checkRecurring.Checked,
                RecurrencePattern = cmbRecPattern.Text
            };

            DataStorage.AddBooking(newBooking);
            LoadBookings();
            ClearFields();
        }

        private void ClearFields()
        {
            txtFName.Clear();
            txtLName.Clear();
            cmbRoomType.SelectedIndex = -1;
            checkInDate.Value = DateTime.Today;
            checkOutDate.Value = DateTime.Today.AddDays(1);
            txtSpecialRequest.Clear();
            checkRecurring.Checked = false;
            cmbRecPattern.SelectedIndex = -1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listViewBookings.SelectedItems.Count > 0)
            {
                int id = int.Parse(listViewBookings.SelectedItems[0].Text);
                DataStorage.DeleteBooking(id);
                LoadBookings();
            }
        }

        private void FName_Click(object sender, EventArgs e)
        {

        }
    }
}
