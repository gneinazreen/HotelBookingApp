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
using HotelBookingApp.Validators;

namespace HotelBookingApp.Views.Booking
{
    public partial class BookingForm : Form
    {
        private ApiClient _api;
        public BookingForm()
        {
            InitializeComponent();
            _api = null;
        }
        public BookingForm(ApiClient api) : this()
        {
            InitializeComponent();
            if (api == null) throw new ArgumentNullException(nameof(api));
            _api = api;
            this.Name = "BookingForm";
            UpdateRoomTypes();
            UpdateRequests();
            LoadBookings();
            //var nav = new NavigationMenu();
            //nav.Dock = DockStyle.Top;
            //this.Controls.Add(nav);
            //this.Controls.SetChildIndex(nav, 0);
            checkRecurring.CheckedChanged += (s, e) =>
            {
                lblRecurrence.Visible = checkRecurring.Checked;
                cmbRecPattern.Visible = checkRecurring.Checked;
            };
            cmbRecPattern.Visible = false;
            lblRecurrence.Visible = false;
        }

        private void LoadBookings()
        {
            listViewBookings.Items.Clear();
            foreach (var b in DataStorage.Bookings)
            {
                var item = new ListViewItem(b.BookingId.ToString());
                item.SubItems.Add(b.FirstName);
                item.SubItems.Add(b.LastName);
                //item.SubItems.Add(b.RoomType);
                var room = DataStorage.Rooms.FirstOrDefault(r => r.RoomId == b.RoomId);
                string roomType = room?.RoomType ?? "Unknown";
                item.SubItems.Add(roomType);

                item.SubItems.Add(b.CheckInDate.ToShortDateString());
                item.SubItems.Add(b.CheckOutDate.ToShortDateString());
                //item.SubItems.Add(b.SpecialRequests);

                var request = DataStorage.Requests.FirstOrDefault(r => r.RequestId == b.RequestId);
                string requestDesc = request?.Description ?? "Unknown";
                item.SubItems.Add(requestDesc);

                item.SubItems.Add(b.RecurrencePattern);
                listViewBookings.Items.Add(item);
            }
        }

        public void UpdateRoomTypes()
        {
            cmbRoomType.Items.Clear();
            foreach (var room in DataStorage.Rooms)
            {
                cmbRoomType.Items.Add(room.RoomType);
            }
        }
        public void UpdateRequests()
        {
            cmbRequests.Items.Clear();
            foreach (var request in DataStorage.Requests)
            {
                cmbRequests.Items.Add(request.Description);
            }
        }
        private void LName_Click(object sender, EventArgs e)
        {

        }


        private void BookingForm_Load(object sender, EventArgs e)
        {
            // Disable past dates
            checkInDate.MinDate = DateTime.Today;
            checkOutDate.MinDate = DateTime.Today.AddDays(1);
        }
        private void listViewBookings_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            listViewBookings.Items[e.Index].Selected = true;
        }

