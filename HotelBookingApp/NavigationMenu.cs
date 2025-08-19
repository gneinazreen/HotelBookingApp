using System;
using System.Linq;
using System.Windows.Forms;
using HotelBookingApp.Services;
using HotelBookingApp.Views;

namespace HotelBookingApp
{
    public partial class NavigationMenu : UserControl
    {
        private ApiClient _api;   // injected at runtime

        public NavigationMenu()
        {
            InitializeComponent();
            BuildMenu();
        }

        /// <summary>Inject the ApiClient from your host form (e.g., Main).</summary>
        public void SetApi(ApiClient api) => _api = api ?? throw new ArgumentNullException(nameof(api));

        private void BuildMenu()
        {
            // Use the designer-created MenuStrip if available; otherwise create one.
            MenuStrip menu;
            try
            {
                // 'menuStrip1' is generated in NavigationMenu.Designer.cs
                menu = this.menuStrip1;
            }
            catch
            {
                menu = null;
            }

            if (menu == null)
            {
                menu = new MenuStrip { Dock = DockStyle.Top };
                Controls.Add(menu);
            }
            else
            {
                menu.Items.Clear();
            }

            var bookings = new ToolStripMenuItem("Bookings");
            bookings.Click += (s, e) =>
            {
                if (!EnsureApi()) return;
                OpenOrActivate(() => new BookingForm(_api));
            };

            var rooms = new ToolStripMenuItem("Rooms");
            rooms.Click += (s, e) =>
            {
                if (!EnsureApi()) return;
                OpenOrActivate(() => new RoomForm(_api));
            };

            var requests = new ToolStripMenuItem("Requests");
            requests.Click += (s, e) =>
            {
                if (!EnsureApi()) return;
                OpenOrActivate(() => new RequestForm(_api));
            };

            var reports = new ToolStripMenuItem("Reports");
            reports.Click += (s, e) =>
            {
                if (!EnsureApi()) return;
                OpenOrActivate(() => new ReportForm(_api));
            };

            var chatbot = new ToolStripMenuItem("Chatbot");
            chatbot.Click += (s, e) =>
            {
                OpenOrActivate(() => new ChatbotForm());
            };

            menu.Items.Add(bookings);
            menu.Items.Add(rooms);
            menu.Items.Add(requests);
            menu.Items.Add(reports);
            menu.Items.Add(chatbot);
        }

        // Designer likely wires this; keep a harmless stub so the designer opens.
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // No-op. We handle clicks per-item in BuildMenu().
        }

        private static void OpenOrActivate<T>(Func<T> factory) where T : Form
        {
            var existing = Application.OpenForms.OfType<T>().FirstOrDefault();
            if (existing == null)
            {
                var f = factory();
                f.Show(); // modeless
            }
            else
            {
                if (existing.WindowState == FormWindowState.Minimized)
                    existing.WindowState = FormWindowState.Normal;
                existing.BringToFront();
                existing.Activate();
            }
        }

        private bool EnsureApi()
        {
            if (_api != null) return true;
            MessageBox.Show(
                "API client is not configured. Call navigationMenu1.SetApi(api) from the host form.",
                "Configuration",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }
    }
}
