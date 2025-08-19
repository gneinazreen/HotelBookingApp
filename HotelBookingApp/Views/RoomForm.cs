using System;
using System.Collections.Generic;
using System.ComponentModel;   // LicenseManager.UsageMode
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelBooking.Contracts;     // RoomDto
using HotelBookingApp.Services;       // ApiClient
using HotelBookingApp.Models;         // for RoomValidator input object
using HotelBookingApp.Validators;     // RoomValidator
using System.Globalization;
namespace HotelBookingApp.Views
{
    public partial class RoomForm : Form
    {
        private ApiClient _api;                   // set at runtime ctor
        private List<RoomDto> _rooms = new List<RoomDto>();

        // ---- DESIGNER-ONLY CTOR (no API, no IO) ----
        public RoomForm()
        {
            InitializeComponent();
            this.Name = "RoomForm";
        }

        // ---- RUNTIME CTOR ----
        public RoomForm(ApiClient api) : this()
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        // ---- LOAD ----
        private async void RoomForm_Load(object sender, EventArgs e)
        {
            // Don’t run when the WinForms designer instantiates the form
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || _api == null)
                return;

            await LoadRoomsAsync();
        }

        // ---- READ ----
        private async Task LoadRoomsAsync()
        {
            try
            {
                _rooms = await _api.GetRooms() ?? new List<RoomDto>();
                listViewRooms.Items.Clear();

                foreach (var r in _rooms)
                {
                    var item = new ListViewItem(r.RoomId.ToString());
                    item.SubItems.Add(r.RoomType);
                    item.SubItems.Add(r.BasePrice.ToString("0.00"));
                    listViewRooms.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load rooms:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---- CREATE ----
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (_api == null) return;

            if (!decimal.TryParse(txtBasePrice.Text.Trim(),
                                  NumberStyles.Number,
                                  CultureInfo.CurrentCulture,
                                  out var basePriceValue))
            {
                MessageBox.Show("Base Price must be a valid number.");
                return;
            }

            var roomForValidation = new Room
            {
                RoomType = txtRoomType.Text.Trim(),
                BasePrice = basePriceValue
            };

            var errors = HotelBookingApp.Validators.RoomValidator.ValidateRoom(roomForValidation);
            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new RoomDto
            {
                RoomId = 0,
                RoomType = roomForValidation.RoomType,
                BasePrice = (decimal)roomForValidation.BasePrice
            };

            try
            {
                await _api.CreateRoom(dto);
                await LoadRoomsAsync();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add room:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---- UPDATE ----
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_api == null) return;
            if (listViewRooms.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a room to update.");
                return;
            }

            int id;
            if (!int.TryParse(listViewRooms.SelectedItems[0].Text, out id))
                return;

            float priceFloat;
            var newType = txtRoomType.Text.Trim();
            var priceText = txtBasePrice.Text.Trim();

            // If price field provided, validate it; otherwise keep existing
            decimal newPriceDecimal;
            if (!string.IsNullOrEmpty(priceText))
            {
                if (!float.TryParse(priceText, out priceFloat) || priceFloat <= 0)
                {
                    MessageBox.Show("Base Price must be a positive number.");
                    return;
                }
                newPriceDecimal = (decimal)priceFloat;
            }
            else
            {
                var existing = _rooms.FirstOrDefault(r => r.RoomId == id);
                newPriceDecimal = existing != null ? existing.BasePrice : 0m;
            }

            if (string.IsNullOrWhiteSpace(newType))
            {
                var existing = _rooms.FirstOrDefault(r => r.RoomId == id);
                newType = existing != null ? existing.RoomType : "";
            }

            var dto = new RoomDto
            {
                RoomId = id,
                RoomType = newType,
                BasePrice = newPriceDecimal
            };

            try
            {
                await _api.UpdateRoom(dto);
                await LoadRoomsAsync();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update room:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---- DELETE ----
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_api == null) return;
            if (listViewRooms.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a room to delete.");
                return;
            }

            int id;
            if (!int.TryParse(listViewRooms.SelectedItems[0].Text, out id))
                return;

            var confirm = MessageBox.Show("Delete this room?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                await _api.DeleteRoom(id);
                await LoadRoomsAsync();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete room:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---- SELECTION ----
        private void listViewRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewRooms.SelectedItems.Count == 0) return;

            int id;
            if (!int.TryParse(listViewRooms.SelectedItems[0].Text, out id))
                return;

            var room = _rooms.FirstOrDefault(r => r.RoomId == id);
            if (room == null) return;

            txtRoomType.Text = room.RoomType;
            txtBasePrice.Text = room.BasePrice.ToString("0.00");
        }

        // ---- HELPERS ----
        private void ClearFields()
        {
            txtRoomType.Clear();
            txtBasePrice.Clear();
            listViewRooms.SelectedItems.Clear();
        }

        // ---- unused handlers kept for designer wiring ----
        private void txtRoomType_TextChanged(object sender, EventArgs e) { }
    }
}
