using System;
using System.Collections.Generic;
using System.ComponentModel;      // LicenseManager.UsageMode
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelBooking.Contracts;     // WeeklyReportRow (class with settable props)
using HotelBookingApp.Services;   // ApiClient
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace HotelBookingApp.Views
{
    public partial class ReportForm : Form
    {
        private ApiClient _api;                                   // injected at runtime
        private List<WeeklyReportRow> _weeklyEntries = new List<WeeklyReportRow>();

        // -------- Designer-safe ctor (no networking) --------
        public ReportForm()
        {
            InitializeComponent();
            this.Name = "ReportForm";

            // Show the current week's Monday in the picker (no API calls here)
            dtpWeekStart.Value = StartOfWeek(DateTime.Today);
        }

        // -------- Runtime ctor (inject ApiClient) --------
        public ReportForm(ApiClient api) : this()
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        private async void ReportForm_Load(object sender, EventArgs e)
        {
            // Design-time OR no API => do nothing so the designer stays happy
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || _api == null)
                return;

            await LoadWeeklyReportAsync(dtpWeekStart.Value);
        }

        // Keep the picker aligned to Mondays (avoid re-entrancy if already Monday)
        private async void dtpWeekStart_ValueChanged(object sender, EventArgs e)
        {
            var current = dtpWeekStart.Value;
            var monday = StartOfWeek(current);
            if (monday != current.Date)            // only set if it actually changes
            {
                dtpWeekStart.Value = monday;
                return;                             // let the next event fire with normalized value
            }

            if (_api != null && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                await LoadWeeklyReportAsync(monday);
        }

        private async void btnLoadWeek_Click(object sender, EventArgs e)
        {
            if (_api == null) return;
            await LoadWeeklyReportAsync(StartOfWeek(dtpWeekStart.Value));
        }

        private static DateTime StartOfWeek(DateTime d)
        {
            d = d.Date;
            while (d.DayOfWeek != DayOfWeek.Monday) d = d.AddDays(-1);
            return d;
        }

        // -------------------- Core loader --------------------
        private async Task LoadWeeklyReportAsync(DateTime weekStart)
        {
            if (_api == null) return;

            try
            {
                var start = StartOfWeek(weekStart);
                var end = start.AddDays(7);   // Mon → next Mon

                List<WeeklyReportRow> rows = null;

                // 1) Try the /reports/daily?start=...&end=... route (EF controllers)
                try
                {
                    rows = await _api.GetDailyReport(start, end);
                }
                catch
                {
                    // 2) Fallback to /reports/weekly?weekStart=... (XML controllers)
                    rows = await _api.GetWeeklyReport(start);
                }

                if (rows == null)
                {
                    rows = new List<WeeklyReportRow>();
                }


                // Render Mon→Sun, filling “No bookings” when needed
                listViewReport.BeginUpdate();
                listViewReport.Items.Clear();
                var filled = new List<WeeklyReportRow>();

                for (int i = 0; i < 7; i++)
                {
                    var day = start.AddDays(i).Date;
                    var rowsForDay = rows.Where(r => r.Day.Date == day).ToList();

                    if (rowsForDay.Count == 0)
                    {
                        var item = new ListViewItem($"{day:dddd, yyyy-MM-dd}");
                        item.SubItems.Add("No bookings");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        listViewReport.Items.Add(item);

                        filled.Add(new WeeklyReportRow { Day = day, Guest = "No bookings", RoomType = "-", Request = "-" });
                    }
                    else
                    {
                        foreach (var r in rowsForDay)
                        {
                            var item = new ListViewItem($"{day:dddd, yyyy-MM-dd}");
                            item.SubItems.Add(r.Guest ?? "");
                            item.SubItems.Add(r.RoomType ?? "");
                            item.SubItems.Add(r.Request ?? "");
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
            catch (System.Net.Http.HttpRequestException httpEx)
            {
                MessageBox.Show(
                    "Failed to load weekly report.\n" +
                    $"Network/HTTP error: {httpEx.Message}\n\n" +
                    "Tip: Ensure the API is running and BaseAddress/UseXmlRoutes in ApiOptions match it.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load weekly report.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                listViewReport.EndUpdate();
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
                    var csv = string.Join(",",
                        EscapeCsv(day),
                        EscapeCsv(entry.Guest),
                        EscapeCsv(entry.RoomType),
                        EscapeCsv(entry.Request));
                    sb.AppendLine(csv);
                }

                File.WriteAllText(sfd.FileName, sb.ToString());
                MessageBox.Show("CSV exported successfully.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static string EscapeCsv(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            return (s.Contains(",") || s.Contains("\"") || s.Contains("\n"))
                ? "\"" + s.Replace("\"", "\"\"") + "\""
                : s;
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

        // Designer-wired placeholders
        private void listViewReport_SelectedIndexChanged(object sender, EventArgs e) { }
        private void navigationMenu1_Load(object sender, EventArgs e) { }
    }
}
