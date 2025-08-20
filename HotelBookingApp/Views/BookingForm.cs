using System;
using System.Collections.Generic;
using System.ComponentModel;         
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelBooking.Contracts;         
using HotelBookingApp.Services;       
using HotelBookingApp.Validators;
using HotelBookingApp;
namespace HotelBookingApp.Views
{
    public partial class BookingForm : Form
    {
        private ApiClient _api;  

        // Local caches from API
        private List<RoomDto> _rooms = new List<RoomDto>();
        private Dictionary<int, RoomDto> _roomById = new Dictionary<int, RoomDto>();
        private List<SpecialRequestDto> _requests = new List<SpecialRequestDto>();
        private Dictionary<int, SpecialRequestDto> _requestById = new Dictionary<int, SpecialRequestDto>();
        private List<BookingDto> _bookings = new List<BookingDto>();
        private NavigationMenu _nav;

        // Helper item for ComboBoxes
        private class ComboItem
        {
            public int Value { get; set; }
            public string Text { get; set; }
            public override string ToString() => Text;
        }

        // Constructors

        // Designer-safe ctor
        public BookingForm()
        {
            InitializeComponent();
            this.Name = "BookingForm";

            // existing checkbox wiring
            checkRecurring.CheckedChanged += (s, e) =>
            {
                lblRecurrence.Visible = checkRecurring.Checked;
                cmbRecPattern.Visible = checkRecurring.Checked;
            };
            cmbRecPattern.Visible = false;
            lblRecurrence.Visible = false;

            // add the nav menu at the top
            _nav = new NavigationMenu { Dock = DockStyle.Top };
            Controls.Add(_nav);
            Controls.SetChildIndex(_nav, 0);
        }

        // Runtime ctor
        public BookingForm(ApiClient api) : this()
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
            _nav.SetApi(_api);                 
        }


        // Form events

