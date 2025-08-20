using System;
using System.Collections.Generic;
using System.ComponentModel;   
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelBooking.Contracts;     // SpecialRequestDto
using HotelBookingApp.Services;       // ApiClient

namespace HotelBookingApp.Views
{
    public partial class RequestForm : Form
    {
        private ApiClient _api;                                   // injected at runtime
        private List<SpecialRequestDto> _requests = new List<SpecialRequestDto>();

        // DESIGNER-SAFE CTOR
        public RequestForm()
        {
            InitializeComponent();
            this.Name = "RequestForm";
        }

        // RUNTIME CTOR (inject ApiClient from Main form)
        public RequestForm(ApiClient api) : this()
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        private async void RequestForm_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || _api == null)
                return;

            await LoadRequestsAsync();
        }

        // READ
        private async Task LoadRequestsAsync()
        {
            try
            {
                _requests = await _api.GetRequests() ?? new List<SpecialRequestDto>();

                listViewRequests.Items.Clear();
                foreach (var r in _requests)
                {
                    var item = new ListViewItem(r.RequestId.ToString());
                    item.SubItems.Add(r.Description);
                    item.SubItems.Add(r.Category);
                    listViewRequests.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load requests:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // CREATE
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (_api == null) return;

            var desc = (txtDescription.Text ?? "").Trim();
            var cat = (txtCategory.Text ?? "").Trim();

            var errors = ValidateInputs(desc, cat);
            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new SpecialRequestDto
            {
                RequestId = 0,
                Description = desc,
                Category = cat
            };

            try
            {
                await _api.CreateRequest(dto);
                await LoadRequestsAsync();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add request:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // UPDATE
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_api == null) return;
            if (listViewRequests.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a request to update.");
                return;
            }

            if (!int.TryParse(listViewRequests.SelectedItems[0].Text, out var id)) return;

            var existing = _requests.FirstOrDefault(r => r.RequestId == id);
            if (existing == null) return;

            var newDesc = (txtDescription.Text ?? "").Trim();
            var newCat = (txtCategory.Text ?? "").Trim();

            var finalDesc = string.IsNullOrEmpty(newDesc) ? existing.Description : newDesc;
            var finalCat = string.IsNullOrEmpty(newCat) ? existing.Category : newCat;

            var errors = ValidateInputs(finalDesc, finalCat);
            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new SpecialRequestDto
            {
                RequestId = id,
                Description = finalDesc,
                Category = finalCat
            };

            try
            {
                await _api.UpdateRequest(dto);
                await LoadRequestsAsync();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update request:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // DELETE
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_api == null) return;
            if (listViewRequests.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a request to delete.");
                return;
            }

            if (!int.TryParse(listViewRequests.SelectedItems[0].Text, out var id)) return;

            var confirm = MessageBox.Show("Delete this request?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                await _api.DeleteRequest(id);
                await LoadRequestsAsync();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete request:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // UI wiring
        private void listViewRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewRequests.SelectedItems.Count == 0) return;

            if (!int.TryParse(listViewRequests.SelectedItems[0].Text, out var id)) return;

            var r = _requests.FirstOrDefault(x => x.RequestId == id);
            if (r == null) return;

            txtDescription.Text = r.Description;
            txtCategory.Text = r.Category;
        }

        private void txtDescription_TextChanged(object sender, EventArgs e) { }

        // helpers
        private static List<string> ValidateInputs(string description, string category)
        {
            var errs = new List<string>();
            if (string.IsNullOrWhiteSpace(description)) errs.Add("Description is required.");
            if (string.IsNullOrWhiteSpace(category)) errs.Add("Category is required.");
            return errs;
        }

        private void ClearFields()
        {
            txtDescription.Clear();
            txtCategory.Clear();
            listViewRequests.SelectedItems.Clear();
        }
    }
}