        private void listViewBookings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listViewBookings.SelectedItems.Count > 0)
            {
                int id = int.Parse(listViewBookings.SelectedItems[0].Text);
                var booking = DataStorage.Bookings.Find(b => b.BookingId == id);

                if(booking != null)
                {
                    txtFName.Text = booking.FirstName;
                    txtLName.Text = booking.LastName;
                    //cmbRoomType.Text = booking.RoomType;

                    var room = DataStorage.Rooms.FirstOrDefault(r=>r.RoomId == booking.RoomId);
                    cmbRoomType.Text = room?.RoomType ?? "";

                    checkInDate.Value = booking.CheckInDate;
                    checkOutDate.Value = booking.CheckOutDate;
                    //cmbRequests.Text = booking.SpecialRequests;

                    var request = DataStorage.Requests.FirstOrDefault(r=>r.RequestId == booking.RequestId);
                    cmbRequests.Text = request?.Description ?? "";
                    checkRecurring.Checked = booking.IsRecurring;
                    cmbRecPattern.Text = booking.RecurrencePattern;
                }
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(txtFName.Text) ||
            //    string.IsNullOrWhiteSpace(txtLName.Text) ||
            //    cmbRoomType.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Please fill in all required fields.");
            //    return;
            //}

            int roomId = DataStorage.Rooms.FirstOrDefault(r => r.RoomType == cmbRoomType.Text)?.RoomId ?? 0;
            int requestId = DataStorage.Requests.FirstOrDefault(r => r.Description == cmbRequests.Text)?.RequestId ?? 0;

            var errors = BookingValidator.ValidateBooking(
                txtFName.Text.Trim(),
                txtLName.Text.Trim(),
                cmbRoomType.Text.Trim(),
                cmbRequests.Text.Trim(),
                checkInDate.Value.Date,
                checkOutDate.Value.Date
            );
            if (errors.Any())
            {
                MessageBox.Show(string.Join("\n", errors), "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var newBooking = new Models.Booking
            {
                FirstName = txtFName.Text.Trim(),
                LastName = txtLName.Text.Trim(),
                RoomId = roomId,
                RequestId = requestId,
                CheckInDate = checkInDate.Value.Date,
                CheckOutDate = checkOutDate.Value.Date,
                IsRecurring = checkRecurring.Checked,
                RecurrencePattern = checkRecurring.Checked ? cmbRecPattern.Text : "None"
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
            cmbRequests.SelectedIndex = -1;
            checkRecurring.Checked = false;
            cmbRecPattern.SelectedIndex = -1;
            listViewBookings.SelectedItems.Clear();
        }

        //private void btnAdd_Click_1(object sender, EventArgs e)
        //{
        //    if (listViewBookings.SelectedItems.Count > 0)
        //    {
        //        int id = int.Parse(listViewBookings.SelectedItems[0].Text);
        //        DataStorage.DeleteBooking(id);
        //        LoadBookings();
        //    }
        //}

        private void FName_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (listViewBookings.SelectedItems.Count > 0)
            {
                int id = int.Parse(listViewBookings.SelectedItems[0].Text);
                var booking = DataStorage.Bookings.Find(b => b.BookingId == id);

                if (booking != null)
                {
                    string newFirst = txtFName.Text.Trim();
                    string newLast = txtLName.Text.Trim();
                    string newRoom = cmbRoomType.Text.Trim();
                    string newReq = cmbRequests.Text.Trim();
                    string newRecPattern = cmbRecPattern.Text.Trim();



                    booking.FirstName = UpdateIfNoEmpty(booking.FirstName, newFirst);
                    booking.LastName = UpdateIfNoEmpty(booking.LastName, newLast);
                    //booking.RoomType = UpdateIfNoEmpty(booking.RoomType, newRoom);
                    //booking.SpecialRequests = UpdateIfNoEmpty(booking.SpecialRequests, newReq);

                    var newRoomObj = DataStorage.Rooms.FirstOrDefault(r => r.RoomType == newRoom);
                    if (newRoomObj != null)
                        booking.RoomId = newRoomObj.RoomId;

                    var newReqObj = DataStorage.Requests.FirstOrDefault(r => r.Description == newReq);
                    if (newReqObj != null)
                        booking.RequestId = newReqObj.RequestId;
                    booking.IsRecurring = checkRecurring.Checked;
                    booking.RecurrencePattern = UpdateIfNoEmpty(booking.RecurrencePattern, newRecPattern);
                    
                    DateTime? newCheckIn = checkInDate.Value != DateTime.Today ? checkInDate.Value.Date : (DateTime?)null;
                    DateTime? newCheckOut = checkOutDate.Value != DateTime.Today ? checkOutDate.Value.Date: (DateTime?)null;

                    if(newCheckIn.HasValue)
                        booking.CheckInDate = newCheckIn.Value;
                    if(newCheckOut.HasValue)
                        booking.CheckOutDate = newCheckOut.Value;
                    LoadBookings();
                    ClearFields();

                }
            }
        }
        private string UpdateIfNoEmpty(string original, string newValue)
        {
            return string.IsNullOrEmpty(newValue) ? original : newValue;
        }

        private void cmbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbRequests_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listViewBookings_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            //var nav = new NavigationMenu();
            //nav.Dock = DockStyle.Top;
            //this.Controls.Add(nav);
            //this.Controls.SetChildIndex(nav, 0);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void checkRecurring_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkRecurring.Checked;
            cmbRecPattern.Visible = isChecked;
            lblRecurrence.Visible = isChecked;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listViewBookings.SelectedItems.Count > 0)
            {
                int id = int.Parse(listViewBookings.SelectedItems[0].Text);
                var booking = DataStorage.Bookings.FirstOrDefault(b => b.BookingId == id);
                if (booking != null)
                {
                    var confirm = MessageBox.Show($"Are you sure you want to delete booking for {booking.FirstName} {booking.LastName}?",
                                                  "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirm == DialogResult.Yes)
                    {
                        DataStorage.DeleteBooking(id);
                        LoadBookings();
                        ClearFields();
                        MessageBox.Show("Booking deleted successfully.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a booking to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtFName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