        private async void BookingForm_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || _api == null)
                return;

            checkInDate.MinDate = DateTime.Today;
            checkOutDate.MinDate = DateTime.Today.AddDays(1);

            await LoadReferenceDataAsync();
            await LoadBookingsAsync();
        }

        private void listViewBookings_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            listViewBookings.Items[e.Index].Selected = true;
        }

        private void listViewBookings_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateFromSelection();
        }

        private void listViewBookings_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            PopulateFromSelection();
        }

        private void checkRecurring_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkRecurring.Checked;
            cmbRecPattern.Visible = isChecked;
            lblRecurrence.Visible = isChecked;
        }

        // Data loading

        private async Task LoadReferenceDataAsync()
        {
            if (_api == null || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            try
            {
                // ApiClient methods should return non-null lists
                var rooms = await _api.GetRooms();               // List<RoomDto>
                var reqs = await _api.GetRequests();            // List<SpecialRequestDto>

                _rooms = rooms;
                _roomById = _rooms.ToDictionary(r => r.RoomId, r => r);

                _requests = reqs;
                _requestById = _requests.ToDictionary(r => r.RequestId, r => r);

                // Repopulate combos
                cmbRoomType.BeginUpdate();
                cmbRoomType.Items.Clear();
                foreach (var r in _rooms)
                    cmbRoomType.Items.Add(new ComboItem { Value = r.RoomId, Text = r.RoomType });
                cmbRoomType.EndUpdate();

                cmbRequests.BeginUpdate();
                cmbRequests.Items.Clear();
                foreach (var s in _requests)
                    cmbRequests.Items.Add(new ComboItem { Value = s.RequestId, Text = s.Description });
                cmbRequests.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load rooms/requests.\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Reset local caches and UI on failure
                _rooms.Clear();
                _roomById.Clear();
                _requests.Clear();
                _requestById.Clear();
                cmbRoomType.Items.Clear();
                cmbRequests.Items.Clear();
            }
        }


        private async Task LoadBookingsAsync()
        {
            if (_api == null) return;

            try
            {
                _bookings = await _api.GetBookings();   // guaranteed non-null from ApiClient

                listViewBookings.BeginUpdate();
                listViewBookings.Items.Clear();

                foreach (var b in _bookings)
                {
                    var roomType = _roomById.TryGetValue(b.RoomId, out var r) ? r.RoomType : "Unknown";
                    var reqDesc = _requestById.TryGetValue(b.RequestId, out var q) ? q.Description : "Unknown";

                    var item = new ListViewItem(b.BookingId.ToString());
                    item.SubItems.Add(b.FirstName);
                    item.SubItems.Add(b.LastName);
                    item.SubItems.Add(roomType);
                    item.SubItems.Add(b.CheckIn.ToShortDateString());
                    item.SubItems.Add(b.CheckOut.ToShortDateString());
                    item.SubItems.Add(reqDesc);
                    item.SubItems.Add(string.IsNullOrWhiteSpace(b.RecurrencePattern) ? "None" : b.RecurrencePattern);

                    listViewBookings.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load bookings.\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                _bookings = new List<BookingDto>();
                listViewBookings.Items.Clear();
            }
            finally
            {
                listViewBookings.EndUpdate();
            }
        }



        // Called by RoomForm after room CRUD
        public void UpdateRoomTypes()
        {
            cmbRoomType.Items.Clear();
            var items = _rooms
                .Select(r => new ComboItem { Value = r.RoomId, Text = r.RoomType })
                .Cast<object>()
                .ToArray();
            cmbRoomType.Items.AddRange(items);
        }

        public void UpdateRequests()
        {
            cmbRequests.Items.Clear();
            var items = _requests
                .Select(r => new ComboItem { Value = r.RequestId, Text = r.Description })
                .Cast<object>()
                .ToArray();
            cmbRequests.Items.AddRange(items);
        }

        private int GetSelectedRoomId()
            => (cmbRoomType.SelectedItem as ComboItem)?.Value ?? 0;

        private int GetSelectedRequestId()
            => (cmbRequests.SelectedItem as ComboItem)?.Value ?? 0;

        private void SelectComboByValue(ComboBox combo, int value)
        {
            for (int i = 0; i < combo.Items.Count; i++)
            {
                if ((combo.Items[i] as ComboItem)?.Value == value)
                {
                    combo.SelectedIndex = i;
                    return;
                }
            }
            combo.SelectedIndex = -1;
        }

        private void PopulateFromSelection()
        {
            if (listViewBookings.SelectedItems.Count == 0) return;
            if (!int.TryParse(listViewBookings.SelectedItems[0].Text, out var id)) return;

            var booking = _bookings.FirstOrDefault(b => b.BookingId == id);
            if (booking == null) return;

            txtFName.Text = booking.FirstName;
            txtLName.Text = booking.LastName;

            SelectComboByValue(cmbRoomType, booking.RoomId);
            SelectComboByValue(cmbRequests, booking.RequestId);

            checkInDate.Value = booking.CheckIn.Date;
            checkOutDate.Value = booking.CheckOut.Date;

            checkRecurring.Checked = booking.IsRecurring;
            cmbRecPattern.Text = string.IsNullOrWhiteSpace(booking.RecurrencePattern) ? "None" : booking.RecurrencePattern;
        }

        // CRUD

        private async void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (_api == null) return;

            int roomId = GetSelectedRoomId();
            int requestId = GetSelectedRequestId();

            var errors = BookingValidator.ValidateBooking(
                txtFName.Text.Trim(),
                txtLName.Text.Trim(),
                roomId,
                requestId,
                checkInDate.Value.Date,
                checkOutDate.Value.Date
            );

            if (errors.Any())
            {
                MessageBox.Show(string.Join("\n", errors), "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // use object initializer (class DTO)
            var dto = new BookingDto
            {
                BookingId = 0,
                FirstName = txtFName.Text.Trim(),
                LastName = txtLName.Text.Trim(),
                RoomId = roomId,
                RequestId = requestId,
                CheckIn = checkInDate.Value.Date,
                CheckOut = checkOutDate.Value.Date,
                IsRecurring = checkRecurring.Checked,
                RecurrencePattern = checkRecurring.Checked ? (cmbRecPattern.Text ?? "None") : "None"
            };

            try
            {
                await _api.CreateBooking(dto);
                await LoadBookingsAsync();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create booking.\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_api == null) return;

            if (listViewBookings.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a booking to update.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!int.TryParse(listViewBookings.SelectedItems[0].Text, out var id)) return;

            int roomId = GetSelectedRoomId();
            int requestId = GetSelectedRequestId();

            var errors = BookingValidator.ValidateBooking(
                txtFName.Text.Trim(),
                txtLName.Text.Trim(),
                roomId,
                requestId,
                checkInDate.Value.Date,
                checkOutDate.Value.Date
            );
            if (errors.Any())
            {
                MessageBox.Show(string.Join("\n", errors), "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // use object initializer (class DTO)
            var dto = new BookingDto
            {
                BookingId = id,
                FirstName = txtFName.Text.Trim(),
                LastName = txtLName.Text.Trim(),
                RoomId = roomId,
                RequestId = requestId,
                CheckIn = checkInDate.Value.Date,
                CheckOut = checkOutDate.Value.Date,
                IsRecurring = checkRecurring.Checked,
                RecurrencePattern = checkRecurring.Checked ? (cmbRecPattern.Text ?? "None") : "None"
            };

            try
            {
                await _api.UpdateBooking(dto);
                await LoadBookingsAsync();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update booking.\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_api == null) return;

            if (listViewBookings.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a booking to delete.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!int.TryParse(listViewBookings.SelectedItems[0].Text, out var id)) return;

            var confirm = MessageBox.Show("Are you sure you want to delete this booking?",
                                          "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                await _api.DeleteBooking(id);
                await LoadBookingsAsync();
                ClearFields();
                MessageBox.Show("Booking deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete booking.\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Misc UI handlers

        private void LName_Click(object sender, EventArgs e) { }
        private void FName_Click(object sender, EventArgs e) { }
        private void cmbRoomType_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbRequests_SelectedIndexChanged(object sender, EventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void txtFName_TextChanged(object sender, EventArgs e) { }

        // Helpers

        private void ClearFields()
        {
            txtFName.Clear();
            txtLName.Clear();
            cmbRoomType.SelectedIndex = -1;
            cmbRequests.SelectedIndex = -1;
            checkInDate.Value = DateTime.Today;
            checkOutDate.Value = DateTime.Today.AddDays(1);
            checkRecurring.Checked = false;
            cmbRecPattern.SelectedIndex = -1;
            listViewBookings.SelectedItems.Clear();
        }
    }
}
