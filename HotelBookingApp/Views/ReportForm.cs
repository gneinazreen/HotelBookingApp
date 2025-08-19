using System;
using System.Collections.Generic;
using System.ComponentModel;             // LicenseManager.UsageMode
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelBooking.Contracts;        // WeeklyReportRow
using HotelBookingApp.Services;          // ApiClient
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace HotelBookingApp.Views
{
    public partial class ReportForm : Form
    {
        private ApiClient _api;                               // injected at runtime
        private List<WeeklyReportRow> _weeklyEntries = new List<WeeklyReportRow>();

        // -------- Designer-safe ctor (no network) --------
        public ReportForm()
        {
            InitializeComponent();
            this.Name = "ReportForm";

            // Snap the picker to the previous Monday by default (design-time safe)
            var monday = DateTime.Today;
            while (monday.DayOfWeek != DayOfWeek.Monday)
                monday = monday.AddDays(-1);
            dtpWeekStart.Value = monday;
        }

        // -------- Runtime ctor (inject ApiClient) --------
        public ReportForm(ApiClient api) : this()
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        private async void ReportForm_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || _api == null)
                return;

            await LoadWeeklyReportAsync(dtpWeekStart.Value);
        }

        // Keep the picker aligned to Mondays
        private void dtpWeekStart_ValueChanged(object sender, EventArgs e)
        {
            DateTime selected = dtpWeekStart.Value;
            while (selected.DayOfWeek != DayOfWeek.Monday)
                selected = selected.AddDays(-1);
            dtpWeekStart.Value = selected;
        }

        private async void btnLoadWeek_Click(object sender, EventArgs e)
        {
            if (_api == null) return;
            await LoadWeeklyReportAsync(dtpWeekStart.Value);
        }

        // -------------------- Core loader --------------------
        private async Task LoadWeeklyReportAsync(DateTime weekStart)
        {
            if (_api == null) return;

            try
            {
                var start = weekStart.Date;
                while (start.DayOfWeek != DayOfWeek.Monday)
                    start = start.AddDays(-1);
                var end = start.AddDays(7);

                // Pull check-in rows for the selected week
                var rows = await _api.GetDailyReport(start, end) ?? new List<WeeklyReportRow>();

                // Build a full Mon→Sun list and add "No bookings" placeholders
                var filled = new List<WeeklyReportRow>();
                listViewReport.Items.Clear();

                for (int i = 0; i < 7; i++)
                {
                    var day = start.AddDays(i).Date;
                    var rowsForDay = rows.Where(r => r.Day.Date == day).ToList();

                    if (rowsForDay.Count == 0)
                    {
                        // UI row
                        var item = new ListViewItem($"{day:dddd, yyyy-MM-dd}");
                        item.SubItems.Add("No Bookings");
                        listViewReport.Items.Add(item);

                        // Keep a placeholder for export
                        filled.Add(new WeeklyReportRow
                        {
                            Day = day,
                            Guest = "No bookings",
                            RoomType = "-",
                            Request = "-"
                        });
                    }
                    else
                    {
                        foreach (var r in rowsForDay)
                        {
                            var item = new ListViewItem($"{day:dddd, yyyy-MM-dd}");
                            item.SubItems.Add(r.Guest);
                            item.SubItems.Add(r.RoomType);
                            item.SubItems.Add(r.Request);
                            listViewReport.Items.Add(item);

                            filled.Add(new WeeklyReportRow
                            {
                                Day = r.Day.Date,
                                Guest = r.Guest ?? "",
                                RoomType = r.RoomType ?? "",
                                Request = r.Request ?? ""
                            });
                        }
                    }
                }

                _weeklyEntries = filled;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load weekly report.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------- CSV Export --------------------
        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            if (_weeklyEntries == null || _weeklyEntries.Count == 0)
            {
                MessageBox.Show("Nothing to export.");
                return;
            }

            using (var sfd = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Save Weekly Report as CSV",
                FileName = "WeeklyReport.csv"
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;

                var sb = new StringBuilder();
                sb.AppendLine("Day,Guest,RoomType,Request");

                foreach (var entry in _weeklyEntries)
                {
                    var day = entry.Day.ToString("dddd, yyyy-MM-dd");
                    var guest = EscapeCsv(entry.Guest);
                    var room = EscapeCsv(entry.RoomType);
                    var req = EscapeCsv(entry.Request);
                    sb.AppendLine($"{EscapeCsv(day)},{guest},{room},{req}");
                }

                File.WriteAllText(sfd.FileName, sb.ToString());
                MessageBox.Show("CSV exported successfully.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static string EscapeCsv(string s)
        {
            if (s == null) return "";
            if (s.Contains(",") || s.Contains("\"") || s.Contains("\n"))
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            return s;
        }

        // -------------------- PDF Export --------------------
        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            if (_weeklyEntries == null || _weeklyEntries.Count == 0)
            {
                MessageBox.Show("Nothing to export.");
                return;
            }

            using (var sfd = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Title = "Save Weekly Report as PDF",
                FileName = "WeeklyReport.pdf"
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;

                using (var writer = new PdfWriter(sfd.FileName))
                {
                    var pdf = new PdfDocument(writer);
                    var doc = new Document(pdf);

                    doc.Add(new Paragraph("Weekly Hotel Booking Report").SetFontSize(16));

                    var table = new Table(4, true);
                    table.AddHeaderCell("Day");
                    table.AddHeaderCell("Guest");
                    table.AddHeaderCell("Room Type");
                    table.AddHeaderCell("Request");

                    foreach (var entry in _weeklyEntries)
                    {
                        table.AddCell(entry.Day.ToString("dddd, yyyy-MM-dd"));
                        table.AddCell(entry.Guest ?? "");
                        table.AddCell(entry.RoomType ?? "");
                        table.AddCell(entry.Request ?? "");
                    }

                    doc.Add(table);
                    doc.Close();
                }

                MessageBox.Show("PDF exported successfully.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Designer-wired but unused in this version
        private void listViewReport_SelectedIndexChanged(object sender, EventArgs e) { }
        private void navigationMenu1_Load(object sender, EventArgs e) { }
    }
}
