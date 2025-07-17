using HotelBookingApp.Models;
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
    public partial class RoomForm : Form
    {
        private BookingForm formBooking;
        public RoomForm(BookingForm bookingForm)
        {
            InitializeComponent();
            formBooking = bookingForm;
            LoadRooms();
        }

        private void txtRoomType_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadRooms()
        {
            listViewRooms.Items.Clear();
            foreach (var r in DataStorage.Rooms)
            {
                var item = new ListViewItem(r.RoomId.ToString());
                item.SubItems.Add(r.RoomType);
                item.SubItems.Add(r.BasePrice.ToString("0.00"));
                listViewRooms.Items.Add(item);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var newRoom = new Room
            {
                RoomType = txtRoomType.Text,
                BasePrice = float.TryParse(txtBasePrice.Text, out float price) ? price : 0f
            };
            DataStorage.AddRoom(newRoom);
            LoadRooms();
            formBooking?.UpdateRoomTypes();
            txtRoomType.Clear();
            txtBasePrice.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listViewRooms.SelectedItems.Count > 0)
            {
                int id = int.Parse(listViewRooms.SelectedItems[0].Text);
                DataStorage.DeleteRoom(id);
                LoadRooms();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (listViewRooms.SelectedItems.Count > 0)
            {
                int id = int.Parse(listViewRooms.SelectedItems[0].Text);
                var room = DataStorage.Rooms.Find(r => r.RoomId == id);

                if (room != null)
                {
                    string newType = txtRoomType.Text.Trim();
                    string newPriceText = txtBasePrice.Text.Trim();

                    room.RoomType = string.IsNullOrEmpty(newType) ? room.RoomType : newType;

                    if(float.TryParse(newPriceText, out float newPrice))
                    {
                        room.BasePrice = newPrice;
                    }

                    //room.BasePrice = float.TryParse(txtBasePrice.Text, out float price) ? price : 0f;

                    LoadRooms();
                    ClearFields();

                }
            }
        }
        private void ClearFields()
        {
            txtRoomType.Clear();
            txtBasePrice.Clear();
            listViewRooms.SelectedItems.Clear();
        }

        private void listViewRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewRooms.SelectedItems.Count > 0)
            {
                int id = int.Parse(listViewRooms.SelectedItems[0].Text);
                var room = DataStorage.Rooms.Find(r => r.RoomId == id);

                if (room != null)
                {
                    txtRoomType.Text = room.RoomType;
                    txtBasePrice.Text = room.BasePrice.ToString("F2");
                }
            }
        }
    }
}
