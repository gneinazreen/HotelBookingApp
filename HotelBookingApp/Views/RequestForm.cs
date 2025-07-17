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

namespace HotelBookingApp.Views
{
    public partial class RequestForm : Form
    {
        public RequestForm()
        {
            InitializeComponent();
            this.Name = "RequestForm";
            LoadRequests();
        }
        private void LoadRequests()
        {
            listViewRequests.Items.Clear();
            foreach (var r in DataStorage.Requests)
            {
                var item = new ListViewItem(r.RequestId.ToString());
                item.SubItems.Add(r.Description);
                item.SubItems.Add(r.Category);
                listViewRequests.Items.Add(item);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var newRequest = new SpecialRequest
            {
                Description = txtDescription.Text,
                Category = txtCategory.Text
            };
            DataStorage.AddRequest(newRequest);
            LoadRequests();
            txtDescription.Clear();
            txtCategory.Clear();
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listViewRequests.SelectedItems.Count > 0)
            {
                int id = int.Parse(listViewRequests.SelectedItems[0].Text);
                DataStorage.DeleteRequest(id);
                LoadRequests();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(listViewRequests.SelectedItems.Count > 0)
            {
                int id = int.Parse(listViewRequests.SelectedItems[0].Text);
                var request = DataStorage.Requests.Find(r => r.RequestId == id);

                if(request != null)
                {
                    string newDesc = txtDescription.Text.Trim();
                    string newCategory = txtCategory.Text.Trim();

                    if(!string.IsNullOrEmpty(newDesc))
                        request.Description = newDesc;
                    if(request.Category != newCategory)
                        request.Category = newCategory;

                    LoadRequests();
                    ClearFields();

                }
            }
        }
        private void ClearFields()
        {
            txtDescription.Clear();
            txtCategory.Clear();
            listViewRequests.SelectedItems.Clear();
        }

        private void listViewRequests_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RequestForm_Load(object sender, EventArgs e)
        {

        }
    }
}
